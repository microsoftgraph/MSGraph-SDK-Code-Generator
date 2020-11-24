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
        private const string dotNetCorePathprefix = "../GraphODataTemplateWriter";
        private const string configFolder = ".config";
        public ConfigurationProvider()
        {
            var builder = new ConfigurationBuilder();
            if (!string.IsNullOrWhiteSpace(Environment.CurrentDirectory))
            {
                builder.AddJsonFile(GetConfigurationFilePath("TemplateWriterSettings.json"), true);
                builder.AddJsonFile(GetConfigurationFilePath("GraphEndpointListSettings.json"), true);
            }

            Configuration = builder.AddEnvironmentVariables().Build();
        }
        public ConfigurationProvider(string targetLanguage)
        {
            var builder = new ConfigurationBuilder();
            if (!string.IsNullOrWhiteSpace(Environment.CurrentDirectory))
            {
                builder.AddJsonFile(GetConfigurationFilePath("TemplateWriterSettings.json"), true);
                builder.AddJsonFile(GetConfigurationFilePath($"{targetLanguage}Settings.json"), false);
            }

            Configuration = builder.AddEnvironmentVariables().Build();
        }
        private string GetConfigurationFilePath(string fileName)
        { //when using dotnet run the Environment.CurrentDirectory is set to csproj directory, not the bin like when runing with Visual Studio
            var regularPath = Path.Combine(Environment.CurrentDirectory, configFolder, fileName);
            if (File.Exists(regularPath))
                return regularPath;
            else
                return Path.Combine(Environment.CurrentDirectory, dotNetCorePathprefix, configFolder, fileName);
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
