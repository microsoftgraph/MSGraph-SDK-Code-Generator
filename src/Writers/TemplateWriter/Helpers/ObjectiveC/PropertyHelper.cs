using System;
using System.Threading;
using Vipr.Core.CodeModel;

namespace TemplateWriter.Helpers.ObjectiveC
{
    public static class PropertyHelperIsSystem
    {
        public static string GetTypeString(this OdcmType type)
        {
            if (type == null)
            {
                return "error!";
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
                    return "Byte";
                case "Boolean":
                    return "bool";
                case "Stream":
                    return "NSStream";
                default:
                    //return Builder.GetClassPrefix() + Builder.GetContainerName() + property.Type;
                    return "FixMe"; //TODO: Fix
            }
        }

        public static string GetTypeString(this OdcmProperty property)
        {
            return GetTypeString(property.Type);
        }

        public static bool IsComplex(this OdcmProperty property)
        {
            string t = property.GetTypeString();
            return !(t == "int" || t == "bool" || t == "Byte");
        }

        public static bool IsComplex(this OdcmType type)
        {
            string t = type.GetTypeString();
            return !(t == "int" || t == "bool" || t == "Byte");
        }

        public static string ToPropertyString(this OdcmProperty property)
        {
            return string.Format("{0} {1}{2}", property.GetFullType(), (property.IsComplex() ? "*" : string.Empty), property.Name);
        }

        public static string GetFullType(this OdcmProperty property)
        {
            string result;
            if (property.IsCollection)
                result = !property.IsSystem() ? 
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
            return (t == "int" || t == "bool" || t == "Byte" || t == "NSString" || t == "NSDate");
        }

        public static bool IsSystem(this OdcmType type)
        {
            string t = type.GetTypeString();
            return (t == "int" || t == "bool" || t == "Byte" || t == "NSString" || t == "NSDate");
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

        public static bool IsEnum(this OdcmProperty property)
        {
            return property.Type is OdcmEnum;
        }

        public static bool IsCollection(this OdcmProperty property)
        {
            return property.IsCollection;
        }
    }
}