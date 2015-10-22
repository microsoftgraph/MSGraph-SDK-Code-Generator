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

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
 
    public class TemplateProcessor : ITemplateProcessor
    {
        private static CustomT4Host _host;

        protected static CustomT4Host Host(ITemplateInfo templateInfo, string templatesDirectory, OdcmObject odcmObject, OdcmModel odcmModel)
        {
            if (_host == null)
            {
                _host = new CustomT4Host(templateInfo, templatesDirectory, odcmObject, odcmModel);
            }
            else
            {
                _host.Reset(templateInfo, templatesDirectory, odcmObject, odcmModel);
            }

            return _host;
        }

        private Dictionary<SubProcessor, Func<ITemplateInfo, IEnumerable<TextFile>>> subProcessors;

        protected Dictionary<SubProcessor, Func<ITemplateInfo, IEnumerable<TextFile>>> SubProcessors
        {
            get
            {
                if (this.subProcessors == null)
                {
                    InitializeSubprocessors();
                }
                return this.subProcessors;
            }
        }

        protected void InitializeSubprocessors()
        {
            this.subProcessors = new Dictionary<SubProcessor, Func<ITemplateInfo, IEnumerable<TextFile>>>() {
                {SubProcessor.EntityType,                   ProcessEntityTypes},
                {SubProcessor.ComplexType,                  ProcessComplexTypes},
                {SubProcessor.EnumType,                     ProcessEnumTypes},
                {SubProcessor.EntityContainer,              ProcessEntityContainerType},
                {SubProcessor.Property,                     ProcessProperties},
                {SubProcessor.CollectionProperty,           ProcessCollections},
                {SubProcessor.Method,                       ProcessMethods},
                {SubProcessor.NonCollectionMethod,          ProcessNonCollectionMethods},
                {SubProcessor.CollectionMethod,             ProcessCollectionMethods},
                {SubProcessor.MethodWithBody,               ProcessMethodsWithBody},
                {SubProcessor.Other,                        ProcessTemplate},
            };
        }

        protected OdcmModel CurrentModel { get; set; }
        protected Microsoft.VisualStudio.TextTemplating.Engine T4Engine { get; set; }
        protected IPathWriter PathWriter { get; set; }
        protected string TemplatesDirectory { get; set; }

        public TemplateProcessor(IPathWriter pathWriter, OdcmModel odcmModel, string templatesDirectory)
        {
            this.T4Engine = new Microsoft.VisualStudio.TextTemplating.Engine();
            this.CurrentModel = odcmModel;
            this.PathWriter = pathWriter;
            this.PathWriter.Model = odcmModel;
            this.TemplatesDirectory = templatesDirectory;
        }

        public string GetProcessorVerboseOutput()
        {
            return _host.TemplateHostStats.ToString();
        }

        public IEnumerable<TextFile> Process(ITemplateInfo templateInfo)
        {
            Func<ITemplateInfo, IEnumerable<TextFile>> subProcessor = ProcessTemplate;
            SubProcessors.TryGetValue(templateInfo.SubprocessorType, out subProcessor);
            return subProcessor(templateInfo);
        }


        protected virtual IEnumerable<TextFile> ProcessEntityContainerType(ITemplateInfo templateInfo)
        {
            var container = this.CurrentModel.EntityContainer;
            yield return ProcessTemplate(templateInfo, container, templateInfo.BaseFileName(containerName: container.Name));
        }

        protected virtual IEnumerable<TextFile> ProcessEnumTypes(ITemplateInfo templateInfo)
        {
            foreach (OdcmObject enumType in FilterOdcmEnumerable(templateInfo, this.CurrentModel.GetEnumTypes))
            {
                yield return ProcessTemplate(templateInfo, 
                                             enumType, 
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: enumType.Name));
            }
        }

        protected virtual IEnumerable<TextFile> ProcessComplexTypes(ITemplateInfo templateInfo)
        {
            foreach (OdcmObject complexType in FilterOdcmEnumerable(templateInfo, this.CurrentModel.GetComplexTypes))
            {
                yield return ProcessTemplate(templateInfo, 
                                             complexType, 
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: complexType.Name));
            }
        }

        protected virtual IEnumerable<TextFile> ProcessEntityTypes(ITemplateInfo templateInfo)
        {
            foreach (OdcmClass entityType in FilterOdcmEnumerable(templateInfo, this.CurrentModel.GetEntityTypes))
            {
                yield return ProcessTemplate(templateInfo, 
                                             entityType, 
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: entityType.Name));
            }
        }

        protected virtual IEnumerable<TextFile> ProcessCollections(ITemplateInfo templateInfo)
        {
            foreach (OdcmProperty property in FilterOdcmEnumerable(templateInfo, this.CollectionProperties))
            {
                yield return ProcessTemplate(templateInfo, 
                                             property, 
                                             templateInfo.BaseFileName(containerName:this.CurrentModel.EntityContainer.Name,
                                                                       className:property.Class.Name,
                                                                       propertyName:property.Name,
                                                                       propertyType:property.Type.Name));
            }
        }

        protected virtual IEnumerable<TextFile> ProcessProperties(ITemplateInfo templateInfo)
        {
            foreach(OdcmProperty property in FilterOdcmEnumerable(templateInfo, this.CurrentModel.GetProperties))
            {
                yield return ProcessTemplate(templateInfo,
                                             property,
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: property.Class.Name,
                                                                       propertyName: property.Name,
                                                                       propertyType: property.Type.Name));
            }
        }

        protected virtual IEnumerable<TextFile> ProcessMethods(ITemplateInfo templateInfo)
        {
            return this.ProcessMethods(templateInfo, this.CurrentModel.GetMethods);
        }

        protected virtual IEnumerable<TextFile> ProcessMethodsWithBody(ITemplateInfo templateInfo)
        {
            return this.ProcessMethods(templateInfo, this.MethodsWithBody);
        }

        protected virtual IEnumerable<TextFile> ProcessCollectionMethods(ITemplateInfo templateInfo)
        {
            return this.ProcessMethods(templateInfo, this.CollectionMethods);
        }

        protected virtual IEnumerable<TextFile> ProcessNonCollectionMethods(ITemplateInfo templateInfo)
        {
            return this.ProcessMethods(templateInfo, this.NonCollectionMethods);
        }

        protected virtual IEnumerable<TextFile> ProcessMethods(ITemplateInfo templateInfo, Func<IEnumerable<OdcmMethod>> methods)
        {
            foreach(OdcmMethod method in FilterOdcmEnumerable(templateInfo, methods))
            {
                yield return ProcessTemplate(templateInfo,
                                                   method,
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: method.Class.Name,
                                                                       methodName: method.Name));
            }
        }

        protected virtual IEnumerable<OdcmMethod> NonCollectionMethods()
        {
            return this.CurrentModel.GetMethods().Where(method => !method.IsCollection);
        }
        protected virtual IEnumerable<OdcmMethod> MethodsWithBody()
        {
            return this.CurrentModel.GetMethods().Where(method => method.Parameters != null && method.Parameters.Any() && method.IsAction());
        }

        protected virtual IEnumerable<OdcmMethod> CollectionMethods()
        {
            return this.CurrentModel.GetMethods().Where(method => method.IsCollection);
        }

        protected virtual IEnumerable<OdcmProperty> CollectionProperties()
        {
            return this.CurrentModel.GetProperties().Where(prop => prop.IsCollection);
        }

        protected virtual IEnumerable<OdcmObject> FilterOdcmEnumerable(ITemplateInfo templateInfo, Func<IEnumerable<OdcmObject>> modelMethod)
        {
            var filteredEnum = modelMethod().Where(o => templateInfo.ShouldIncludeObject(o));

            if (!filteredEnum.Any())
            {
                _host.TemplateHostStats.RecordProcessedNothing(templateInfo);
            }

            return filteredEnum;
        }

        protected IEnumerable<TextFile> ProcessTemplate(ITemplateInfo templateInfo)
        {
            yield return this.ProcessTemplate(templateInfo, null, templateInfo.BaseFileName());
        }

        protected TextFile ProcessTemplate(ITemplateInfo templateInfo, OdcmObject odcmObject, string fileName)
        {
            var host = TemplateProcessor.Host(templateInfo, this.TemplatesDirectory, odcmObject, this.CurrentModel);

            var templateContent = File.ReadAllText(host.TemplateFile);

            var output = this.T4Engine.ProcessTemplate(templateContent, host);
            if (!string.IsNullOrEmpty(host.TemplateName))
            {
                fileName = host.TemplateName;
            }

            if (host.Errors != null && host.Errors.HasErrors)
            {
                var errors = this.LogErrors(host, templateInfo);
                throw new InvalidOperationException(errors);
            }

            var path = this.PathWriter.WritePath(templateInfo, fileName);

            host.TemplateHostStats.RecordProcessed(templateInfo, odcmObject != null ? odcmObject.Name : string.Empty, path);
            return new TextFile(path, output);
        }

        public string LogErrors(CustomT4Host host, ITemplateInfo templateInfo)
        {
            var sb = new StringBuilder();
            if (host.Errors == null || host.Errors.Count <= 0) return sb.ToString();

            foreach (CompilerError error in host.Errors)
            {
                sb.AppendLine("TemplateProcessor ERROR").
                    AppendFormat(@"Name:     {0}{1}", templateInfo.TemplateName, Environment.NewLine).
                    AppendFormat(@"Line:     {0}{1}", error.Line, Environment.NewLine).
                    AppendFormat(@"Details:  {0}{1}", error.ErrorText, Environment.NewLine).
                    AppendLine().
                    AppendLine();
            }
            return sb.ToString();
        }
    }
}
