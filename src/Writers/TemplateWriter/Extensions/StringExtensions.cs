using System;

namespace TemplateWriter.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string compare, StringComparison comparison)
        {
            return source.IndexOf(compare, comparison) >= 0;
        }

        public static string Singularize(this string source)
        {
            return Inflector.Inflector.Singularize(source);
        }
    }
}
