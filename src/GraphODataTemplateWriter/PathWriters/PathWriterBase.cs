// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.PathWriters
{
    using System;
    using System.IO;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
    using Vipr.Core.CodeModel;

    public class PathWriterBase : IPathWriter
    {
        public OdcmModel Model { get; set; }

        public PathWriterBase(OdcmModel model)
        {
            this.Model = model;
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

        public virtual string WritePath(ITemplateInfo template, string @namespace, string baseFileName)
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
