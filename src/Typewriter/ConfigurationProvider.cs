using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;
using Vipr.Core;

namespace Typewriter
{
    public class ConfigurationProvider : Vipr.Core.IConfigurationProvider
    {
        private readonly IConfiguration Configuration;
        private const string configFolder = ".config";
        public ConfigurationProvider()
        {
            var builder = new ConfigurationBuilder();
            if (!string.IsNullOrWhiteSpace(Environment.CurrentDirectory))
            {
                builder.AddJsonFile(Path.Combine(Environment.CurrentDirectory, configFolder, "TemplateWriterSettings.json"), true);
                builder.AddJsonFile(Path.Combine(Environment.CurrentDirectory, configFolder, "GraphEndpointListSettings.json"), true);
            }

            Configuration = builder.AddEnvironmentVariables().Build();
        }
        public ConfigurationProvider(string targetLanguage)
        {
            var builder = new ConfigurationBuilder();
            if (!string.IsNullOrWhiteSpace(Environment.CurrentDirectory))
            {
                builder.AddJsonFile(Path.Combine(Environment.CurrentDirectory, configFolder, "TemplateWriterSettings.json"), true);
                builder.AddJsonFile(Path.Combine(Environment.CurrentDirectory, configFolder, $"{targetLanguage}Settings.json"), false);
            }

            Configuration = builder.AddEnvironmentVariables().Build();
        }

        public T GetConfiguration<T>()
        {
            return Configuration.Get<T>();
        }
        public static void SetConfigurationOn(object target)
        {
            var configurable = target as IConfigurable;

            if (configurable != null)
            {
                configurable.SetConfigurationProvider(new ConfigurationProvider());
            }
        }
    }

}
