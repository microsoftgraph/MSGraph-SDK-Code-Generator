// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information. 

namespace Vipr.T4TemplateWriter.Output
{
    using System;
    using System.IO;
    using System.Linq;
    using Vipr.T4TemplateWriter.Settings;
    using Vipr.T4TemplateWriter.TemplateProcessor;

    public class PythonPathWriter : PathWriterBase
    {

        public override String WritePath(ITemplateInfo template, String entityTypeName)
        {
            var theNamespace = CreateNamespace(template.TemplateType.ToString().ToLower());
            var namespacePath = CreatePathFromNamespace(theNamespace);

            var fileName = Extensions.StringExtensions.ToUnderscore(TransformFileName(template, entityTypeName)).ToLower();

            String filePath = Path.Combine(namespacePath, fileName);
            return filePath;
        }

        private string CreateNamespace(string folderName)
        {
            var @namespace = Model.GetNamespace();
            var prefix = ConfigurationService.Settings.NamespacePrefix;

            if (string.IsNullOrEmpty(folderName)) {
                return string.IsNullOrEmpty(prefix) ? @namespace
                                                    : string.Format("{0}.{1}", prefix, @namespace);
            }
            return string.IsNullOrEmpty(prefix) ? string.Format("{0}.{1}", @namespace, folderName)
                                                : string.Format("{0}.{1}.{2}", prefix, @namespace, folderName);
        }

        private string CreatePathFromNamespace(string @namespace)
        {
            var splittedPaths = @namespace.Split('.');

            var destinationPath = splittedPaths.Aggregate(string.Empty, (current, path) =>
                                  current + string.Format("{0}{1}", path, Path.DirectorySeparatorChar));
            return destinationPath;
        }
    }
}
