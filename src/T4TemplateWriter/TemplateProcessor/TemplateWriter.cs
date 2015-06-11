// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vipr.T4TemplateWriter.Output;
using Vipr.T4TemplateWriter.Settings;
using Vipr.T4TemplateWriter.TemplateProcessor;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.TemplateProcessor {
    public class TemplateWriter : IConfigurable, IOdcmWriter {
        private String TemplatesDirectory { get; set; }
        private ITemplateProcessor Processor { get; set; }
        private OdcmModel CurrentModel { get; set; }

        private const String PathWriterClassNameFormatString = "Vipr.T4TemplateWriter.Output.{0}PathWriter";

        public TemplateWriter() : this(null) { }

        public TemplateWriter(String templatesDirectory) {
            if (String.IsNullOrEmpty(templatesDirectory)) {
                String programDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                String templatesDir = Path.Combine(programDir, "Templates");
                if (new DirectoryInfo(templatesDir).Exists) {
                    this.TemplatesDirectory = templatesDir;
                } else {
                    throw new Exception("No templates found.");
                }
            } else {
                this.TemplatesDirectory = templatesDirectory;
            }

            Console.WriteLine("Using templates from {0}", TemplatesDirectory);
        }


        IEnumerable<TextFile> ProcessTemplates() {
            IEnumerable<TemplateFileInfo> allTemplates = Utilities.ReadTemplateFiles(this.TemplatesDirectory);

            IEnumerable<TemplateFileInfo> runnableTemplates =
                allTemplates.Where(templateInfo => templateInfo.TemplateType != TemplateType.Base);

            // Initialize processor.
            String pathWriterClassName = String.Format(PathWriterClassNameFormatString,
                ConfigurationService.Settings.TargetLanguage);
            IPathWriter pathWriterInstance = (IPathWriter) Activator.CreateInstance(Type.GetType(pathWriterClassName));
            this.Processor = new TemplateProcessor(pathWriterInstance, this.CurrentModel, this.TemplatesDirectory);

            foreach (TemplateFileInfo runnableTemplate in runnableTemplates) {
                foreach (TextFile outputFile in this.Processor.Process(runnableTemplate)) {
                    yield return outputFile;
                }
            }
        }

        // IConfigurationProvider
        public void SetConfigurationProvider(IConfigurationProvider configurationProvider) {
            ConfigurationService.Initialize(configurationProvider);
        }

        // IOdcmWriter
        public IEnumerable<TextFile> GenerateProxy(OdcmModel model) {
            this.CurrentModel = model;
            return this.ProcessTemplates();
        }
    }
}
