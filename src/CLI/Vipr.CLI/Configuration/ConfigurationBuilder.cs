using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Mono.Options;
using Newtonsoft.Json;
using TemplateWriter;

namespace Vipr.CLI.Configuration
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private const string StaticConfigFile = "config.json";  

        private ITemplateConfiguration _configuration;
        private readonly BuilderArguments _arguments;

        public OptionSet OptionSet { get; private set; }

        public ConfigurationBuilder()
        {
            _arguments = new BuilderArguments();
        }

        public IConfigurationBuilder WithArguments(params string[] args)
        {
            CreateOptionSet(args);
            return this;
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

        public IConfigurationBuilder WithConfiguration(ITemplateConfiguration configuration)
        {
            ConfigurationService.Initialize(configuration);
            _configuration = ConfigurationService.Configuration;
            return this;
        }

        public IConfigurationBuilder WithJsonConfig()
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var fullPath = string.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, StaticConfigFile);
                var content = File.ReadAllText(fullPath);
                var configuration = JsonConvert.DeserializeObject<StaticConfiguration>(content);

                ConfigurationService.Initialize(configuration);
                _configuration = ConfigurationService.Configuration;

                return this;
            }
            catch (Exception ex)
            {
                var throwableEx = new InvalidOperationException(string.Format("Cannot load static configuration file '{0}'", StaticConfigFile), ex);
                throw throwableEx;
            }
        }

        public IConfigurationBuilder WithDefaultConfig()
        {
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