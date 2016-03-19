// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Android
{
    using System;
    using Vipr.Core.CodeModel;

    public class CodeWriterAndroid : CodeWriterBase
    {
        public CodeWriterAndroid() : base() { }

        public CodeWriterAndroid(OdcmModel model) : base(model) { }

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
