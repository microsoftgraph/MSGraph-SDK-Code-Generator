using Vipr.Core;

namespace TemplateWriter.Settings
{
    public static class ConfigurationService
    {
        private static IConfigurationProvider _configurationProvider;

        public static void Initialize(IConfigurationProvider configurationProvider) {
            _configurationProvider = configurationProvider;
        }

        public static TemplateWriterSettings Settings {
            get {
                return _configurationProvider != null
                    ? _configurationProvider.GetConfiguration<TemplateWriterSettings>()
                    : new TemplateWriterSettings();
            }
        }
    }
}