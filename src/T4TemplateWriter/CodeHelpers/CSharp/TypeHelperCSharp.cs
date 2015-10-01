using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vipr.Core.CodeModel;
using Vipr.T4TemplateWriter.Extensions;

namespace Vipr.T4TemplateWriter.CodeHelpers.CSharp
{
    using System.Linq;
    using System.Text.RegularExpressions;
    public static class TypeHelperCSharp
    {
        public const string ReservedPrefix = "@";
        public static ICollection<string> GetReservedNames()
        {
            return new HashSet<string>{
                "abstract",
                "as",
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
                "struct",
                "switch",
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

        public static string GetTypeString(this OdcmParameter parameter)
        {
            switch (parameter.Type.Name)
            {
                case "String":
                case "Double":
                    return parameter.Type.Name.ToLowerFirstChar();

                default:
                    return parameter.Type.Name;
            }
        }

        public static string GetTypeString(this string type)
        {
            switch (type)
            {
                case "String":
                case "Double":
                    return type.ToLowerFirstChar();
                default:
                    return type.ToCheckedCase();
            }
        }

        public static string GetTypeString(this OdcmType type)
        {
            return type.Name.GetTypeString();
        }

        public static string GetTypeString(this OdcmProperty property)
        {
            return GetTypeString(property.Type);
        }

        public static bool IsTypeNullable(this OdcmProperty property)
        {
            var t = property.GetTypeString();
            return property.Type is OdcmClass || t == "Stream" || t == "string";
        }

        public static bool IsTypeNullable(this OdcmType type)
        {
            var t = type.GetTypeString();
            return type is OdcmClass || t == "Stream" || t == "string";
        }

        public static bool IsComplex(this OdcmParameter property)
        {
            string t = property.GetTypeString();
            return t.IsComplex();
        }

        public static bool IsComplex(this string t)
        {
            return !(t == "Int32" || t == "Int64" || t == "DateTimeOffset"
                   || t == "string" || "long" == t || t == "double");
        }

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToLowerFirstChar();
        }

        public static string GetSanitizedPropertyName(this OdcmProperty property)
        {
            return GetSanitizedPropertyName(property.Name);
        }

        public static string GetSanitizedPropertyName(this string property)
        {
            if (GetReservedNames().Contains(property.ToLower()))
            {
                return ReservedPrefix + property;
            }
            return property;
        }

        public static string GetToLowerImport(this OdcmProperty property)
        {
            var index = property.Type.Name.LastIndexOf('.');
            return property.Type.Name.Substring(0, index).ToLower() + property.Type.Name.Substring(index);
        }
    }
}
