// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Vipr.Core.CodeModel;
    using NLog;

    public static class TypeHelperCSharp
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
            return !TypeHelperCSharp.SimpleTypes.Contains(t);
        }

        public static string GetNamespaceName(this OdcmNamespace namespaceObject)
        {
            return Inflector.Inflector.Titleize(namespaceObject.Name);
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
                return description.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
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
    }
}
