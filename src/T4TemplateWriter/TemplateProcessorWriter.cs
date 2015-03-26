// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.Collections.Generic;
using System.Linq;
using T4TemplateWriter.Output;
using T4TemplateWriter.Settings;
using T4TemplateWriter.Strategies;
using T4TemplateWriter.Templates;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter
{
    public class TemplateProcessorWriter : IConfigurable, IOdcmWriter
    {
        private readonly ITemplateTempLocationFileWriter _tempLocationFileWriter;
        private readonly Dictionary<string, Func<OdcmModel, TemplateWriterSettings, string /* path to base template */, ITemplateProcessor>> _processors;

        public TemplateProcessorWriter()
            : this(new TemplateTempLocationFileWriter(new TemplateSourceReader()))
        {
        }

        public TemplateProcessorWriter(ITemplateTempLocationFileWriter tempLocationFileWriter)
        {
            _tempLocationFileWriter = tempLocationFileWriter;
            _processors = new Dictionary<string, Func<OdcmModel, TemplateWriterSettings, string, ITemplateProcessor>>
            {
                {"java", (model, config, baseFilePath) =>
                    new JavaTemplateProcessor(new JavaPathWriter(model, config), model, baseFilePath)},
                {"objectivec", (model, config ,baseFilePath) =>
		 			new ObjectiveCTemplateProcessor(new ObjectiveCPathWriter(model, config), model, baseFilePath )}
            };
        }

        public void SetConfigurationProvider(IConfigurationProvider configurationProvider)
        {
            ConfigurationService.Initialize(configurationProvider);
        }

        TextFileCollection ProcessTemplates(OdcmModel model)
        {
            var fileCollection = new TextFileCollection();
            var runnableTemplates = _tempLocationFileWriter.WriteUsing(typeof(CustomHost), ConfigurationService.Settings)
                                               .Where(x => !x.IsBase &&
                                                            x.IsForLanguage(ConfigurationService.Settings.TargetLanguage));

            var baseTemplate = _tempLocationFileWriter.WriteUsing(typeof(CustomHost), ConfigurationService.Settings)
                                                           .Single(x => x.IsBase && x.IsForLanguage(ConfigurationService.Settings.TargetLanguage));

            var processor = _processors[ConfigurationService.Settings.TargetLanguage]
                                .Invoke(model, ConfigurationService.Settings, baseTemplate.Path);

            var textFiles = runnableTemplates.SelectMany(template => processor.Process(template));
            fileCollection.AddRange(textFiles);
            return fileCollection;
        }

        public TextFileCollection GenerateProxy(OdcmModel model)
        {
            return ProcessTemplates(model);
        }
    }
}
