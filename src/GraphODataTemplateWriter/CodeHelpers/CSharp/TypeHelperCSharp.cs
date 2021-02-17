// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Inflector;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using NLog;
    using Vipr.Core.CodeModel;

    public static class TypeHelperCSharp
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string DeprecationString = "[Obsolete(\"{0}\")]";

        public const string DefaultReservedPrefix = "@";
        public static ICollection<string> GetReservedNames()
        {
            return new HashSet<string>(StringComparer.Ordinal)
            {
                "abstract",
                "as",
                "async",
                "base",
                "bool",
                "break",
                "byte",
                "case",
                "catch",
                "char",
                "checked",
                "class",
                "const",
                "continue",
                "decimal",
                "default",
                "delegate",
                "do",
                "double",
                "else",
                "enum",
                "event",
                "explicit",
                "extern",
                "false",
                "finally",
                "fixed",
                "float",
                "for",
                "foreach",
                "goto",
                "if",
                "implicit",
                "in",
                "int",
                "interface",
                "internal",
                "is",
                "lock",
                "long",
                "namespace",
                "new",
                "null",
                "object",
                "operator",
                "out",
                "override",
                "params",
                "private",
                "null",
                "object",
                "operator",
                "out",
                "override",
                "params",
                "private",
                "protected",
                "public",
                "readonly",
                "ref",
                "return",
                "sbyte",
                "sealed",
                "short",
                "sizeof",
                "stackalloc",
                "static",
                "string",
                "String",
                "struct",
                "switch",
                "Task",
                "this",
                "throw",
                "true",
                "try",
                "typeof",
                "uint",
                "ulong",
                "unchecked",
                "unsafe",
                "ushort",
                "using",
                "virtual",
                "void",
                "volatile",
                "while",
            };
        }

        public static ICollection<string> GetReservedModelNames()
        {
            return new HashSet<string>(StringComparer.Ordinal)
            {
                "Required"
            };
        }

        private static readonly ICollection<string> SimpleTypes =
            new HashSet<string> (StringComparer.OrdinalIgnoreCase)
            {
                "int32",
                "int64",
                "datetimeoffset",
                "long",
                "double",
                "string"
            };

        public static string GetTypeString(this OdcmParameter parameter)
        {
            return parameter.Type.Name.GetTypeString();
        }

        public static string GetTypeString(this string type)
        {
            switch (type.ToLowerInvariant())
            {
                case "string":
                case "double":
                    return type.ToLowerFirstChar();
                case "binary":
                    return "byte[]";
                case "boolean":
                case "bool":
                    return "bool";
                case "date":
                    return "Date";
                case "json":
                    return "Newtonsoft.Json.Linq.JToken";
                default:
                    return type.ToCheckedCase();
            }
        }

        /// <summary>
        /// We use Request postfix to create Request objects in the SDK
        /// but metadata has type names that end with Request as well. To disambiguate those
        /// we append Object at the end of the generated type name.
        /// </summary>
        /// <param name="typeName">type name that potentially ends with "Request"</param>
        /// <returns>disambiguated type name. If the type is myRequest, it is converted into myRequestObject</returns>
        public static string DisambiguateRequestObject(this string typeName)
        {
            if (typeName.EndsWith("Request"))
            {
                return $"{typeName}Object";
            }

            return typeName;
        }

        public static string GetTypeString(this OdcmType type)
        {
            return type.Name.GetTypeString();
        }

        public static string GetTypeString(this OdcmProperty property)
        {
            return GetTypeString(property.Projection.Type);
        }

        /// <summary>
        /// Determines whether the type needs fully qualified name or simple type name in the given namespace context
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="namespaceContext">namespace where type is referenced</param>
        /// <param name="format">a generated type format, such as I{0}RequestBuilder</param>
        /// <returns>Either the fully qualified or plain type name</returns>
        public static string GetTypeString(this OdcmType type, string namespaceContext, string format = null)
        {
            var typesNamespace = type.Namespace.GetNamespaceName().Replace("edm.", "");
            var plainTypeString = type.GetTypeString();

            // refer to core implementation when they are available for Edm types
            var coreModelTypes = new List<string> { "Date", "Duration", "TimeOfDay" };
            if (typesNamespace == "Edm" && coreModelTypes.Contains(plainTypeString))
            {
                typesNamespace = "Microsoft.Graph";
            }

            if (format != null)
            {
                plainTypeString = string.Format(format, plainTypeString);
            }

            if (string.Equals(typesNamespace, namespaceContext, StringComparison.OrdinalIgnoreCase)
                || typesNamespace == "Edm")
            {
                return plainTypeString;
            }
            else
            {
                return typesNamespace + "." + plainTypeString;
            }
        }

        /// <summary>
        /// Wrapper for GetTypeString for property input
        /// </summary>
        /// <param name="property">property whose type needs to be resolved</param>
        /// <param name="namespaceContext">namespace where property is referenced</param>
        /// <param name="format">a generated type format, such as I{0}RequestBuilder</param>
        /// <returns>Either the fully qualified or plain type name</returns>
        public static string GetTypeString(this OdcmProperty property, string namespaceContext, string format = null)
        {
            return property.Projection.Type.GetTypeString(namespaceContext, format);
        }

        public static string GetDeprecationString(this OdcmObject instance)
        {
            if (instance.IsDeprecated)
            {
                return String.Format(DeprecationString, instance.Deprecation.Description);
            }

            return String.Empty;
        }

        public static bool IsTypeNullable(this OdcmProperty property)
        {
            return property.Projection.Type.IsTypeNullable();
        }

        /// <summary>
        /// converts from Microsoft.Graph.CallRecords.Class to Microsoft.Graph.CallRecords.IClass
        /// </summary>
        /// <param name="class">Class name (which can be fully qualified with namespaces)</param>
        /// <returns>Interface name</returns>
        public static string ClassToInterface(this string @class)
        {
            if (@class == null)
            {
                throw new ArgumentNullException(nameof(@class));
            }

            var split = @class.Split('.');
            split[split.Length - 1] = $"I{split.Last()}";
            return string.Join(".", split);
        }

        /// <summary>
        /// Gets the class' namespace where the property appears
        /// </summary>
        /// <param name="property">property</param>
        /// <returns>Namespace where class of a property belongs to</returns>
        public static string GetClassNamespace(this OdcmProperty property)
        {
            return property?.Class?.AsOdcmClass()?.GetNamespaceName();
        }

        /// <summary>
        /// Gets namespace of a class
        /// </summary>
        /// <param name="odcmClass">odcm class</param>
        /// <returns>Namespace of a class</returns>
        public static string GetNamespaceName(this OdcmClass odcmClass)
        {
            return odcmClass?.Namespace?.GetNamespaceName();
        }

        public static bool IsTypeNullable(this OdcmType type)
        {
            var t = type.GetTypeString();
            return type is OdcmClass || t == "Date" || t == "Stream" || t == "string" || t == "byte[]" || t == "TimeOfDay" || t == "Duration";
        }

        public static bool IsByteArray(this OdcmProperty property)
        {
            var t = property.GetTypeString();
            return t == "byte[]";
        }

        public static bool IsComplex(this OdcmProperty property)
        {
            return property.Projection.Type.IsComplex();
        }

        public static bool IsComplex(this OdcmParameter property)
        {
            string t = property.GetTypeString();
            return t.IsComplex();
        }

        public static bool IsComplex(this OdcmType type)
        {
            string t = type.GetTypeString();
            return t.IsComplex();
        }

        public static bool IsComplex(this string t)
        {
            return !SimpleTypes.Contains(t);
        }
        private static readonly Inflector inflector = new Inflector(CultureInfo.GetCultureInfo("en-US"));
        public static string GetNamespaceName(this OdcmNamespace namespaceObject)
        {
            return inflector.Titleize(namespaceObject.Name).Replace(" ", "");
        }

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToLowerFirstChar();
        }

        public static string GetSanitizedLongDescription(this OdcmProperty property)
        {
            var description = property.LongDescription ?? property.Description;

            if (description != null)
            {
                return description.Replace("<", "&lt;")
                                  .Replace(">", "&gt;")
                                  .Replace("&", "&amp;")
                                  .Replace("\r\n", "\r\n///"); // &#xD;&#xA; The HTML encoded has already been converted to escaped chars.
            }
            return null;
        }

        public static string GetSanitizedPropertyName(this OdcmProperty property, string prefix = null, string suffix = null)
        {
            return GetSanitizedPropertyName(property.Name, prefix, suffix);
        }

        public static string GetSanitizedClassName(this OdcmClass odcmClass)
        {
            return GetSanitizedClassName(odcmClass.Name, odcmClass);
        }

        public static string GetSanitizedPropertyName(this string property, string prefix = null, string suffix = null)
        {
            return GetSanitizedPropertyName(property, null, prefix, suffix);
        }

        /// <summary>
        /// Sanitizes a property name for the following conditions: 
        /// 1) a property has the same name as a C# keyword. Prefix @ to the property name to make it valid. 
        /// 2) a property has the same name as the class. First we'll try to change the property name to the
        /// return type name. If the return type name is the same as the class name, then we'll append 
        /// "Property" to the property name.
        /// </summary>
        /// <param name="property">The string that called this extension.</param>
        /// <param name="odcmProperty">An OdcmProperty. Use the property that you want to sanitize.</param>
        /// <param name="prefix">The prefix to use on this property.</param>
        /// <returns></returns>
        public static string GetSanitizedPropertyName(this string property, OdcmProperty odcmProperty, string prefix = null, string suffix = null)
        {
            string result = property;
            if (GetReservedNames().Contains(property))
            {
                var reservedPrefix = string.IsNullOrEmpty(prefix) ? DefaultReservedPrefix : prefix;

                logger.Info("Property \"{0}\" is a reserved word in .NET. Converting to \"{1}{0}\"", property.ToUpperFirstChar(), reservedPrefix);
                result = string.Concat(reservedPrefix, property.ToUpperFirstChar());
            }
            else if (odcmProperty != null && property == odcmProperty.Class.Name.ToUpperFirstChar())
            {
                // Check whether the propertyObject is null (means they called the extension from a string).
                // Check whether the property name is the same as the class name.
                // Only constructor members may be named the same as the class name.

                // Check whether the property type is the same as the class name.
                if (odcmProperty.Projection.Type.Name.ToUpperFirstChar() == odcmProperty.Class.Name.ToUpperFirstChar())
                {
                    // Name the property: {metadataName} + "Property"
                    logger.Info("Property type \"{0}\" has the same name as the class. Converting to \"{0}Property\"", property);
                    result = string.Concat(property, "Property");
                }
                else
                {
                    // Name the property by its type. Sanitize it in case the type is a reserved name.  
                    result = odcmProperty.Projection.Type.Name.ToUpperFirstChar().GetSanitizedPropertyName();
                }
            }

            return result+suffix;
        }

        public static string GetSanitizedClassName(this string className, OdcmClass odcmClass)
        {
            var entityName = className.ToCheckedCase();
            if (entityName.EndsWith("Request"))
            {
                entityName = String.Concat(entityName, "Object");
            }
            return entityName;
        }

        public static string GetSanitizedParameterName(this string parameter, string prefix = null)
        {
            if (GetReservedNames().Contains(parameter))
            {
                var reservedPrefix = string.IsNullOrEmpty(prefix) ? DefaultReservedPrefix : prefix;

                return string.Concat(reservedPrefix, parameter.ToLowerFirstChar());
            }

            return parameter;
        }

        public static string GetToLowerImport(this OdcmProperty property)
        {
            var type = property.Projection.Type;
            var index = type.Name.LastIndexOf('.');
            return type.Name.Substring(0, index).ToLower() + type.Name.Substring(index);
        }

        /// <summary>
        /// If the type appears in Microsoft.Graph namespace, use plain type name
        /// otherwise use fully qualified type name.
        /// </summary>
        /// <param name="namespace">Namespace where type appears</param>
        /// <param name="type">type</param>
        /// <returns>Either the fully qualified or plain type name for core library types</returns>
        public static string GetCoreLibraryType(this string @namespace, string type)
        {
            if (@namespace == "Microsoft.Graph")
            {
                return type;
            }
            else
            {
                return $"Microsoft.Graph.{type}";
            }
        }

        /// <summary>
        /// Use this method to get the method information for all of the methods.
        /// Used in *MethodRequestBuilder
        /// </summary>
        /// <param name="methods">A list of OdcmMethods.</param>
        /// <param name="namespace">The namespace for the methods.</param>
        /// <returns>An ordered (to minimize diffs caused by reorder in the CSDL) list of 
        /// MethodInfo objects used to construct the request builders.</returns>
        public static List<MethodInfo> GetAllMethodsInfo(this List<OdcmMethod> methods, string @namespace)
        {
            return methods.Select(m =>
            {
                var parameters = m.Parameters
                    .Select(p =>
                    {
                        var type = p.Type.GetTypeString(@namespace);
                        var name = p.Name.ToLowerFirstChar();
                        var parameterName = p.Name.GetSanitizedParameterName();
                        var propertyName = p.Name.ToCheckedCase();

                        // Adds support for classes ending in "Request" that have been dismabiguated.
                        if (type.EndsWith("Request"))
                        {
                            type = String.Concat(type, "Object");
                        }

                        // Adjust the type string
                        if (p.IsCollection)
                        {
                            type = $"IEnumerable<{type}>";
                        }
                        else if (!p.Type.IsTypeNullable() && p.IsNullable)
                        {
                            type += "?";
                        }

                        return new ParameterInfo(type, name, parameterName, propertyName, p.IsNullable);
                    })
                    .OrderBy(p => p.IsNullable ? 1 : 0); // I suspect this could cause a problem if new overloads are added.

                var paramStrings = parameters.Select(p => $",\n            {p.Type} {p.ParameterName}");
                var paramComments = parameters.Select(p => $"\n        /// <param name=\"{p.ParameterName}\">A {p.ParameterName} parameter for the OData method call.</param>");
                var paramArgsForConstructor = parameters.Select(p => $",\n                {p.ParameterName}");

                var entityName = m.Class.Name.ToCheckedCase();
                var methodName = m.Name.ToCheckedCase();
                var requestType = entityName + methodName + "Request";
                var requestBuilderType = requestType + "Builder";

                return new MethodInfo()
                {
                    Parameters = parameters,
                    ParametersAsArguments = string.Join(string.Empty, paramStrings),
                    ParamArgsForConstructor = string.Join(string.Empty, paramArgsForConstructor),
                    ParameterComments = string.Join(string.Empty, paramComments),
                    RequestBuilderType = requestBuilderType,
                    MethodName = methodName,
                    MethodFullName = m.FullName,
                    MethodParametersAsArguments = paramStrings.Count() == 0 ? string.Empty : string.Join(string.Empty, paramStrings).Substring(1)
                };
            }).OrderBy(m => m.MethodName).ToList();
        }

        /// <summary>
        /// Gets the list of navigation property information for CSharp *MethodRequestBuilder.
        /// </summary>
        /// <param name="navProperties"></param>
        /// <returns></returns>
        public static List<NavigationPropertyInfo> GetAllNavigationPropertyInfo(this List<OdcmProperty> navProperties)
        {
            return navProperties.Select(p =>
            {
                var returnClassRequestBuilderName = String.Format("{0}RequestBuilder", p.Type.Name.ToCheckedCase());
                var returnInterfaceRequestBuilderName = String.Format("I{0}", returnClassRequestBuilderName);
                var name = p.Name.ToCheckedCase();
                var segment = p.Name;
                var description = p.Description ?? string.Empty;
                return new NavigationPropertyInfo()
                {
                    ReturnInterfaceRequestBuilderName = returnInterfaceRequestBuilderName,
                    ReturnClassRequestBuilderName = returnClassRequestBuilderName,
                    Segment = segment,
                    Name = name,
                    Description = description
                };
            }).OrderBy(n => n.Name).ToList();
        }
    }
}
