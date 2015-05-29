// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.Collections.Generic;
using System.Linq;
using T4TemplateWriter.Extensions;
using T4TemplateWriter.Settings;

namespace T4TemplateWriter.Templates
{
    public class TemplateSourceReader : ITemplateSourceReader
    {
        public IList<Template> Read(Type targetType, TemplateWriterSettings config)
        {
            var resourceNames = targetType.Assembly.GetManifestResourceNames();
            var baseString = string.Format("{0}.Base", config.TargetLanguage);

            return resourceNames.Select(resource =>
            {
                var splits = resource.Split('.');
                var name = splits.ElementAt(splits.Count() - 2);
                var folderName = FolderName(resource, config);

                return new Template(name, resource)
                {
                    FolderName = folderName,
                    Name = name,
                    ResourceName = resource,
                    IsBase = resource.Contains(baseString, StringComparison.InvariantCultureIgnoreCase),
                    TemplateType = ResolveTemplateType(folderName)
                };
            }).ToList();
        }

        public TemplateType ResolveTemplateType(string name)
        {
            if (name.Equals("model", StringComparison.InvariantCultureIgnoreCase))
            {
                return TemplateType.Model;
            }

            if (name.Equals("orc", StringComparison.InvariantCultureIgnoreCase))
            {
                return TemplateType.Orc;
            }

            return TemplateType.Other;
        }

        private string FolderName(string resourceName, TemplateWriterSettings config)
        {
            var modelLocation = string.Format("{0}.Models", config.TargetLanguage);
            var odataLocation = string.Format("{0}.Orc", config.TargetLanguage);

            if (resourceName.Contains(modelLocation, StringComparison.InvariantCultureIgnoreCase))
            {
                return "model";
            }
            if (resourceName.Contains(odataLocation, StringComparison.InvariantCultureIgnoreCase))
            {
                return "orc";
            }
            return string.Empty;
        }
    }
}
