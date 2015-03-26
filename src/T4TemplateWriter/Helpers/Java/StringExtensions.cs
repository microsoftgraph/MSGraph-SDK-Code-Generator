// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System.Text.RegularExpressions;

namespace T4TemplateWriter.Helpers.Java
{
    public static class StringExtensions
    {
        public static string SplitCamelCase(this string input)
        {
            return Regex.Replace(input, "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled);
        }

        public static string Singularize(this string input)
        {
            var output = Inflector.Inflector.Singularize(input);
            return output ?? input;
        }
    }
}
