// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System.IO;
using T4TemplateWriter.Settings;
using T4TemplateWriter.Templates;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Output
{
    public class BasePathWriter : IPathWriter
    {
        protected readonly OdcmModel Model;
        protected readonly TemplateWriterSettings Configuration;

        public string FileExtension
        {
            get { return ".txt"; }
        }

        public BasePathWriter(OdcmModel model, TemplateWriterSettings configuration)
        {
            Model = model;
            Configuration = configuration;
        }

        protected virtual string FileName(Template template, string identifier)
        {
            return template.FolderName == "fetchers" ? template.Name.Replace("Entity", identifier)
                                                  : identifier;
        }

        public virtual string WritePath(Template template, string fileName)
        {
            // we no longer specify our own base path, only the relative path
            var identifier = FileName(template, fileName);
            var filePath = string.Format("{0}{1}", identifier, FileExtension);
            return filePath;
        }

        /// <summary>
        /// Creates a directory structure based on the given parameter
        /// </summary>
        /// <param name="directoryPath"></param>
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
