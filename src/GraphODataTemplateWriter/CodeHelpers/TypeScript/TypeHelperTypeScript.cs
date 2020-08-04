// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.TypeScript
{
    using Vipr.Core.CodeModel;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;

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
                case "Decimal":
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
                return description.Replace("<", "&lt;")
                                  .Replace(">", "&gt;")
                                  .Replace("&", "&amp;")
                                  .Replace("\r\n", "\r\n///"); // &#xD;&#xA; The HTML encoded has already been converted to escaped chars.
            }
            return null;
        }

        /// <summary>
        /// Converts an OdcmModel into printable TypeScript namespaces
        /// </summary>
        /// <param name="model">Odcm model</param>
        /// <returns>Main and Subnamespaces</returns>
        public static TypeScriptNamespaces GetTypeScriptNamespaces(this OdcmModel model)
        {
            TypeScriptNamespace mainNamespace = null;
            var subNamespaces = new Dictionary<string, TypeScriptNamespace>();

            foreach(var odcmNamespace in model.GetOdcmNamespaces())
            {
                var typeScriptNamespace = new TypeScriptNamespace(odcmNamespace);
                if (typeScriptNamespace.IsMainNamespace)
                {
                    mainNamespace = typeScriptNamespace;
                }
                else
                {
                    subNamespaces[typeScriptNamespace.Name] = typeScriptNamespace;
                }
            }

            return new TypeScriptNamespaces()
            {
                MainNamespace = mainNamespace,
                SubNamespaces = subNamespaces
            };
        }
    }
}
