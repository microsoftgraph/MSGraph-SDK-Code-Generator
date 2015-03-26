# Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
# Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using T4TemplateWriter.Output;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Strategies
{
    public class JavaTemplateProcessor : BaseTemplateProcessor
    {
        public JavaTemplateProcessor(IPathWriter pathWriter, OdcmModel model, string baseFilePath) : base(pathWriter, model, baseFilePath)
        {
            StrategyName = "Java";
        }
    }
}
