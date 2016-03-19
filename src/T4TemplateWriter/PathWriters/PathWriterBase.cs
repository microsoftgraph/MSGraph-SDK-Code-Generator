// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Vipr.T4TemplateWriter.Output
{
    using System;
    using System.IO;
    using Vipr.Core.CodeModel;
    using Vipr.T4TemplateWriter.TemplateProcessor;

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
