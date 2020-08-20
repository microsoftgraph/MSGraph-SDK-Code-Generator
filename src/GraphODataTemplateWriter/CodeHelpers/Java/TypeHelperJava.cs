// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Java
{
    using System.Collections.Generic;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Vipr.Core.CodeModel;
    using System;
    using NLog;

    public static class TypeHelperJava
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public const string ReservedPrefix = "msgraph";
        public static Lazy<HashSet<string>> ReservedNames { get; private set; } = new Lazy<HashSet<string>>(() => new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                    "abstract", "continue", "for", "new", "switch", "assert", "default", "if", "package", "synchronized", "boolean", "do", "goto", "private", "this", "break", "double", "implements", "protected", "throw", "byte", "else", "import", "public", "throws", "case", "enum", "instanceof", "return", "transient", "catch", "extends", "int", "short", "try", "char", "final", "interface", "static", "void", "class", "finally", "long", "strictfp", "volatile", "const", "float", "native", "super", "while", "true", "false", "null", "import"
                });

        public static string GetReservedPrefix(this OdcmType @type)
        {
            return ReservedPrefix;
        }

        public static string GetTypeString(this OdcmType @type)
        {
            // If isFlags = true, return an EnumSet instead of an enum. This will be 
            // serialized to and deserialized from a string
            if (String.Equals(@type.ToString(), "Vipr.Core.CodeModel.OdcmEnum") && @type.AsOdcmEnum().IsFlags)
            {
                return "EnumSet<" + @type.Name.ToUpperFirstChar() + ">";
            }

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
                    return "com.microsoft.graph.models.extensions.DateOnly";
                case "TimeOfDay":
                    return "com.microsoft.graph.models.extensions.TimeOfDay";
                case "Duration":
                    return "javax.xml.datatype.Duration";
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
            if (propertyType.Namespace != OdcmNamespace.Edm && ReservedNames.Value.Contains(typeString))
            {
                typeString = "com.microsoft.graph.models.extensions." + typeString;
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

        public static string SanitizePropertyName(this string property, OdcmObject odcmProperty = null)
        {
            if (ReservedNames.Value.Contains(property))
            {
                var result = ReservedPrefix + property.ToUpperFirstChar();
                logger.Info($"Property \"{property}\" is a reserved word in Java. Converting to \"{result}\"");
                return result;
            }
            else if (property == odcmProperty?.Name?.ToUpperFirstChar() && !(odcmProperty?.Name?.StartsWith("_") ?? false))
            {
                // Name the property by its type. Sanitize it in case the type is a reserved name.  
                return odcmProperty?.Projection?.Type?.Name?.ToUpperFirstChar()?.SanitizePropertyName(odcmProperty) ?? odcmProperty?.Name?.SanitizePropertyName();
            }
            else
                return property?.Replace("@", string.Empty)?.Replace(".", string.Empty);
        }

        public static string GetToLowerImport(this OdcmProperty property)
        {
            var type = property.Projection.Type;
            var index = type.Name.LastIndexOf('.');
            return type.Name.Substring(0, index).ToLower() + type.Name.Substring(index);
        }

    }
}
