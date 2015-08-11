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

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    public class TemplateWriter : IConfigurable, IOdcmWriter
    {
        private String TemplatesDirectory {get; set;}
        private ITemplateProcessor Processor { get; set; }
        private OdcmModel CurrentModel { get; set; }
        private ITemplateConfiguration TemplateConfiguration { get; set; }

        private const String PathWriterClassNameFormatString = "Vipr.T4TemplateWriter.Output.{0}PathWriter";

        private void SetTemplatesDirectory(string templatesDirectory, bool relative = true)
        {
            String programDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            String relativeTemplatesDirectory = templatesDirectory ?? "Templates";

            this.TemplatesDirectory = (relative) ? Path.Combine(programDir, templatesDirectory) : templatesDirectory;

            if (!new DirectoryInfo(this.TemplatesDirectory).Exists)
            {
                throw new DirectoryNotFoundException(string.Format("Could not find : {0}", this.TemplatesDirectory));
            }

            Console.WriteLine("Using templates from {0}", TemplatesDirectory);
        }

        IEnumerable<TextFile> ProcessTemplates()
        {
            // There is no need to process "shared" tempaltes, they are only meant to be imported from other templates.
            var templates = Utilities.ReadTemplateFiles(this.TemplatesDirectory, this.TemplateConfiguration)
                            .Where(templateInfo => templateInfo.TemplateType != TemplateType.Shared);

            // Initialize processor.
            String pathWriterClassName = String.Format(PathWriterClassNameFormatString, ConfigurationService.Settings.TargetLanguage);
            var type = Type.GetType(pathWriterClassName);
            IPathWriter pathWriterInstance = (IPathWriter)Activator.CreateInstance(type);
            this.Processor = new TemplateProcessor(pathWriterInstance, this.CurrentModel, this.TemplatesDirectory, this.TemplateConfiguration);

            foreach (TemplateFileInfo template in templates)
            {
                foreach (TextFile outputFile in this.Processor.Process(template))
                {
                    yield return outputFile;
                }
            }
        }

        // IConfigurationProvider
        public void SetConfigurationProvider(IConfigurationProvider configurationProvider)
        {
            ConfigurationService.Initialize(configurationProvider);
            this.TemplateConfiguration = new TemplateConfiguration(ConfigurationService.Settings.TemplateConfiguration);
            SetTemplatesDirectory(ConfigurationService.Settings.TemplatesDirectory);
        }

        // IOdcmWriter
        public IEnumerable<TextFile> GenerateProxy(OdcmModel model)
        {
            this.CurrentModel = model;
            return this.ProcessTemplates();
        }
    }
}
