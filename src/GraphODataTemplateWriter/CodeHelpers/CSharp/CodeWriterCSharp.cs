// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp
{
    using System;
    using Vipr.Core.CodeModel;

    public class CodeWriterCSharp : CodeWriterBase
    {
        public CodeWriterCSharp(OdcmModel model) : base(model) { }

        public override String WriteOpeningCommentLine()
        {
            return "// ------------------------------------------------------------------------------" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine()
        {
            return "// ------------------------------------------------------------------------------";
        }

        public override string WriteInlineCommentChar()
        {
            return "//  ";
        }

        public string JsonContentType = "Constants.ContentTypes.JsonContentType";

        public string StreamContentType = "Constants.ContentTypes.StreamContentType";

        public string GetMethod = "Constants.HttpMethods.GET.ToString()";

        public string PostMethod = "Constants.HttpMethods.POST.ToString()";

        public string PatchMethod = "Constants.HttpMethods.PATCH.ToString()";

        public string PutMethod = "Constants.HttpMethods.PUT.ToString()";

        public string DeleteMethod = "Constants.HttpMethods.DELETE.ToString()";
    }
}
