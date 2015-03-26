// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.Collections.Generic;
using T4TemplateWriter.Settings;

namespace T4TemplateWriter.Templates
{
    public interface ITemplateTempLocationFileWriter
    {
        IList<Template> WriteUsing(Type sourceType, TemplateWriterSettings config);
    }
}
