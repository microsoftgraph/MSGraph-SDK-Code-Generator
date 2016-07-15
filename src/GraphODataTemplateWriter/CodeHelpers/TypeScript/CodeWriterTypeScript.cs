// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.TypeScript
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            }
            return type;
        }

        public String FullTypeName(OdcmProperty prop)
        {
            var Name = prop.Projection.Type.Name;
            Name = ConvertToJSTypes(Name);

            return (prop.IsCollection) ? "[" + Name + "]" : Name;
        }
    }

}
