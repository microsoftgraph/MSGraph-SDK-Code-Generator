// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.Collections.Generic;
using Vipr.T4TemplateWriter.Extensions;
using Vipr.T4TemplateWriter.CodeHelpers;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.CodeHelpers.Java
{
    public static class TypeHelperJava
    {
        public const string ReservedPrefix = "$$__$$";
        public static ICollection<string> ReservedNames
        {
            get
            {
                return new HashSet<string> {
                    "abstract", "continue", "for", "new", "switch", "assert", "default", "if", "package", "synchronized", "boolean", "do", "goto", "private", "this", "break", "double", "implements", "protected", "throw", "byte", "else", "import", "public", "throws", "case", "enum", "instanceof", "return", "transient", "catch", "extends", "int", "short", "try", "char", "final", "interface", "static", "void", "class", "finally", "long", "strictfp", "volatile", "const", "float", "native", "super", "while"
                };
            }
        }

        public static string GetTypeString(this OdcmType @type)
        {
            switch (@type.Name)
            {
                case "Int32":
                    return "Integer";
                case "Int64":
                    return "Long";
                case "Guid":
                    return "java.util.UUID";
                case "DateTimeOffset":
                    return "java.util.Calendar";
                case "Binary":
                case "Stream":
                    return "byte[]";
                default:
                    return @type.Name.ToUpperFirstChar();
            }
        }

        public static string GetTypeString(this OdcmParameter parameter)
        {
            switch (parameter.Type.Name)
            {
                case "Int32":
                    return "Integer";
                case "Int64":
                    return "Long";
                case "Guid":
                    return "java.util.UUID";
                case "DateTimeOffset":
                    return "java.util.Calendar";
                case "Binary":
                case "Stream":
                    return "byte[]";
                default:
                    return parameter.Type.Name;
            }
        }



        public static string GetTypeString(this OdcmProperty property)
        {
            return GetTypeString(property.Type);
        }


        public static bool IsComplex(this OdcmParameter property)
        {
            string t = property.GetTypeString();
            return !(t == "Integer" || t == "java.util.UUID" || t == "java.util.Calendar"
                  || t == "byte[]" || t == "String" || "long" == t || "Byte[]" == t);
        }

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToLowerFirstChar();
        }

        public static string SanitizePropertyName(this OdcmObject property)
        {
            if (ReservedNames.Contains(property.Name.ToLower()))
            {
                return ReservedPrefix + property.Name;
            }
            return property.Name;
        }

        public static string GetToLowerImport(this OdcmProperty property)
        {
            var index = property.Type.Name.LastIndexOf('.');
            return property.Type.Name.Substring(0, index).ToLower() + property.Type.Name.Substring(index);
        }

    }
}
