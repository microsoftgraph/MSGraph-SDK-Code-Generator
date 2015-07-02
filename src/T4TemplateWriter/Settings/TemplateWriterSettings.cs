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
        }

        /// <summary>
        /// Target languages provided via templates.
        /// </summary>
        public IList<string> AvailableLanguages { get; set; }

        /// <summary>
        /// The code language to be targeted by this templateInfo writer instance.
        /// </summary>
        public string TargetLanguage { get; set; }

        public IList<string> Plugins { get; set; }

        public string PrimaryNamespaceName { get; set; }

        public string NamespacePrefix { get; set; }

        public bool InitializeCollections { get; set; }

        public bool AllowShortActions { get; set; }
    }
}
