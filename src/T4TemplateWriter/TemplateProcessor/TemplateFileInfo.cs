// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{

    public class TemplateFileInfo : ITemplateInfo
    {
        public string Id { get { return this.FullPath; } }

        public string TemplateName { get; set; }

        public string TemplateLanguage { get; set; }

        public TemplateType TemplateType { get; set; }

        public SubProcessorType SubprocessorType { get; set; }

        public string TemplateDirectoryName { get; set; }

        public string TemplateBaseName { get; set; }

        public string FullPath { get; set; }

        public string NameFormat { get; set; }

        public string FileExtension { get; set; }
        public IEnumerable<string> IncludedObjects { get; set; }

        public IEnumerable<string> ExcludedObjects { get; set; }

        public IEnumerable<string> ObjectDescriptions { get; set; }

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
            if (shouldInclude && this.ObjectDescriptions != null)
            {
                shouldInclude = this.ObjectDescriptions.Any(objDescp => odcmObject.LongDescriptionContains(objDescp));
            }

            return shouldInclude;
        }

        public string BaseFileName(string className = "", string propertyName = "", string methodName = "")
        {
            string coreName;
            if (this.NameFormat != null)
            {
                // TODO: aclev
                // This is naive for now.  Once we add casing to the strings order will matter.
                coreName = this.NameFormat.Replace("<Class>", className).Replace("<Property>", propertyName).Replace("<Method>", methodName);
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
            return Equals((TemplateFileInfo)obj);
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
