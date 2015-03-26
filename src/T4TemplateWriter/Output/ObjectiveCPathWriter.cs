# Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
# Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System.IO;
using T4TemplateWriter.Settings;
using T4TemplateWriter.Templates;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Output
{
    class ObjectiveCPathWriter : BasePathWriter
    {
        public ObjectiveCPathWriter(OdcmModel model, TemplateWriterSettings configuration)
            : base(model, configuration)
        {
        }

        public new string FileExtension { get; set; }

        public override string WritePath(Template template, string fileName)
        {
            var identifier = FileName(template, fileName);
            FileExtension = template.ResourceName.Contains("header") ? ".h" : ".m";

            var filePath = Path.Combine(template.FolderName, string.Format("{0}{1}", identifier, FileExtension));
            return filePath;
        }

        protected override string FileName(Template template, string identifier)
        {
            string ret;

            if (template.Name.Contains("Entity") && (template.FolderName == "odata")) {
                ret = template.Name.Replace("Entity", identifier);
            } else {
                ret = identifier;
            }

            return ret;
        }
    }
}
