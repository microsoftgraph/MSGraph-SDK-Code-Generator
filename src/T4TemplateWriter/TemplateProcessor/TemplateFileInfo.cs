// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


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
        public IEnumerable<string> IncludedTypes { get; set; }

        public IEnumerable<string> ExcludedTypes { get; set; }

        public bool ShouldIncludeType(string typeName)
        {
            // Included and excluded are mutually exclusive
            if (this.IncludedTypes != null)
            {
                return this.IncludedTypes.Any(type => typeName.Contains(type));
            }
            else if (this.ExcludedTypes != null)
            {
                return this.ExcludedTypes.All(type => !typeName.Contains(type));
            }

            return true;
        }

        public string BaseFileName(string className= "", string propertyName = "", string methodName = "")
        {
            string coreName;
            if (this.NameFormat != null)
            {
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
