using System;
using System.Globalization;
using Inflector;

namespace TemplateWriter
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string compare, StringComparison comparison)
        {
            return source.IndexOf(compare, comparison) >= 0;
        }

        public static string ToCheckedCase(this string input)
        {
            var output = input.Substring(0, 1).ToUpper() + input.Substring(1);
            return output;
        }

    }
}
