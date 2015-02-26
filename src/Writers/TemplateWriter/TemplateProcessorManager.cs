using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ODataReader.v4;
using TemplateWriter.Output;
using TemplateWriter.Strategies;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace TemplateWriter
{
    public class TemplateProcessorManager : ITemplateProcessorManager
    {
        private readonly IOdcmReader _reader;
        private readonly ITemplateTempLocationFileWriter _tempLocationFileWriter;
        private readonly Dictionary<string, Func<OdcmModel, IConfigArguments, string, ITemplateProcessor>> _processors;

        public TemplateProcessorManager()
            : this(new OdcmReader(), new TemplateTempLocationFileWriter(new TemplateSourceReader()))
        {
        }

        public TemplateProcessorManager(IOdcmReader reader, ITemplateTempLocationFileWriter tempLocationFileWriter)
        {
            _reader = reader;
            _tempLocationFileWriter = tempLocationFileWriter;
            _processors = new Dictionary<string, Func<OdcmModel, IConfigArguments, string, ITemplateProcessor>>
            {
                {"java", (model, configArguments, baseFilePath) => 
                    new JavaTemplateProcessor(new JavaFileWriter(model, configArguments), model,baseFilePath)},
                {"objectivec", (model, configArguments,baseFilePath) =>
					new ObjectiveCTemplateProcessor(new ObjectiveCFileWriter(model, configArguments), model, baseFilePath )}
            };
        }

        public void Process(IConfigArguments configuration)
        {
            var runnableTemplates = _tempLocationFileWriter.WriteUsing(typeof(CustomHost), configuration.BuilderArguments)
                                                           .Where(x => !x.IsBase &&
                                                                        x.IsForLanguage(configuration.BuilderArguments.Language));

            var baseTemplate = _tempLocationFileWriter.WriteUsing(typeof(CustomHost), configuration.BuilderArguments)
                                                           .Single(x => x.IsBase && x.IsForLanguage(configuration.BuilderArguments.Language));

            var model = _reader.GenerateOdcmModel(new Dictionary<string, string>
            {
                { "$metadata", File.ReadAllText(configuration.BuilderArguments.InputFile) }
            });

            var processor = _processors[configuration.BuilderArguments.Language]
                                .Invoke(model, configuration, baseTemplate.Path);

            foreach (var template in runnableTemplates)
            {
                Action<Template> action;
                if (processor.Templates.TryGetValue(template.Name, out action))
                {
                    action(template);
                }
            }
        }
    }
}