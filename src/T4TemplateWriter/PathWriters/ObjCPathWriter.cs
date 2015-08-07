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

        public override string WritePath(TemplateFileInfo template, String entityTypeName)
        {
            String prefix = ConfigurationService.Settings.NamespacePrefix;
            String coreFileName = this.TransformFileName(template, entityTypeName);
            String extension = template.FileExtension;

            String containerName = entityTypeName == this.Model.EntityContainer.Name ? String.Empty : this.Model.EntityContainer.Name;

            return Path.Combine(
                template.TemplateName, 
                String.Format("{0}{1}{2}",
                    prefix,
                    containerName,
                    coreFileName
                )
            );
        }

        protected override String TransformFileName(TemplateFileInfo template, String entityTypeName)
        {
            string result;

            if (template.TemplateName.Contains("Entity") && template.TemplateName.Contains("Fetcher"))
            {
                result = template.TemplateName.Replace("Entity", entityTypeName);
            } 
            else 
            {
                result = String.Format("{0}.{1}", entityTypeName, template.FileExtension);
            }

            return result;
        }
    }
}
