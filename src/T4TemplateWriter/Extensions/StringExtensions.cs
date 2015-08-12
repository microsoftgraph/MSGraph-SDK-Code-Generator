// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Vipr.T4TemplateWriter.Extensions
{
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

        public static string SplitCamelCase(this String input) 
        {
            return Regex.Replace(input, "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled);
        }

        public static string Singularize(this string input) 
        {
            return Inflector.Inflector.Singularize(input);
        }

        public static string Camelize (this string input)
        {
            return Inflector.Inflector.Camelize(input);
        }

        public static string UnderScore(this string input)
        {
            return Inflector.Inflector.Underscore(input);
        }

    }
}
