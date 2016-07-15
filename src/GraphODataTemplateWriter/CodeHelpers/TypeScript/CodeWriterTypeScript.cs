// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.TypeScript
{
    using System;
    using Vipr.Core.CodeModel;

    public class CodeWriterTypeScript : CodeWriterBase
    {
        public CodeWriterTypeScript() : base() { }

        public CodeWriterTypeScript(OdcmModel model) : base(model) { }

        public override String WriteOpeningCommentLine()
        {
            return "/****************************************************************************" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine()
        {
            return "*****************************************************************************/" + this.NewLineCharacter;
        }

        public override string WriteInlineCommentChar()
        {
            return "// ";
        }

        public override String NewLineCharacter
        {
            get { return "\n"; }
        }
        
        public static string ConvertToJSTypes(string type)
        {
            switch (type)
            {
                case "Stream":
                    return "any";
                case "Int16":
                case "Int32":
                case "Int64":
                case "Double":
                case "Binary": // let binary: number = 0b1010;
                    return "Number";
                case "Guid":
                    return "String";
                case "DateTimeOffset":
                    return "Date";
                case "Boolean":
                    return "boolean";
            }
            return type;
        }

        public String UpperCaseFirstChar(String s)
        {
            return char.ToUpper(s[0]) + s.Substring(1); ;
        }

        public String FullTypeName(OdcmProperty prop)
        {
            var Name = prop.Projection.Type.Name;
            
            // capitalize the first letter
            Name = UpperCaseFirstChar(Name);
            
            // Needs to come after uppercase() because some native JS types need to be lowercase
            Name = ConvertToJSTypes(Name);

            return (prop.IsCollection) ? "[" + Name + "]" : Name;
        }
    }

}
