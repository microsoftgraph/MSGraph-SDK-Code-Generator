// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.PathWriters
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
    using Microsoft.Graph.ODataTemplateWriter.CodeHelpers.PHP;

    public class PHPPathWriter : PathWriterBase
    {

        public override String WritePath(ITemplateInfo template, String entityTypeName)
        {
            var theNamespace = this.CreateNamespace(template.TemplateType.ToString().ToUpperFirstChar());
            var namespacePath = this.CreatePathFromNamespace(theNamespace);

            var fileName = Extensions.StringExtensions.ToCheckedCase(this.TransformFileName(template, TypeHelperPHP.SanitizeEntityName(entityTypeName)));

            String filePath = Path.Combine(namespacePath, fileName);
            return filePath;
        }

        public override string WritePath(ITemplateInfo template, string @namespace, string entityTypeName)
        {
            // for backwards compatibility e.g. we want folders to be com/Microsoft/Graph/Model for default namespace.
            // TODO: maybe we can break this assumption by modifying the pipelines which copy the files after generation.
            // maybe remove this.
            var theNamespace = "com." + @namespace + ".Model";
            var namespacePath = this.CreatePathFromNamespace(theNamespace);

            var fileName = Extensions.StringExtensions.ToCheckedCase(this.TransformFileName(template, TypeHelperPHP.SanitizeEntityName(entityTypeName)));

            String filePath = Path.Combine(namespacePath, fileName);
            return filePath;
        }

        private string CreateNamespace(string folderName)
        {
            var @namespace = this.Model.GetNamespace();
            var prefix = ConfigurationService.Settings.NamespacePrefix;

            if (string.IsNullOrEmpty(folderName))
            {
                return string.IsNullOrEmpty(prefix) ? @namespace
                                                    : string.Format("{0}.{1}", prefix, @namespace);
            }
            return string.IsNullOrEmpty(prefix) ? string.Format("{0}.{1}", @namespace, folderName)
                                                : string.Format("{0}.{1}.{2}", prefix, @namespace, folderName);
        }

        private string CreatePathFromNamespace(string @namespace)
        {
            var splittedPaths = @namespace.Split('.');

            //Capitalize all but the src directory
            for (int i = 1; i < splittedPaths.Length; i++)
            {
                splittedPaths[i] = splittedPaths[i].ToUpperFirstChar();
            }

            var destinationPath = splittedPaths.Aggregate(string.Empty, (current, path) =>
                                  current + string.Format("{0}{1}", path, Path.DirectorySeparatorChar));
            return destinationPath;
        }
    }
}
