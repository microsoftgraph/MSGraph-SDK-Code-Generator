// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp
{
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Settings;
    using System;
    using System.Collections.Generic;
    using Vipr.Core.CodeModel;

    public static class TypeHelperCSharp
    {
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
                    return "bool";
                case "date":
                    return "Date";
                case "json":
                    return "Newtonsoft.Json.Linq.JToken";
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
            return GetTypeString(property.Projection.Type);
        }

        public static bool IsTypeNullable(this OdcmProperty property)
        {
            return property.Projection.Type.IsTypeNullable();
        }

        public static bool IsTypeNullable(this OdcmType type)
        {
            var t = type.GetTypeString();
            return type is OdcmClass || t == "Date" || t == "Stream" || t == "string" || t == "byte[]";
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
            return !TypeHelperCSharp.SimpleTypes.Contains(t);
        }

        public static string GetNamespaceName(this OdcmNamespace namespaceObject)
        {
            if (!string.IsNullOrEmpty(ConfigurationService.Settings.NamespaceOverride))
            {
                return ConfigurationService.Settings.NamespaceOverride;
            }

            return Inflector.Inflector.Titleize(namespaceObject.Name);
        }

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToLowerFirstChar();
        }

        public static string GetSanitizedPropertyName(this OdcmProperty property)
        {
            return GetSanitizedPropertyName(property.Name);
        }

        public static string GetSanitizedPropertyName(this string property, string prefix = null)
        {
            if (GetReservedNames().Contains(property))
            {
                var reservedPrefix = string.IsNullOrEmpty(prefix) ? DefaultReservedPrefix : prefix;

                return string.Concat(reservedPrefix, property.ToUpperFirstChar());
            }

            return property;
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
    }
}
