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
            return "//" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine()
        {
            return "//" + this.NewLineCharacter;
        }

        public override string WriteInlineCommentChar()
        {
            return "// ";
        }

        public override String NewLineCharacter
        {
            get { return "\n"; }
        }
        public String UpperCaseFirstChar(String s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

    }

}
