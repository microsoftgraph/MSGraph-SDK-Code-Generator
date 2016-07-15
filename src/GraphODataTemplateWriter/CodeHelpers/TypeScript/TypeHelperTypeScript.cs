// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.TypeScript
{
    using Vipr.Core.CodeModel;
    using System;

    public static class TypeHelperTypeScript
    {
        
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
                case "DateTimeOffset":
                    typeStr = "Date";
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
        
    }
}