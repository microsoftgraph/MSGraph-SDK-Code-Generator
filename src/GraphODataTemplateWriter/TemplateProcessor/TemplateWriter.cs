// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.TemplateProcessor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Graph.ODataTemplateWriter.PathWriters;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor.Enums;
    using Vipr.Core;
    using Vipr.Core.CodeModel;
    using NLog;

    public class TemplateWriter : IConfigurable, IOdcmWriter
    {
        private String TemplatesDirectory { get; set;}
        private ITemplateProcessor Processor { get; set; }
        private OdcmModel CurrentModel { get; set; }
        public ITemplateInfoProvider TemplateInfoProvider { get; set; }

        private static Logger logger = LogManager.GetCurrentClassLogger();

        private string pathWriterClassNameFormatString;
        
        private string targetLanguage;
        private IEnumerable<string> properties;

        public TemplateWriter()
        {
        }

        public TemplateWriter(string targetLanguage)  
        {
            this.targetLanguage = targetLanguage;
        }

        public TemplateWriter(string targetLanguage, IEnumerable<string> properties)
        {
            this.targetLanguage = targetLanguage;
            this.properties = properties;
        }

        private string PathWriterClassNameFormatString
        {
            get {
                return this.pathWriterClassNameFormatString ??
                       (this.pathWriterClassNameFormatString =
                           typeof(CSharpPathWriter).FullName.Replace("CSharp", "{0}"));
            }
        }

        private void SetTemplatesDirectory(string templatesDirectory, bool relative = true)
        {
            string programDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            this.TemplatesDirectory = (relative) ? Path.Combine(programDir, templatesDirectory) : templatesDirectory;

            if (!new DirectoryInfo(this.TemplatesDirectory).Exists)
            {
                throw new DirectoryNotFoundException(string.Format("Could not find : {0}", this.TemplatesDirectory));
            }
        }

        IEnumerable<TextFile> ProcessTemplates()
        {
            // There is no need to process "shared" tempaltes, they are only meant to be imported from other templates.
            var templates = this.TemplateInfoProvider.Templates().Where(templateInfo => templateInfo.TemplateType != Template.Shared);

            logger.Debug($"Processing {templates.Count()} templates.");

            // Initialize processor.
            var pathWriterClassName = String.Format(this.PathWriterClassNameFormatString, ConfigurationService.Settings.TargetLanguage);

            logger.Debug($"Using writer {pathWriterClassName}.");

            var type = Type.GetType(pathWriterClassName);
            if (type == null)
            {
                throw new InvalidOperationException("Unable to find path writer " + pathWriterClassName);
            }

            IPathWriter pathWriterInstance = (IPathWriter)Activator.CreateInstance(type);
            this.Processor = new TemplateProcessor(pathWriterInstance, this.CurrentModel, this.TemplatesDirectory);

            foreach (ITemplateInfo template in templates)
            {
                foreach (TextFile outputFile in this.Processor.Process(template))
                {
                    logger.Debug("Created file {0}", outputFile.RelativePath);
                    yield return outputFile;
                }
            }
        }

        // IConfigurationProvider
        public void SetConfigurationProvider(IConfigurationProvider configurationProvider)
        {
            ConfigurationService.Initialize(configurationProvider, this.targetLanguage, this.properties);
            FileNameCasing nameCasing;
            if(!Enum.TryParse(ConfigurationService.Settings.DefaultFileCasing, out nameCasing))
            {
                nameCasing = FileNameCasing.UpperCamel;
            }
            if (Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates"))) {
                this.SetTemplatesDirectory("Templates", true);
            } else {
                this.SetTemplatesDirectory(ConfigurationService.Settings.TemplatesDirectory);
            }
            this.TemplateInfoProvider = new TemplateInfoProvider(ConfigurationService.Settings.TemplateConfiguration,
                                                                 Path.Combine(this.TemplatesDirectory, ConfigurationService.Settings.TargetLanguage),
                                                                defaultNameCasing: nameCasing);
        }

        // IOdcmWriter
        public IEnumerable<TextFile> GenerateProxy(OdcmModel model)
        {
            this.CurrentModel = model;
            logger.Debug("Currently processing model {0}", model);
            return this.ProcessTemplates();
        }
    }
}
