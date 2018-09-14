using System;
using System.IO;
using System.Reflection;
using Its.Configuration;
using Vipr.Core;

namespace Typewriter
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationProvider()
        {
                Settings.SettingsDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ".config");
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
