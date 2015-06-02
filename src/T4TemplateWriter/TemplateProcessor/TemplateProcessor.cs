// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TextTemplating;
using Vipr.T4TemplateWriter.Extensions;
using Vipr.T4TemplateWriter.Output;
using Vipr.T4TemplateWriter;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.TemplateProcessor {

    public enum FileType {
        ComplexType,
        EntityType,
        EnumType,
        EntityFetcher,
        EntityOperations,
        EntityCollectionFetcher,
        EntityCollectionOperations,
        EntityClient,
        Other,
        Unknown
    }

    public class TemplateProcessor : ITemplateProcessor {
        protected readonly String _baseFilePath;

        private Dictionary<FileType, Func<TemplateFileInfo, IEnumerable<TextFile>>> _subProcessors;
        private static CustomHost _hostIntance;

        protected Dictionary<FileType, Func<TemplateFileInfo, IEnumerable<TextFile>>> SubProcessors {
            get { 
                if (null == _subProcessors) {
                    InitializeSubprocessors();
                }
                return _subProcessors;
            }
            set { 
                _subProcessors = value;
            }
        }

        protected OdcmModel Model { get; set; }
        protected Microsoft.VisualStudio.TextTemplating.Engine T4Engine { get; set; }
        protected IPathWriter PathWriter { get; set; }

        public TemplateProcessor(IPathWriter pathWriter, OdcmModel model, String baseFilePath) {
            T4Engine = new Engine();
            Model = model;
            PathWriter = pathWriter;
            PathWriter.Model = model;
            _baseFilePath = baseFilePath;
        }

        public IEnumerable<TextFile> Process(TemplateFileInfo template) {
            FileType fileType;
            Func<TemplateFileInfo, IEnumerable<TextFile>> subProcessor;

            String baseFileName = Path.GetFileNameWithoutExtension(template.TemplateName);
            Boolean valid = Enum.TryParse(baseFileName, true, out fileType);

            if (valid) {
                SubProcessors.TryGetValue(fileType, out subProcessor);
            } else {
                SubProcessors.TryGetValue(FileType.Other, out subProcessor);
            }
            return subProcessor(template);
        }


        protected void InitializeSubprocessors() {
             _subProcessors = new Dictionary<FileType, Func<TemplateFileInfo, IEnumerable<TextFile>>>() {             
                // Models
                {FileType.EntityType, ProcessEntityTypes},
                {FileType.ComplexType, ProcessComplexTypes},
                {FileType.EnumType, ProcessEnumTypes},
               
                // Fetchers
                {FileType.EntityCollectionFetcher, ProcessEntityTypes},
                {FileType.EntityCollectionOperations, ProcessEntityTypes},
                {FileType.EntityFetcher, ProcessEntityTypes},
                {FileType.EntityOperations, ProcessEntityTypes},

                // EntityContainer
                {FileType.EntityClient, ProcessEntityContainerType},

                // Other
                {FileType.Other, ProcessTemplate},
            };
        }



        protected virtual IEnumerable<TextFile> ProcessEntityContainerType(TemplateFileInfo template) {
            var container = Model.EntityContainer;
            yield return ProcessTemplate(template, container);
        }

        protected virtual IEnumerable<TextFile> ProcessEnumTypes(TemplateFileInfo template) {
            var enumTypes = Model.GetEnumTypes();
            foreach (OdcmObject enumType in enumTypes) {
                yield return ProcessTemplate(template, enumType);
            }
        }

        protected virtual IEnumerable<TextFile> ProcessComplexTypes(TemplateFileInfo template) {
            var complexTypes = Model.GetComplexTypes();
            foreach (OdcmObject complexType in complexTypes) {
                yield return ProcessTemplate(template, complexType);
            }
        }

        protected virtual IEnumerable<TextFile> ProcessEntityTypes(TemplateFileInfo template) {
            var entityTypes = Model.GetEntityTypes();
            foreach (OdcmObject entityType in entityTypes) {
                yield return ProcessTemplate(template, entityType);
            }
        }


        protected CustomHost GetCustomHost(TemplateFileInfo template, OdcmObject odcmObject) {
            if (_hostIntance == null)
                _hostIntance = new CustomHost(odcmObject);

            _hostIntance.BaseTemplatePath = _baseFilePath;
            _hostIntance.TemplateFile = template.FullPath;
            _hostIntance.Model = Model;
            _hostIntance.OdcmType = odcmObject;

            return _hostIntance;
        }

        protected IEnumerable<TextFile> ProcessTemplate(TemplateFileInfo template) {
            yield return this.ProcessTemplateInternal(template);
        }

        private TextFile ProcessTemplateInternal(TemplateFileInfo template) {
            return this.ProcessTemplate(template, null);
        }

        protected TextFile ProcessTemplate(TemplateFileInfo template, OdcmObject odcmObject) {
            var host = GetCustomHost(template, odcmObject);

            var templateContent = File.ReadAllText(host.TemplateFile);
            var output = T4Engine.ProcessTemplate(templateContent, host);

            if (host.Errors != null && host.Errors.HasErrors) {
                var errors = LogErrors(host, template);
                throw new InvalidOperationException(errors);
            }

            String fileName = (odcmObject != null) ? odcmObject.Name.ToCheckedCase() : template.TemplateName.ToCheckedCase();
            var path = PathWriter.WritePath(template, fileName);

            return new TextFile(path, output);
        }

        public string LogErrors(CustomHost host, TemplateFileInfo template) {
            var sb = new StringBuilder();
            if (host.Errors == null || host.Errors.Count <= 0) return sb.ToString();
            foreach (CompilerError error in host.Errors) {
                sb.AppendFormat(@"Error in template: {0}{1}", template.TemplateName, Environment.NewLine);
                sb.AppendFormat(@"Details: {0}{1}", error.ErrorText, Environment.NewLine);
                sb.AppendFormat(@"Line: {0}{1}", error.Line, Environment.NewLine);
                sb.AppendFormat(@"{0}{0}", Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
