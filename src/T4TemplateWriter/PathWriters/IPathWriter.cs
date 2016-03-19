// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Vipr.T4TemplateWriter.Output
{
    using Vipr.Core.CodeModel;
    using Vipr.T4TemplateWriter.TemplateProcessor;

    public interface IPathWriter
    {
        OdcmModel Model { get; set; }
        string WritePath(ITemplateInfo template, string entityTypeName);
    }
}
