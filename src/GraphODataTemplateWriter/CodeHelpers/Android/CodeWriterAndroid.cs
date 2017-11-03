// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Android
{
    using System;
    using Vipr.Core.CodeModel;
    using CodeHelpers.Java;

    public class CodeWriterAndroid : CodeWriterJava
    {
        public CodeWriterAndroid() : base() { }

        public CodeWriterAndroid(OdcmModel model) : base(model) { }
    }
}
