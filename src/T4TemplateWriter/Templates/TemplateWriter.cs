// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Vipr.T4TemplateWriter.Output;
using Vipr.T4TemplateWriter.Settings;
using Vipr.T4TemplateWriter.TemplateProcessors;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter
{
    public class TemplateWriter : IConfigurable, IOdcmWriter
    {
        private String TemplateSourcePath { get; set; }
        private readonly Dictionary<String, Func<OdcmModel, String /* path to base template */, ITemplateProcessor>> _processors;

        public TemplateWriter() : this(Path.Combine(Directory.GetCurrentDirectory(), "T4")) { }

        public TemplateWriter(String templateSourcePath) {
            this.TemplateSourcePath = templateSourcePath;

            _processors = new Dictionary<String, Func<OdcmModel, String, ITemplateProcessor>>
            {
                {"Java",  (model, basePath) => new TemplateProcessor(new JavaPathWriter(), model, basePath)},
                {"Obj-C", (model, basePath) => new TemplateProcessor(new ObjectiveCPathWriter(), model, basePath )}
            };
        }


        IEnumerable<TextFile> ProcessTemplates(OdcmModel model)
        {
            IEnumerable<TemplateFileInfo> allTemplates = Utilities.ReadTemplateFiles(this.TemplateSourcePath);
                
            IEnumerable<TemplateFileInfo> runnableTemplates = 
                allTemplates.Where(templateInfo => templateInfo.TemplateType != TemplateType.Base);

            TemplateFileInfo baseTemplate = 
                allTemplates.Single(templateInfo => templateInfo.TemplateType == TemplateType.Base);

            var processor = _processors[ConfigurationService.Settings.TargetLanguage]
                                .Invoke(model, baseTemplate.FullPath);

            foreach (TemplateFileInfo runnableTemplate in runnableTemplates) {
                foreach (TextFile outputFile in processor.Process(runnableTemplate)) {
                    yield return outputFile;
                }
            }
        }

        public void SetConfigurationProvider(IConfigurationProvider configurationProvider) {
            ConfigurationService.Initialize(configurationProvider);
        }

        public IEnumerable<TextFile> GenerateProxy(OdcmModel model)
        {
            return this.ProcessTemplates(model);
        }
    }
}
