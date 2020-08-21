// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.PathWriters
{
    using System.IO;
    using System.Linq;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
    using Microsoft.Graph.ODataTemplateWriter.CodeHelpers.PHP;
    using System.Text;

    public class PHPPathWriter : PathWriterBase
    {

        public override string WritePath(ITemplateInfo template, string entityTypeName)
        {
            return WritePath(template, Model.GetNamespace(), entityTypeName);
        }

        public override string WritePath(ITemplateInfo template, string @namespace, string entityTypeName)
        {
            var theNamespace = this.CreateNamespace(@namespace, template.TemplateType.ToString().ToUpperFirstChar());
            var namespacePath = this.CreatePathFromNamespace(theNamespace);

            var fileName = StringExtensions.ToCheckedCase(this.TransformFileName(template, TypeHelperPHP.SanitizeEntityName(entityTypeName)));

            return Path.Combine(namespacePath, fileName);
        }

        /// <summary>
        /// Creates a full namespace using odcm namespace and optionally prepending prefixes such as com and Beta
        /// and appending folderName such as Model
        /// </summary>
        /// <param name="namespace">odcm namespace</param>
        /// <param name="folderName">folder name e.g. Model</param>
        /// <returns>full namespace such as com.Beta.Microsoft.Graph.CallRecords.Model</returns>
        private string CreateNamespace(string @namespace, string folderName)
        {
            var namespaceBuilder = new StringBuilder();
            var prefix = ConfigurationService.Settings.NamespacePrefix;
            if (!string.IsNullOrEmpty(prefix))
            {
                namespaceBuilder.Append($"{prefix}."); // e.g. "com."
            }

            // TemplateWriterSettings.Properties are set at the Typewriter command line. Check the command line 
            // documentation for more information on how the TemplateWriterSettings.Properties is used.
            if (ConfigurationService.Settings.Properties?.ContainsKey("php.namespacePrefix") == true)
            {
                namespaceBuilder.Append($"{ConfigurationService.Settings.Properties["php.namespacePrefix"]}."); // e.g. "com.Beta."
            }

            namespaceBuilder.Append(@namespace); // e.g. com.Beta.Microsoft.Graph.CallRecords
            if (!string.IsNullOrEmpty(folderName))
            {
                namespaceBuilder.Append($".{folderName}"); // e.g. com.Beta.Microsoft.Graph.CallRecords.Model
            }

            return namespaceBuilder.ToString();
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
