// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.PathWriters
{
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
    using Vipr.Core.CodeModel;

    public interface IPathWriter
    {
        OdcmModel Model { get; set; }
        string WritePath(ITemplateInfo template, string entityTypeName);

        string WritePath(ITemplateInfo template, string @namespace, string entityTypeName);
    }
}
