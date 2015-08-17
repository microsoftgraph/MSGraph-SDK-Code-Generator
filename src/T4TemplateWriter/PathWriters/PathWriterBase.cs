// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.IO;
using Vipr.T4TemplateWriter.Settings;
using Vipr.T4TemplateWriter.TemplateProcessor;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.Output
{
    public class PathWriterBase : IPathWriter
    {
        public OdcmModel Model { get; set; }

        public PathWriterBase(OdcmModel model)
        {
            Model = model;
        }

        public PathWriterBase()
            : this(null)
        {

        }

        protected virtual string TransformFileName(ITemplateInfo template, string baseFileName)
        {
            return String.Format("{0}.{1}", baseFileName, template.FileExtension);
        }

        public virtual string WritePath(ITemplateInfo template, string baseFileName)
        {
            return Path.Combine(
                template.TemplateLanguage,
                template.OutputParentDirectory,
                this.TransformFileName(template, baseFileName)
            );
        }

        public void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
        }

        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
    }
}
