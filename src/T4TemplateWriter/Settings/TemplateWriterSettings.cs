// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System.Collections.Generic;
using System.Reflection;

namespace Vipr.T4TemplateWriter.Settings
{
    public class TemplateWriterSettings
    {
        public static TemplateWriterSettings mainSettingsObject = null;

        private Dictionary<string, List<Dictionary<string, string>>> templateMapping;

        //TODO: Differentiate between Java and Obj-C
        public TemplateWriterSettings()
        {
            // defaults
            AvailableLanguages = new List<string> { "Java", "ObjC" };
            PrimaryNamespaceName = "";
            NamespacePrefix = "MS";
            StaticCodePrefix = "MS";

            Plugins = new List<string>();
            InitializeCollections = true;
            TargetLanguage = "Java";
            NamespaceOverride = "com.microsoft.services.onenote";
            this.templateMapping = new Dictionary<string, List<Dictionary<string, string>>>();
            this.templateConfiguration = new List<Dictionary<string, string>>();
            TemplatesDirectory = null;
            DefaultFileCasing = "UpperCamel";
            CustomFlags = new List<string>();
        }

        public void CopyPropertiesFromMainSettings()
        {
            if (mainSettingsObject == null) return;

            foreach (PropertyInfo property in typeof(TemplateWriterSettings).GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(this, property.GetValue(mainSettingsObject, null), null);
                }
            }
        }

        /// <summary>
        /// Target languages provided via templates.
        /// </summary>
        public IList<string> AvailableLanguages { get; set; }

        /// <summary>
        /// The code language to be targeted by this templateInfo writer instance.
        /// </summary>
        public string TargetLanguage { get; set; }

        /// <summary>
        /// The template configuration mapping for all platforms.
        /// </summary>
        public Dictionary<string, List<Dictionary<string, string>>> TemplateMapping
        {
            get { return this.templateMapping; }
            set
            {
                //Replace input keys
                foreach (var entry in value)
                {
                    this.templateMapping[entry.Key] = entry.Value;
                }

            }
        }

        /// <summary>
        /// The default casing method to be used for file names when a casing method ins't specified.
        /// </summary>
        public string DefaultFileCasing;

        public IList<string> CustomFlags { get; set; }

        public IList<string> Plugins { get; set; }

        public string PrimaryNamespaceName { get; set; }

        public string NamespaceOverride { get; set; }

        public string NamespacePrefix { get; set; }

        public string StaticCodePrefix { get; set; }

        public bool InitializeCollections { get; set; }

        public bool AllowShortActions { get; set; }

        public string TemplatesDirectory { get; set; }

        public string[] LicenseHeader { get; set; }

        private List<Dictionary<string, string>> templateConfiguration;

        /// <summary>
        /// The dictionary created by combining the "Shared" and current language mapping.
        /// </summary>
        public IList<Dictionary<string, string>> TemplateConfiguration
        {
            get
            {
                if (templateConfiguration.Count == 0)
                {
                    List<Dictionary<string, string>> sharedConfig;
                    this.TemplateMapping.TryGetValue("Shared", out sharedConfig);
                    List<Dictionary<string, string>> languageConfig;
                    if (templateConfiguration != null && this.TemplateMapping.TryGetValue(this.TargetLanguage, out languageConfig))
                    {
                        //TODO aclev this is niave for now..
                        templateConfiguration.InsertRange(0, sharedConfig);
                        templateConfiguration.InsertRange(0, languageConfig);
                    }
                }
                return templateConfiguration;
            }
        }
    }
}
