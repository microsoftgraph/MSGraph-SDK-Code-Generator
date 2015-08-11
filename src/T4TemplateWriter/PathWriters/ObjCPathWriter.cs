// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.IO;
using Vipr.T4TemplateWriter.Settings;
using Vipr.T4TemplateWriter.TemplateProcessor;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.Output
{
    class ObjCPathWriter : PathWriterBase
    {

        public override string WritePath(ITemplateInfo template, String entityTypeName)
        {
            String prefix = ConfigurationService.Settings.NamespacePrefix;
            String coreFileName = this.TransformFileName(template, entityTypeName);
            String extension = template.FileExtension;

            return Path.Combine(
                template.TemplateDirectoryName, 
                String.Format("{0}{1}",
                    prefix,
                    coreFileName
                )
            );
        }

    }
}
