// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.IO;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{

    public class TemplateFileInfo : Vipr.T4TemplateWriter.TemplateProcessor.TemplateInfoBase
    {
        public override String Id { get { return this.FullPath; } }

        public override TemplateType TemplateType { get; set; }

        public override String TemplateName { get; set; }

        public override String TemplateLanguage { get; set; }

        public String TemplateBaseName { get; set; }

        public String FullPath { get; set; }

        public String FileExtension { get; set; }

        public TemplateFileInfo(String fullPath)
        {
            this.FullPath = fullPath;

            // <rootPath>/<grandparent>/<parent>/<fileName>.<fileExtension>.tt
            this.TemplateName = Path.GetFileNameWithoutExtension(fullPath);  // <fileName>.<fileExtension>
            this.FileExtension = Path.GetExtension(this.TemplateName).Substring(1);  // <fileExtension>

            this.TemplateBaseName = Path.GetFileNameWithoutExtension(this.TemplateName); // <fileName>

            String parentPath = Path.GetDirectoryName(fullPath);  // <rootPath>/<grandparent>/<parent>
            String parentName = Path.GetFileNameWithoutExtension(parentPath);  // <parent>

            String grandparentPath = Path.GetDirectoryName(parentPath);  // <rootPath>/<grandparent>
            String grandparentName = Path.GetFileNameWithoutExtension(grandparentPath);  // <grandparent>

            this.TemplateLanguage = grandparentName;

            TemplateType parsed;
            Boolean valid = Enum.TryParse(parentName, true, out parsed);

            if (valid)
            {
                this.TemplateType = parsed;
            }
            else
            {
                Console.WriteLine("Unknown template type. Recognized types are Base, Fetcher, Model, and Other.");
                this.TemplateType = TemplateType.Unknown;
            }

        }


    }
}
