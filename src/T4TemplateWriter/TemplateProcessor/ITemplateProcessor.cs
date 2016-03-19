// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    using System;
    using System.Collections.Generic;
    using Vipr.T4TemplateWriter;
    using Vipr.Core;

    interface ITemplateProcessor
    {
        IEnumerable<TextFile> Process(ITemplateInfo template);

        string GetProcessorVerboseOutput();
    }
}
