// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information. 

namespace Vipr.T4TemplateWriter.CodeHelpers.ObjC
{
    using System.Collections.Generic;
    using Vipr.Core.CodeModel;
    using Vipr.T4TemplateWriter.Extensions;
    using Vipr.T4TemplateWriter.Settings;

    public static class TypeHelperObjC
	{
	    public static string Prefix = ConfigurationService.Settings.NamespacePrefix;

        private static ICollection<string> reservedNames;
        public static ICollection<string> ReservedNames
        {
            get 
            {
                if (reservedNames == null)
                {
                    reservedNames=new HashSet<string> 
                    {
                        "id",
                        "YES",
                        "NO",
                        "true",
                        "false",
                        "NULL",
                        "nil",
                        "self",
                        "description",
                        "auto",
                        "else",
                        "long",
                        "switch",
                        "break",
                        "enum",
                        "register",
                        "typedef",
                        "case",
                        "extern",
                        "return",
                        "union",
                        "char",
                        "float",
                        "short",
                        "unsigned",
                        "const",
                        "for",
                        "signed",
                        "void",
                        "continue",
                        "goto",
                        "sizeof",
                        "volatile",
                        "default",
                        "if",
                        "static",
                        "while",
                        "do",
                        "int",
                        "struct",
                        "_Packed",
                        "double",
                        "protocol",
                        "interface",
                        "implementation",
                        "NSObject",
                        "NSInteger",
                        "NSNumber",
                        "CGFloat",
                        "property",
                        "nonatomic",
                        "retain",
                        "weak",
                        "unsafe_unretained",
                        "readwrite",
                        "readonly",
                        "inline",
                        "operations"
                    };
                }
                return reservedNames;
            }
        }

        public static string GetTypeString(this OdcmType type) 
        {
            if (type == null) 
            {
                return "id";
            }
            switch (type.Name) {
                case "String":
                    return "NSString";
                case "Int32":
                    return "int32_t";
                case "Int64":
                    return "int64_t";
                case "Int16":
                    return "int16_t";
                case "Guid":
                    return "NSString";
                case "Double":
                case "Float":
                    return "CGFloat";
                case "DateTimeOffset":
                case "Date":
                    return "NSDate";
                case "Binary":
                    return "NSString";
                case "Boolean":
                    return "BOOL";
                case "Stream":
                    return "NSStream";
                default:
                    return Prefix + type.Name.ToUpperFirstChar();
            }
        }

        public static string GetTypeString(this OdcmProperty property) 
        {
            return property.Type.GetTypeString();
        }

        public static bool IsComplex(this OdcmType type) 
        {
            string t = GetTypeString(type);
            return
                !(t.Contains("int") || t == "BOOL" || t == "Byte" || t == "CGFloat" ||
                  type is OdcmEnum);
        }

		public static bool IsComplex(this OdcmProperty property) 
        {
            return property.Type.IsComplex();
        }

        public static string ToSetterTypeString(this OdcmProperty property)
        {
            return string.Format("{0} {1}", property.GetFullType(), (property.IsComplex() ? "*" : string.Empty));
        }

        public static string SanitizePropertyName(this OdcmProperty property) 
        {
            if (ReservedNames.Contains(property.Name.ToLower())) 
            {
                return property.Class.Name.ToLowerFirstChar() + property.Name.ToUpperFirstChar();
            }
            return property.Name;
        }

        public static string GetFullType(this OdcmProperty property) 
        {
            if (property.IsCollection)
            {
                return  "NSMutableArray";
            }
            else
            {
                return property.Type.GetTypeString();
            }
		}

		public static string GetFullType(this OdcmType type)
		{
			return GetTypeString(type);
		}

		public static bool IsSystem(this OdcmProperty property)
		{
            return property.Type.IsSystem();
		}

		public static bool IsSystem(this OdcmType type)
		{
			string t = GetTypeString(type);
			return (t.Contains("int") || t == "BOOL" || t == "Byte" || t == "NSString" || t == "NSDate" || t == "NSStream" || t == "CGFloat");
		}

        public static bool IsDate(this OdcmProperty prop)
        {
            return prop.Type.IsDate();
        }

        public static bool IsDate(this OdcmType type)
        {
            string typeString = GetTypeString(type);
            return typeString.Equals("NSDate");
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

        public static string GetToUpperFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToUpperFirstChar();
        }

		public static bool IsEnum(this OdcmProperty property)
		{
			return property.Type is OdcmEnum;
		}
        public static string GetNSNumberValueMethod(this OdcmType type)
        {
            string objectiveCType = type.GetTypeString();
            if (objectiveCType.Equals("int32_t") || objectiveCType.Equals("int16_t"))
            {
                return "intValue";
            }
            if (objectiveCType.Equals("int64_t"))
            {
                return "longLongValue";
            }
            else if(objectiveCType.Equals("BOOL"))
            {
                return "boolValue";
            }
            else if (objectiveCType.Equals("CGFloat"))
            {
                return "floatValue";
            }
            else if (type is OdcmEnum)
            {
                return "intValue";
            }

            return null;
        }
	}
}
