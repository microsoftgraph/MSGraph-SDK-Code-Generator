// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.PathWriters
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;

    public class TypeScriptPathWriter : PathWriterBase
    {

        public override string WritePath(ITemplateInfo template, String baseFileName)
        {
            var theNamespace = this.CreateNamespace(template.OutputParentDirectory.ToLower());
            var namespacePath = this.CreatePathFromNamespace(theNamespace);
            var fileName = this.TransformFileName(template, baseFileName);
            String filePath = Path.Combine(namespacePath, fileName);
            return filePath;
        }

        public override string WritePath(ITemplateInfo template, string @namespace, string baseFileName)
        {
            // Typescript uses a single namespace, so redirect the call to other WritePath method:
            return WritePath(template, baseFileName);
        }

        private string CreateNamespace(string folderName)
        {
            var @namespace = this.Model.GetNamespace();
            var prefix = ConfigurationService.Settings.NamespacePrefix;

            if (String.IsNullOrEmpty(ConfigurationService.Settings.NamespaceOverride))
            {
                if (string.IsNullOrEmpty(folderName))
                {
                    return string.IsNullOrEmpty(prefix) ? @namespace
                                                        : string.Format("{0}.{1}", prefix, @namespace);
                }

                return string.IsNullOrEmpty(prefix) ? string.Format("{0}.{1}", @namespace, folderName)
                                                    : string.Format("{0}.{1}.{2}", prefix, @namespace, folderName);
            }

            @namespace = ConfigurationService.Settings.NamespaceOverride;
            return folderName != null ? string.Format("{0}.{1}", @namespace, folderName)
                                         : @namespace;
        }

        private string CreatePathFromNamespace(string @namespace)
        {
            var splitPaths = @namespace.Split('.');

            var destinationPath = splitPaths.Aggregate(string.Empty, (current, path) =>
                                  current + string.Format("{0}{1}", path, Path.DirectorySeparatorChar));
            return destinationPath;
        }
    }
}
