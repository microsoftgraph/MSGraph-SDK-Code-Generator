// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.TypeScript
{
    using Vipr.Core.CodeModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TypeHelperTypeScript
    {

        // enum value string, ex: "low" | "normal" | "high"
        public static String GetEnumValues(this OdcmEnum _enum) {
            String enumVals = null;
            foreach (var member in _enum.Members)
            {
                if (enumVals == null)
                {
                    enumVals = '"' + member.Name + '"';
                } else
                {
                    enumVals += " | " + '"' + member.Name + '"';
                }
            }

            return enumVals;
        }


        public static string GetTypeString(this OdcmProperty prop)
        {
            string typeStr = UpperCaseFirstChar(prop.Type.Name);

            switch (typeStr)
            {
                case "Stream":
                    typeStr = "any";
                    break;
                case "Int16":
                case "Int32":
                case "Int64":
                case "Double":
                case "Binary": // let binary: number = 0b1010;
                    typeStr = "number";
                    break;
                case "Guid":
                case "String":
                    typeStr = "string"; //lowercase
                    break;
                // all dates need to be of type string so they can be wrapped in new Date(___)
                case "DateTimeOffset": // ISO 8601 format in UTC time, ex 2014-01-01T00:00:00Z
                case "Date":
                    typeStr = "string";
                    break;
                case "Boolean":
                    typeStr = "boolean";
                    break;

            }
            return (prop.IsCollection) ? "[" + typeStr + "]" : typeStr;
            
        }
        public static String UpperCaseFirstChar(String s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static OdcmClass FindEntityByName(this IEnumerable<OdcmClass> entities, String name)
        {
            foreach (var entity in entities)
            {
                if (entity.Name.Equals(name))
                {
                    return entity;
                }
            }
            return null;
        }

        public static List<string> GetNavigationPaths(this OdcmProperty prop, string baseString, IEnumerable<OdcmClass> entities, int depth)
        {
            List<String> paths = new List<string>();
            if (depth > 5) return paths;
            OdcmClass entity = entities.FindEntityByName(prop.Type.Name);

            string newBase;
            if (prop.IsCollection)
            {
                newBase = baseString + "/" + prop.Name + "/{" + prop.Type.Name + "Id}";
                paths.Add(baseString + "/" + prop.Name);
            }
            else
            {
                newBase = baseString + "/" + prop.Name;
         
            }
            paths.Add(newBase);
            
            foreach (var eprop in entity.Properties)
            {
                if (eprop.IsLink)
                {
                    paths.AddRange(GetNavigationPaths(eprop, newBase, entities, depth + 1));
                }
            }
            return paths;
        }
        
    }
}