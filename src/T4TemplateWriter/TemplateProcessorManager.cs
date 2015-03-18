using System;
using System.Collections.Generic;
using System.Linq;
using T4TemplateWriter.Output;
using T4TemplateWriter.Settings;
using T4TemplateWriter.Strategies;
using T4TemplateWriter.Templates;
using TemplateWriter.Strategies;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter
{
    public class TemplateProcessorManager : IConfigurable, IOdcmWriter
    {
        private readonly ITemplateTempLocationFileWriter _tempLocationFileWriter;
        private readonly Dictionary<string, Func<OdcmModel, TemplateWriterSettings, string /* path to base template */, ITemplateProcessor>> _processors;

        public TemplateProcessorManager()
            : this(new TemplateTempLocationFileWriter(new TemplateSourceReader()))
        {
        }

        public TemplateProcessorManager(ITemplateTempLocationFileWriter tempLocationFileWriter)
        {
            _tempLocationFileWriter = tempLocationFileWriter;
            _processors = new Dictionary<string, Func<OdcmModel, TemplateWriterSettings, string, ITemplateProcessor>>
            {
                {"java", (model, config, baseFilePath) => 
                    new JavaTemplateProcessor(new JavaFileWriter(model, config), model, baseFilePath)},
                {"objectivec", (model, config ,baseFilePath) =>
		 			new ObjectiveCTemplateProcessor(new ObjectiveCFileWriter(model, config), model, baseFilePath )}
            };
        }

        public void SetConfigurationProvider(IConfigurationProvider configurationProvider)
        {
            ConfigurationService.Initialize(configurationProvider);
        }

        public TextFileCollection GenerateProxy(OdcmModel model)
        {

            // TODO: Collect output into TextFileCollection
            var fileCollection = new TextFileCollection();

            var runnableTemplates = _tempLocationFileWriter.WriteUsing(typeof(CustomHost), ConfigurationService.Settings)
                                               .Where(x => !x.IsBase &&
                                                            x.IsForLanguage(ConfigurationService.Settings.TargetLanguage));

            var baseTemplate = _tempLocationFileWriter.WriteUsing(typeof(CustomHost), ConfigurationService.Settings)
                                                           .Single(x => x.IsBase && x.IsForLanguage(ConfigurationService.Settings.TargetLanguage));

            var processor = _processors[ConfigurationService.Settings.TargetLanguage]
                                .Invoke(model, ConfigurationService.Settings, baseTemplate.Path);

            foreach (var template in runnableTemplates)
            {
                Action<Template> action;
                if (processor.Templates.TryGetValue(template.Name, out action))
                {
                    action(template);
                }
            }


            return fileCollection;
        }
    }
}