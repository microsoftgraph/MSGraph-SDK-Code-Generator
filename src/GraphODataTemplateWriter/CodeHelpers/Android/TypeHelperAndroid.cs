// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Android
{
    using System.Collections.Generic;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Vipr.Core.CodeModel;
    using System;

    public static class TypeHelperAndroid
    {
        public const string ReservedPrefix = "msgraph_";
        public static HashSet<string> ReservedNames
        {
            get
            {
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                    "abstract", "continue", "for", "new", "switch", "assert", "default", "if", "package", "synchronized", "boolean", "do", "goto", "private", "this", "break", "double", "implements", "protected", "throw", "byte", "else", "import", "public", "throws", "case", "enum", "instanceof", "return", "transient", "catch", "extends", "int", "short", "try", "char", "final", "interface", "static", "void", "class", "finally", "long", "strictfp", "volatile", "const", "float", "native", "super", "while"
                };
            }
        }

        public static string GetReservedPrefix(this OdcmType @type)
        {
            return ReservedPrefix;
        }

        public static string GetTypeString(this OdcmType @type)
        {
            switch (@type.Name)
            {
                case "Int16":
                case "Int32":
                    return "Integer";
                case "Int64":
                    return "Long";
                case "Guid":
                    return "java.util.UUID";
                case "DateTimeOffset":
                    return "java.util.Calendar";
                case "Date":
                    return "com.microsoft.graph.model.DateOnly";
                case "TimeOfDay":
                    return "com.microsoft.graph.model.TimeOfDay";
                case "Json":
                    return "com.google.gson.JsonElement";
                case "Binary":
                    return "byte[]";
                case "Single":
                    return "float";
                default:
                    return @type.Name.ToUpperFirstChar();
            }
        }

        public static string GetTypeString(this OdcmParameter parameter)
        {
            return GetTypeString(parameter.Type);
        }

        public static string GetTypeString(this OdcmProperty property)
        {
            var propertyType = property.Projection.Type;
            var typeString = GetTypeString(propertyType);
            if (propertyType.Namespace != OdcmNamespace.Edm && ReservedNames.Contains(typeString))
            {
                typeString = "com.microsoft.graph.extensions." + typeString;
            }
            return typeString;
        }

        public static bool IsComplex(this OdcmParameter property)
        {
            string t = property.GetTypeString();
            return !(t == "Integer" || t == "java.util.UUID" || t == "java.util.Calendar"
                  || t == "byte[]" || t == "String" || "long" == t || "Byte[]" == t
                  || t == "Short" || t == "com.microsoft.graph.model.DateOnly");
        }

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToLowerFirstChar();
        }

        public static string SanitizePropertyName(this OdcmObject property)
        {
            if (ReservedNames.Contains(property.Name))
            {
                return ReservedPrefix + property.Name;
            }

            return property.Name.Replace("@", string.Empty).Replace(".", "_");
        }

        public static string GetToLowerImport(this OdcmProperty property)
        {
            var type = property.Projection.Type;
            var index = type.Name.LastIndexOf('.');
            return type.Name.Substring(0, index).ToLower() + type.Name.Substring(index);
        }

    }
}
