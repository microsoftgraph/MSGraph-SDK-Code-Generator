// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using Vipr.T4TemplateWriter.TemplateProcessor;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.Output
{
    public interface IPathWriter
    {
        OdcmModel Model { get; set; }
        string WritePath(TemplateFileInfo template, String entityTypeName);
    }
}
