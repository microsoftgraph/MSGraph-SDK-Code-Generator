// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Helpers.ObjectiveC
{
    public static class PropertyHelper
    {
        public static string Prefix = "";

        public static string GetTypeString(this OdcmType type)
        {
            if (type == null)
            {
                return "int";
            }
            switch (type.Name)
            {
                case "String":
                    return "NSString";
                case "Int32":
                    return "int";
                case "Int64":
                    return "int";
                case "Guid":
                    return "NSString";
                case "DateTimeOffset":
                    return "NSDate";
                case "Binary":
                    return "NSData";
                case "Boolean":
                    return "BOOL";
                case "Stream":
                    return "NSStream";
                default:
                    return Prefix + type.Name;
            }
        }

        public static string GetTypeString(this OdcmProperty property)
        {
            return GetTypeString(property.Type);
        }

        public static bool IsComplex(this OdcmProperty property)
        {
            string t = property.GetTypeString();
            return !(t == "int" || t == "BOOL" || t == "Byte");
        }

        public static bool IsComplex(this OdcmType type)
        {
            string t = type.GetTypeString();
            return !(t == "int" || t == "BOOL" || t == "Byte");
        }

        public static string ToSetterTypeString(this OdcmProperty property)
        {
            return string.Format("{0} {1}", property.GetFullType(), (property.IsComplex() ? "*" : string.Empty));
        }

        public static string ToPropertyString(this OdcmProperty property)
        {
            return string.Format("{0} {1}{2}", property.GetFullType(), (property.IsComplex() ? "*" : string.Empty), GetName(property.Name));
        }

        public static string GetName(string name)
        {
            if (name.Trim() == "description") return "$$__description";

            if (name.Trim() == "default") return "$$__default";

            if (name.Trim() == "self") return "$$__self";

            return name;
        }

        public static string GetFullType(this OdcmProperty property)
        {
            string result;
            if (property.IsCollection)
                result = !property.IsSystem() && property.GetTypeString() != "NSData" ?
                    string.Format("NSMutableArray<{0}>", property.GetTypeString()) : "NSMutableArray";
            else
                result = property.GetTypeString();

            return result;
        }

        public static string GetFullType(this OdcmType type)
        {
            return type.GetTypeString();
        }

        public static bool IsSystem(this OdcmProperty property)
        {
            string t = property.GetTypeString();
            return (t == "int" || t == "BOOL" || t == "Byte" || t == "NSString" || t == "NSDate");
        }

        public static bool IsSystem(this OdcmType type)
        {
            string t = type.GetTypeString();
            return (t == "int" || t == "BOOL" || t == "Byte" || t == "NSString" || t == "NSDate");
        }

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToLowerFirstChar();
        }

        public static string GetToLowerImport(this OdcmProperty property)
        {
            var index = property.Type.Name.LastIndexOf('.');
            return property.Type.Name.Substring(0, index).ToLower() + property.Type.Name.Substring(index);
        }

        public static string ToLowerFirstChar(this string name)
        {
            return Char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        public static string ToUpperFirstChar(this string name)
        {
            return Char.ToUpperInvariant(name[0]) + name.Substring(1);
        }

        public static bool IsEnum(this OdcmProperty property)
        {
            return property.Type is OdcmEnum;
        }
    }
}
