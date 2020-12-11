// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using Vipr.Core;

    /// <summary>
    /// The ConfigurationService configures the template writer with the target language and 
    /// other properties used to direct how the code files are generated.
    /// </summary>
    public static class ConfigurationService
    {
        private static IConfigurationProvider _configurationProvider;
        private static TemplateWriterSettings templateWriterSettings = null;
        private static string targetLanguage = null;
        private static string endpointVersion = null;
        private static Dictionary<string, string> properties = null;

        public static void Initialize(IConfigurationProvider configurationProvider, string targetLanguage = null, IEnumerable<string> properties = null, string endpointVersion = "v1.0")
        {
            ConfigurationService.endpointVersion = endpointVersion;

            _configurationProvider = configurationProvider;
            if (!String.IsNullOrEmpty(targetLanguage))
            {
                ConfigurationService.targetLanguage = targetLanguage;
            }
            if (properties != null)
            {
                Dictionary<string, string> propertyDictionary = new Dictionary<string, string>();
                foreach (string property in properties)
                {
                    string[] props = property.Split(':');

                    if (props.Length != 2)
                    {
                        throw new ArgumentException("A property was set in a unexpected form from the typewriter commandline.", "-p -properties");
                    }

                    propertyDictionary.Add(props[0],props[1]);
                }

                ConfigurationService.properties = propertyDictionary;
            }
        }

        private static TemplateWriterSettings LoadSettingsForLanguage()
        {
            TemplateWriterSettings mainTWS = _configurationProvider.GetConfiguration<TemplateWriterSettings>();

            if (targetLanguage != null)
            {
                mainTWS.TargetLanguage = targetLanguage;
            }
            if (properties != null)
            {
                mainTWS.Properties = properties;
            }

            mainTWS.DefaultBaseEndpointUrl = String.Format("https://graph.microsoft.com/{0}", endpointVersion);

            TemplateWriterSettings.mainSettingsObject = mainTWS;
            
            //Call the generic GetConfiguration method with our new type.
            var mergedSettings = (_configurationProvider
                .GetType()
                .GetConstructor(new [] { typeof(string) })
                .Invoke(new[] { targetLanguage }) as IConfigurationProvider)
                .GetConfiguration<TemplateWriterSettings>();

            mergedSettings.CopyPropertiesFromMainSettings();
            return mergedSettings;

        }
        public static void ResetSettings()
        {
            templateWriterSettings = null;
        }

        public static TemplateWriterSettings Settings
        {
            get
            {
                if (templateWriterSettings == null || templateWriterSettings.TargetLanguage != ConfigurationService.targetLanguage)
                {
                    templateWriterSettings = _configurationProvider != null
                        ? LoadSettingsForLanguage()
                        : new TemplateWriterSettings();
                }

                return templateWriterSettings;
            }
        }
    }
}
