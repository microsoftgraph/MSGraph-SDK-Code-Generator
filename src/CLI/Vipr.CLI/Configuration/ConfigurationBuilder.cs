using System.Diagnostics;
using Mono.Options;
using TemplateWriter;

namespace Vipr.CLI.Configuration
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private ITemplateConfiguration _configuration;
        private readonly BuilderArguments _arguments;

        public OptionSet OptionSet { get; private set; }

        public ConfigurationBuilder()
        {
            _arguments = new BuilderArguments();
        }

        public void CreateOptionSet(params string[] args)
        {
            if (_configuration == null)
            {
                //TODO: Load default config? fallback to default settings?
            }

            Debug.Assert(_configuration != null, "_configuration != null");
            OptionSet = new OptionSet
            {
                {"h|help", "Shows help", v => _arguments.ShowHelp = v != null},
                {
                    "lang|language=", string.Format("Language to generate(required) :{0}", _configuration.Languages),
                    v => _arguments.Language = v
                },
                {"in|inputFile=", "OData Metadata file", v => _arguments.InputFile = v},
                {"out|outputDir=", "Directory to save the generated files(required).", v => _arguments.OutputDir = v},
                {"p|plugins=", "Diferents configurations.(optional)", v => _arguments.Plugins = v.Split(',')},
            };
            OptionSet.Parse(args);
        }

        public IConfigurationBuilder WithArguments(params string[] args)
        {
            CreateOptionSet(args);
            return this;
        }

        public IConfigurationBuilder WithJsonConfig()
        {
            return this;
        }

        public IConfigurationBuilder WithDefaultConfig()
        {
            return this;
        }

        public IConfigurationBuilder WithSpecificConfiguration(ITemplateConfiguration configuration)
        {
            _configuration = configuration;
            return this;
        }

        public IConfigArguments Build()
        {
            var configArguments = new ConfigArguments
            {
                BuilderArguments = _arguments,
                TemplateConfiguration = _configuration //TODO: Static Config
            };
            return configArguments;
        }
    }
}