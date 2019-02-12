// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.TypeScript
{
    using Vipr.Core.CodeModel;
    using System;
    using System.Linq;

    public static class TypeHelperTypeScript
    {

        // enum value string, ex: "low" | "normal" | "high"
        public static String GetEnumValues(this OdcmEnum _enum) {
            return _enum.Members.Select(m => "\"" + m.Name + "\"").Aggregate((cur, next) =>  cur + " | " + next);
        }


        public static string GetTypeString(this OdcmProperty prop)
        {
            string typeStr = prop.Type.Name.UpperCaseFirstChar();

            switch (typeStr)
            {
                case "Stream":
                case "Json":
                    typeStr = "any";
                    break;
                case "Int16":
                case "Int32":
                case "Int64":
                case "Double":
                case "Single":
                case "Binary": // let binary: number = 0b1010;
                    typeStr = "number";
                    break;
                case "Guid":
                case "Duration":
                case "String":
                    typeStr = "string"; //lowercase
                    break;
                // all dates need to be of type string so they can be wrapped in new Date(___)
                case "DateTimeOffset": // ISO 8601 format in UTC time, ex 2014-01-01T00:00:00Z
                case "Date":
                case "TimeOfDay":
                    typeStr = "string";
                    break;
                case "Boolean":
                    typeStr = "boolean";
                    break;
                case "Byte": //https://graph.microsoft.io/en-us/docs/api-reference/beta/resources/intune_onboarding_rgbcolor
                    typeStr = "number";
                    break;

            }
            return (prop.IsCollection) ? typeStr + "[]" : typeStr;
            
        }
        public static String UpperCaseFirstChar(this String s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
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
    }
}
