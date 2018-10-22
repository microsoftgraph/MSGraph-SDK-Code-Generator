using System.Linq;

namespace Typewriter
{
    using ApiDoctor.Validation;
    using ApiDoctor.Validation.Error;
    using ApiDoctor.Validation.OData;
    using ApiDoctor.Validation.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal static class CsdlExtensionMethods
    {

        public static string RequestUriPathOnly(this MethodDefinition method, string[] baseUrlsToRemove, IssueLogger issues)
        {
            var path = method.Request.FirstLineOnly().TextBetweenCharacters(' ', '?').TrimEnd('/');

            if (baseUrlsToRemove != null)
            {
                foreach (var baseUrl in baseUrlsToRemove)
                {
                    if (!string.IsNullOrEmpty(baseUrl) && path.StartsWith(baseUrl, StringComparison.OrdinalIgnoreCase))
                    {
                        path = path.Substring(baseUrl.Length);
                    }
                }
            }

            // just in case there's a stray example that doesn't match, at least chop the domain part off.
            foreach (var scheme in new[] { "https://", "http://" })
            {
                if (path.StartsWith(scheme, StringComparison.OrdinalIgnoreCase))
                {
                    int pathStartIndex = path.IndexOf('/', scheme.Length);
                    if (pathStartIndex != -1)
                    {
                        path = path.Substring(pathStartIndex);
                        break;
                    }
                }
            }

            // Normalize variables in the request path
            path = path.ReplaceTextBetweenCharacters('{', '}', "var");

            if (method.RequestMetadata.SampleKeys != null)
            {
                foreach (var key in method.RequestMetadata.SampleKeys)
                {
                    path = path.Replace("/" + key, "/{var}");
                }
            }

            // Normalize function params
            var substitutions = new Dictionary<string, string>();
            var parenIndex = path.IndexOf('(');
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '(')
                {
                    // this is the start of a function. let's find the closing paren.
                    var close = path.IndexOf(')', i);
                    if (close > -1)
                    {
                        var inner = path.Substring(i + 1, close - i - 1);
                        substitutions[inner] = NormalizeFunctionParameters(inner, issues);
                        i = close;
                    }
                }
            }

            foreach (var sub in substitutions)
            {
                path = path.Replace(sub.Key, sub.Value);
            }

            // Rewrite path syntax into what it logically means
            path = path.ReplaceTextBetweenCharacters(':', ':', "/children/{var}", requireSecondChar: false, removeTargetChars: true);

            return path;
        }

        private static string NormalizeFunctionParameters(string funcParams, IssueLogger issues)
        {
            // foo=bar, baz ='qux',x= 9
            var normalized = new StringBuilder();
            var allParams = funcParams.Split(',');
            for (int i = 0; i < allParams.Length; i++)
            {
                var param = allParams[i].Trim();
                var kvp = param.Split('=');
                if (kvp.Length != 2)
                {
                    issues.Error(ValidationErrorCode.ParameterParserError, $"Malformed function params {funcParams}");
                    return funcParams;
                }

                allParams[i] = kvp[0].Trim() + "={var}";
            }

            return string.Join(",", allParams.OrderBy(p => p));
        }

        //public static string HttpMethodVerb(this MethodDefinition method)
        //{
        //    HttpParser parser = new HttpParser();
        //    var request = parser.ParseHttpRequest(method.Request);
        //    return request.Method;

        //}

        internal static void AppendWithCondition(this System.Text.StringBuilder sb, bool condition, string text, string prefixIfExistingContent = null)
        {
            if (condition)
            {
                if (sb.Length > 0 && prefixIfExistingContent != null)
                    sb.Append(prefixIfExistingContent);
                sb.Append(text);
            }
        }

        /// <summary>
        /// Merge two EntityFramework instances together into the first framework
        /// </summary>
        /// <param name="framework1"></param>
        /// <param name="framework2"></param>
        internal static EntityFramework MergeWith(this EntityFramework framework1, EntityFramework framework2)
        {
            ObjectGraphMerger<EntityFramework> merger = new ObjectGraphMerger<EntityFramework>(framework1, framework2);
            var edmx = merger.Merge();

            // Clean up bindingParameters on actions and methods to be consistently the same
            foreach (var schema in edmx.DataServices.Schemas)
            {
                foreach (var action in schema.Actions)
                {
                    foreach (var param in action.Parameters.Where(x => x.Name == "bindingParameter" || x.Name == "this"))
                    {
                        param.Name = "bindingParameter";
                        param.IsNullable = null;
                    }
                }
                foreach (var func in schema.Functions)
                {
                    foreach (var param in func.Parameters.Where(x => x.Name == "bindingParameter" || x.Name == "this"))
                    {
                        param.Name = "bindingParameter";
                        param.IsNullable = null;
                    }
                }
            }

            return edmx;

        }
    }
}
