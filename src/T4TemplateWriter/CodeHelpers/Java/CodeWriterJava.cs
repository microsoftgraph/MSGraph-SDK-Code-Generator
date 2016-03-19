// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Vipr.T4TemplateWriter.CodeHelpers.Java
{
    using System;
    using Vipr.Core.CodeModel;

    public class CodeWriterJava : CodeWriterBase
    {
        public CodeWriterJava() : base() { }

        public CodeWriterJava(OdcmModel model) : base(model) { }

        public override String WriteOpeningCommentLine()
        {
            return "// ------------------------------------------------------------------------------" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine()
        {
            return "// ------------------------------------------------------------------------------" + this.NewLineCharacter;
        }

        public override string WriteInlineCommentChar()
        {
            return "// ";
        }
    }
}
