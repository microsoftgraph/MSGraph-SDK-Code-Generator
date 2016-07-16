// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static bool Contains(this string source, string compare, StringComparison comparison)
        {
            return source.IndexOf(compare, comparison) >= 0;
        }

        public static bool ToBoolean(this string source)
        {
            return Boolean.Parse(source);
        }

        public static string ToCheckedCase(this string input)
        {
            return input.Substring(0, 1).ToUpper() + input.Substring(1);
        }

        public static IEnumerable<T> ToIEnumerable<T>(this T value)
        {
            yield return value;
        }

        public static string ToLowerFirstChar(this string input) 
        {
            return Char.ToLowerInvariant(input[0]) + input.Substring(1);
        }

        public static string ToUpperFirstChar(this string input) 
        {
            if (input.Length > 0)
            {
                return Char.ToUpperInvariant(input[0]) + input.Substring(1);
            }
            return input;
        }

        public static string SplitCamelCase(this string input) 
        {
            return Regex.Replace(input, "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled);
        }

        public static string ToSpaces(this string toSpaces)
        {
            return Regex.Replace(toSpaces, "[ -~]", " ");
        }

        public static string ToSingularize(this string input) 
        {
            return Inflector.Inflector.Singularize(input);
        }

        public static string ToCamelize (this string input)
        {
            return Inflector.Inflector.Camelize(input);
        }

        public static string ToPascalize(this string input)
        {
            return Inflector.Inflector.Pascalize(input);
        }

        public static string ToUnderscore(this string input)
        {
            return Inflector.Inflector.Underscore(input);
        }

        public static string RemoveFromEnd(this string input, string suffix)
        {
            if (input.EndsWith(suffix))
            {
                return input.Substring(0, input.Length - suffix.Length);
            }
            else
            {
                return input;
            }
        }

        public static bool Equals(this string input, string compareWith)
        {
            return input.Equals(compareWith);
        }
    }
}
