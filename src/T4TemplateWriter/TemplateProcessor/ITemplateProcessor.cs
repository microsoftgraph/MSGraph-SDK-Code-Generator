// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.TemplateProcessor
{
    using System.Collections.Generic;
    using Vipr.Core;

    interface ITemplateProcessor
    {
        IEnumerable<TextFile> Process(ITemplateInfo template);

        string GetProcessorVerboseOutput();
    }
}
