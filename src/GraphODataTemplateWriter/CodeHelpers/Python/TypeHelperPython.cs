// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Python
{
    using System.Collections.Generic;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Vipr.Core.CodeModel;
    using NLog;

    public static class TypeHelperPython
    {
        public const string ReservedPrefix = "$$__$$";
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static ICollection<string> ReservedNames
        {
            get
            {
                return new HashSet<string> {
                    //"abstract", "continue", "for", "new", "switch", "assert", "default", "if", "package", "synchronized", "boolean", "do", "goto", "private", "this", "break", "double", "implements", "protected", "throw", "byte", "else", "import", "public", "throws", "case", "enum", "instanceof", "return", "transient", "catch", "extends", "int", "short", "try", "char", "final", "interface", "static", "void", "class", "finally", "long", "strictfp", "volatile", "const", "float", "native", "super", "while"
                };
            }
        }

        public static string GetTypeString(this OdcmType @type)
        {
            switch (@type.Name)
            {
                case "String":
                    return "str";
                case "Int8":
                case "Int16":
                case "Int32":
                case "Int64":
                    return "int";
                case "Double":
                    return "float";
                case "Guid":
                    return "UUID";
                case "DateTimeOffset":
                    return "datetime";
                case "Boolean":
                    return "bool";
                case "Binary":
                case "Stream":
                    return "bytes";
                default:
                    return @type.Name.ToUpperFirstChar();
            }
        }

        public static string GetTypeString(this OdcmParameter parameter)
        {
            switch (@parameter.Type.Name)
            {
                case "String":
                    return "str";
                case "Int8":
                case "Int16":
                case "Int32":
                case "Int64":
                    return "int";
                case "Double":
                    return "float";
                case "Guid":
                    return "UUID";
                case "DateTimeOffset":
                    return "datetime";
                case "Boolean":
                    return "bool";
                case "Binary":
                case "Stream":
                    return "bytes";
                default:
                    return @parameter.Type.Name.ToUpperFirstChar();
            }
        }



        public static string GetTypeString(this OdcmProperty property)
        {
            return GetTypeString(property.Projection.Type);
        }


        public static bool IsComplex(this OdcmType type)
        {
            string t = type.GetTypeString();
            return !(t == "int" || t == "UUID" || t == "datetime"
                  || t == "bool" || t == "str" || "bytes" == t
                  || t == "float");
        }

        public static bool IsComplex(this OdcmProperty property)
        {
            return property.Projection.Type.IsComplex();
        }

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToLowerFirstChar();
        }

        public static string SanitizePropertyName(this OdcmProperty property)
        {
            if (ReservedNames.Contains(property.Name.ToLower())) {
                logger.Info("Property \"{0}\" is a reserved word in Python. Converting to \"{1}{0}\"", property.Name, ReservedPrefix);
                return ReservedPrefix + property.Name;
            }
            return property.Name;
        }

        public static string GetToLowerImport(this OdcmProperty property)
        {
            var type = property.Projection.Type;
            var index = type.Name.LastIndexOf('.');
            return type.Name.Substring(0, index).ToLower() + type.Name.Substring(index);
        }

    }
}
