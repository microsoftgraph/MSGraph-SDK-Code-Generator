// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.PathWriters
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;

    public class PythonPathWriter : PathWriterBase
    {

        public override String WritePath(ITemplateInfo template, String entityTypeName)
        {
            var theNamespace = this.CreateNamespace(template.TemplateType.ToString().ToLower());
            var namespacePath = this.CreatePathFromNamespace(theNamespace);

            var fileName = Extensions.StringExtensions.ToUnderscore(this.TransformFileName(template, entityTypeName)).ToLower();

            String filePath = Path.Combine(namespacePath, fileName);
            return filePath;
        }

        private string CreateNamespace(string folderName)
        {
            var @namespace = this.Model.GetNamespace();
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
