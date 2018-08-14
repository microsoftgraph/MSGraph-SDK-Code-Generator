using System;
using System.IO;
using Its.Configuration;
using Vipr.Core;

namespace GraphSDKGenerator
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationProvider()
        {
            if (!String.IsNullOrWhiteSpace(Environment.CurrentDirectory))
                Settings.SettingsDirectory = Path.Combine(Environment.CurrentDirectory, ".config");
        }

        public T GetConfiguration<T>()
        {
            return Settings.Get<T>();
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
