// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information. 

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Vipr.Core;
    using Vipr.Core.CodeModel;
    using Vipr.T4TemplateWriter.Output;
    using Vipr.T4TemplateWriter.Settings;

    public class TemplateWriter : IConfigurable, IOdcmWriter
    {
        private String TemplatesDirectory { get; set;}
        private ITemplateProcessor Processor { get; set; }
        private OdcmModel CurrentModel { get; set; }
        private ITemplateInfoProvider TemplateInfoProvider { get; set; }

        private const String PathWriterClassNameFormatString = "Vipr.T4TemplateWriter.Output.{0}PathWriter";

        private void SetTemplatesDirectory(string templatesDirectory, bool relative = true)
        {
            string programDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string relativeTemplatesDirectory = templatesDirectory ?? "Templates";

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
            var templates = this.TemplateInfoProvider.Templates().Where(templateInfo => templateInfo.TemplateType != Template.Shared);

            // Initialize processor.
            string pathWriterClassName = String.Format(PathWriterClassNameFormatString, ConfigurationService.Settings.TargetLanguage);
            var type = Type.GetType(pathWriterClassName);
            IPathWriter pathWriterInstance = (IPathWriter)Activator.CreateInstance(type);
            this.Processor = new TemplateProcessor(pathWriterInstance, this.CurrentModel, this.TemplatesDirectory);

            foreach (ITemplateInfo template in templates)
            {
                foreach (TextFile outputFile in this.Processor.Process(template))
                {
                    yield return outputFile;
                }
            }

            Console.WriteLine("Done processing templates");
            Console.WriteLine();
            Console.WriteLine(this.Processor.GetProcessorVerboseOutput());
        }

        // IConfigurationProvider
        public void SetConfigurationProvider(IConfigurationProvider configurationProvider)
        {
            ConfigurationService.Initialize(configurationProvider);
            FileNameCasing nameCasing;
            if(!Enum.TryParse(ConfigurationService.Settings.DefaultFileCasing, out nameCasing))
            {
                nameCasing = FileNameCasing.UpperCamel;
            }
            SetTemplatesDirectory(ConfigurationService.Settings.TemplatesDirectory);
            this.TemplateInfoProvider = new TemplateInfoProvider(ConfigurationService.Settings.TemplateConfiguration,
                                                                 Path.Combine(TemplatesDirectory, ConfigurationService.Settings.TargetLanguage),
                                                                defaultNameCasing: nameCasing);
        }

        // IOdcmWriter
        public IEnumerable<TextFile> GenerateProxy(OdcmModel model)
        {
            this.CurrentModel = model;
            return this.ProcessTemplates();
        }
    }
}
