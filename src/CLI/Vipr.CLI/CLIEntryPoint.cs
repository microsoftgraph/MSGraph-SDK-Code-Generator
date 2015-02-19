using System;
using System.IO;
using Vipr.CLI.Configuration;

namespace Vipr.CLI
{
    public class CLIEntryPoint
    {
        private readonly IConfigurationBuilder _configBuilder;
        private readonly ITemplateProcessorManager _processor;
        public CLIEntryPoint(IConfigurationBuilder configBuilder, ITemplateProcessorManager processor)
        {
            _configBuilder = configBuilder;
            _processor = processor;
        }

        public void DisplayOptions(TextWriter writer)
        {
            if (_configBuilder.OptionSet == null)
                _configBuilder.Build();

            _configBuilder.OptionSet.WriteOptionDescriptions(writer);
        }

        public void Process()
        {
            var configuration = _configBuilder.Build();
            if (configuration == null)
                throw new InvalidOperationException("Unable to build configuration");

            _processor.Process(configuration);
        }
    }
}