// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.PHP
{
    using System.Collections.Generic;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Vipr.Core.CodeModel;
    using System;

    public static class TypeHelperPHP
    {
        public const string ReservedPrefix = "msgraph_";
        public static HashSet<string> ReservedNames
        {
            get
            {
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                    "abstract", "and", "array()", "as", "break", "callable", "case", "catch", "class", "clone", "const", "continue", "declare", "default", "die()",
                    "do", "echo", "else", "elseif", "empty()", "enddeclare", "endfor", "endforeach", "endif", "endswitch", "endwhile", "eval()", "exit()", "extends",
                    "final", "finally", "for", "foreach", "function", "global", "goto", "if", "implements", "include", "include_once", "instanceof", "insteadof", "interface",
                    "isset()", "list()", "namespace", "new", "or", "print", "private", "protected", "public", "require", "require_once", "return", "static", "switch",
                    "throw", "trait", "try", "unset()", "use", "var", "while", "xor", "yield", "int", "float", "bool", "string", "true", "false", "null"
                };
            }
        }

        private static readonly ICollection<string> SimpleTypes =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "int32",
                "int64",
                "datetimeoffset",
                "long",
                "double",
                "string"
            };


        public static string GetTypeString(this OdcmType @type)
        {
            switch (@type.Name)
            {
                case "String":
                case "Json":
                case "Guid":
                    return "string";
                case "Int8":
                case "Int16":
                case "Int32":
                case "Int64":
                    return "int";
                case "Double":
                    return "float";
                case "DateTimeOffset":
                case "Date":
                    return "\\DateTime";
                case "Boolean":
                    return "bool";
                case "Binary":
                case "Stream":
                    return "\\GuzzleHttp\\Psr7\\Stream";
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
                  || t == "bool" || t == "string" || "bytes" == t
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
        public static string GetNamespaceName(this OdcmNamespace namespaceObject)
        {
            return Inflector.Inflector.Titleize(namespaceObject.Name);
        }
    }
}
