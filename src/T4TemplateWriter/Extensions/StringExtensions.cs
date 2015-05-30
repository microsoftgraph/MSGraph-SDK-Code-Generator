// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.Collections.Generic;

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

        public static string ToCheckedCase(this string input)
        {
            var output = input.Substring(0, 1).ToUpper() + input.Substring(1);
            return output;
        }

        public static IEnumerable<T> ToIEnumerable<T>(this T value)
        {
            yield return value;
        }

    }
}
