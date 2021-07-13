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

        public string JsonContentType = "CoreConstants.MimeTypeNames.Application.Json";

        public string StreamContentType = "CoreConstants.MimeTypeNames.Application.Stream";

        public string GetMethod = "HttpMethods.GET";

        public string PostMethod = "HttpMethods.POST";

        public string PatchMethod = "HttpMethods.PATCH";

        public string PutMethod = "HttpMethods.PUT";

        public string DeleteMethod = "HttpMethods.DELETE";
    }
}
