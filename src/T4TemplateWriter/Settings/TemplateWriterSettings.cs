// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System.Collections.Generic;

namespace Vipr.T4TemplateWriter.Settings
{
    public class TemplateWriterSettings
    {

        //TODO: Differentiate between Java and Obj-C
        public TemplateWriterSettings()
        {
            // defaults
            AvailableLanguages = new List<string> { "Java", "ObjC" };
            PrimaryNamespaceName = "";
            NamespacePrefix = "MS";
            Plugins = new List<string>();
            InitializeCollections = true;
            TargetLanguage = "Java";
            NamespaceOverride = "com.microsoft.services.onenote";
            TemplateMapping = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            templateConfiguration = null;
            TemplatesDirectory = null;
            DefaultFileCasing = "UpperCamel";
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
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> TemplateMapping { get; set; }

        /// <summary>
        /// The default casing method to be used for file names when a casing method ins't specified.
        /// </summary>
        public string DefaultFileCasing;

        public IList<string> Plugins { get; set; }

        public string PrimaryNamespaceName { get; set; }

        public string NamespaceOverride { get; set; }

        public string NamespacePrefix { get; set; }

        public bool InitializeCollections { get; set; }

        public bool AllowShortActions { get; set; }

        public string TemplatesDirectory { get; set; }

        private Dictionary<string, Dictionary<string, string>> templateConfiguration;

        /// <summary>
        /// The dictionary created by combining the "Shared" and current language mapping.
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> TemplateConfiguration
        {
            get
            {
                if (templateConfiguration == null)
                {
                    this.TemplateMapping.TryGetValue(this.TargetLanguage, out templateConfiguration);
                    Dictionary<string, Dictionary<string, string>> sharedConfiguration;
                    if (templateConfiguration != null && this.TemplateMapping.TryGetValue("Shared", out sharedConfiguration))
                    {
                        foreach(var mapping in sharedConfiguration)
                        {
                            templateConfiguration.Add(mapping.Key, mapping.Value);
                        }
                    }
                }
                return templateConfiguration;
            }
        }
    }
}
