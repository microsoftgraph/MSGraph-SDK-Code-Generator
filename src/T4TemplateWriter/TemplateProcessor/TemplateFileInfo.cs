// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.TemplateProcessor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor.Enums;
    using Vipr.Core.CodeModel;

    public class TemplateFileInfo : ITemplateInfo
    {
        public string Id { get { return this.FullPath; } }

        public string TemplateName { get; set; }

        public string TemplateLanguage { get; set; }

        public Template TemplateType { get; set; }

        public SubProcessor SubprocessorType { get; set; }

        public FileNameCasing Casing { get; set; }

        public string OutputParentDirectory { get; set; }

        public string TemplateBaseName { get; set; }

        public string FullPath { get; set; }

        public string NameFormat { get; set; }

        public string FileExtension { get; set; }
        public IEnumerable<string> IncludedObjects { get; set; }

        public IEnumerable<string> ExcludedObjects { get; set; }

        public IEnumerable<string> MatchingDescriptions { get; set; }

        public IEnumerable<string> IgnoreDescriptions { get; set; }

        public bool ShouldIncludeObject(OdcmObject odcmObject)
        {
            bool shouldInclude = true;
            if (this.IncludedObjects != null)
            {
                shouldInclude = this.IncludedObjects.Any(objectName => odcmObject.Name.Equals(objectName));
            }
            else if (this.ExcludedObjects != null)
            {
                shouldInclude = this.ExcludedObjects.All(objectName => !odcmObject.Name.Equals(objectName));
            }

            // Include and Exclude have priority over matches. 
            // Only check if the description matches if we should include the object.
            if (shouldInclude && this.MatchingDescriptions != null)
            {
                shouldInclude = this.MatchingDescriptions.Any(objDescp => odcmObject.LongDescriptionContains(objDescp));
            }

            if (shouldInclude && this.IgnoreDescriptions != null)
            {
                shouldInclude = !this.IgnoreDescriptions.Any(objDescp => odcmObject.LongDescriptionContains(objDescp));
            }

            return shouldInclude;
        }

        public string BaseFileName(string containerName = "", string className = "", string propertyName = "", string methodName = "", string propertyType = "")
        {
            string coreName;
            if (this.NameFormat != null)
            {
                // If the namespace has been left on the method name, remove it.
                if (!String.IsNullOrEmpty(methodName) && methodName.Contains('.'))
                {
                    methodName = methodName.Split('.')[1];
                }
                //Replace all values with UpperCamelCased values from Edmx (default for Edmx is lower camel case).
                coreName = this.NameFormat.Replace("<Class>", className.ToUpperFirstChar())
                                          .Replace("<Property>", propertyName.ToUpperFirstChar())
                                          .Replace("<PropertyType>", propertyType.ToUpperFirstChar())
                                          .Replace("<Method>", methodName.ToUpperFirstChar())
                                          .Replace("<Container>", containerName.ToUpperFirstChar())
                                          .Replace("<NamespacePrefix>", ConfigurationService.Settings.NamespacePrefix.ToUpperFirstChar());
                // replace with the proper naming scheme.
                switch (this.Casing)
                {
                    case FileNameCasing.UpperCamel:
                        coreName = coreName.ToUpperFirstChar();
                        break;
                    case FileNameCasing.LowerCamel:
                        coreName = coreName.ToLowerFirstChar();
                        break;
                    case FileNameCasing.Snake:
                        coreName = coreName.ToLowerFirstChar().ToUnderscore();
                        break;
                }
            }
            else
            {
                coreName = this.TemplateBaseName;
            }
            return coreName;
        }

        virtual protected bool Equals(TemplateFileInfo other)
        {
            return (this.Id == other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((TemplateFileInfo)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(TemplateFileInfo left, TemplateFileInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TemplateFileInfo left, TemplateFileInfo right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            var dirSep = Path.DirectorySeparatorChar;
            return (this.TemplateLanguage + dirSep + this.TemplateName + dirSep + this.TemplateName);
        }

    }
}
