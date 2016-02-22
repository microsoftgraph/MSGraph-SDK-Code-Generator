// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
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

using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    using Vipr.T4TemplateWriter.CodeHelpers.ObjC;

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
        private Dictionary<String, Func<ITextTemplatingEngineHost, string>> preProcessedTemplates;

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
                {SubProcessor.MediaEntityType,              ProcessMediaEntityTypes},
                {SubProcessor.Property,                     ProcessProperties},
                {SubProcessor.StreamProperty,               ProcessStreamProperties},
                {SubProcessor.CollectionProperty,           ProcessCollections},
                {SubProcessor.NavigationCollectionProperty, ProcessNavigationCollections},
                {SubProcessor.CollectionReferenceProperty,  ProcessCollectionReferences},
                {SubProcessor.EntityReferenceType,          ProcessEntityReferenceProperties},
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
            this.preProcessedTemplates = new Dictionary<string, Func<ITextTemplatingEngineHost, string>>();
        }

        public string GetProcessorVerboseOutput()
        {
            //TODO:Check NullReferenceException due no templates loaded.
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
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: property.Class.Name,
                                                                       propertyName: property.Name,
                                                                       propertyType: property.Type.Name));
            }
        }

       

        protected virtual IEnumerable<TextFile> ProcessNavigationCollections(ITemplateInfo templateInfo)
        {
            foreach (OdcmProperty property in FilterOdcmEnumerable(templateInfo, this.NavigationCollectionProperties))
            {
                yield return ProcessTemplate(templateInfo,
                                             property,
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: property.Class.Name,
                                                                       propertyName: property.Name,
                                                                       propertyType: property.Type.Name));
            }
        }

        protected virtual IEnumerable<TextFile> ProcessCollectionReferences(ITemplateInfo templateInfo)
        {
            foreach (OdcmProperty property in FilterOdcmEnumerable(templateInfo, this.CollectionReferenceProperties))
            {
                yield return ProcessTemplate(templateInfo,
                                             property,
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: property.Class.Name,
                                                                       propertyName: property.Name,
                                                                       propertyType: property.Type.Name));
            }
        }

        protected virtual IEnumerable<TextFile> ProcessProperties(ITemplateInfo templateInfo)
        {
            foreach (OdcmProperty property in FilterOdcmEnumerable(templateInfo, this.CurrentModel.GetProperties))
            {
                yield return ProcessTemplate(templateInfo,
                                             property,
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: property.Class.Name,
                                                                       propertyName: property.Name,
                                                                       propertyType: property.Type.Name));
            }
        }

        protected virtual IEnumerable<TextFile> ProcessEntityReferenceProperties(ITemplateInfo templateInfo)
        {
            foreach (OdcmClass entityType in FilterOdcmEnumerable(templateInfo, this.CurrentModel.GetEntityReferenceTypes))
            {
                yield return ProcessTemplate(templateInfo,
                                             entityType,
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: entityType.Name));
            }
        }

        protected virtual IEnumerable<TextFile> ProcessStreamProperties(ITemplateInfo templateInfo)
        {
            foreach (OdcmProperty property in FilterOdcmEnumerable(templateInfo, this.CurrentModel.GetStreamProperties))
            {
                yield return ProcessTemplate(templateInfo,
                                             property,
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: property.Class.Name,
                                                                       propertyName: property.Name,
                                                                       propertyType: property.Type.Name));
            }
        }

        protected virtual IEnumerable<TextFile> ProcessMediaEntityTypes(ITemplateInfo templateInfo)
        {
            foreach (OdcmClass entityType in FilterOdcmEnumerable(templateInfo, this.CurrentModel.GetMediaEntityTypes))
            {
                yield return ProcessTemplate(templateInfo,
                                             entityType,
                                             templateInfo.BaseFileName(containerName: this.CurrentModel.EntityContainer.Name,
                                                                       className: entityType.Name));
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
            foreach (OdcmMethod method in FilterOdcmEnumerable(templateInfo, methods))
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
            return this.CurrentModel.GetMethods().Where(method => !method.IsCollection && (method.ReturnType == null || method.ReturnType is OdcmPrimitiveType));
        }
        protected virtual IEnumerable<OdcmMethod> MethodsWithBody()
        {
            return this.CurrentModel.GetMethods().Where(method => method.Parameters != null && method.Parameters.Any() && method.IsAction());
        }

        protected virtual IEnumerable<OdcmMethod> CollectionMethods()
        {
            var methods = this.CurrentModel.GetMethods().Where(method => method.IsCollection && method.ReturnType != null).ToList();
            return methods;
        }

        protected virtual IEnumerable<OdcmProperty> NavigationCollectionProperties()
        {
            return this.CurrentModel.GetProperties().Where(prop => prop.IsCollection && prop.IsNavigation());
        }

        protected virtual IEnumerable<OdcmProperty> CollectionReferenceProperties()
        {
            return this.CurrentModel.GetProperties().Where(prop => prop.IsReference() && prop.IsCollection);
        }

        protected virtual IEnumerable<OdcmProperty> CollectionProperties()
        {
            return this.CurrentModel.GetProperties().Where(prop => prop.IsCollection && !(prop.Type is OdcmPrimitiveType));
        }

        protected virtual IEnumerable<OdcmObject> FilterOdcmEnumerable(ITemplateInfo templateInfo, Func<IEnumerable<OdcmObject>> modelMethod)
        {
            var filteredEnum = modelMethod().Where(o => templateInfo.ShouldIncludeObject(o));

            if (_host != null && !filteredEnum.Any())
            {
                _host.TemplateHostStats.RecordProcessedNothing(templateInfo);
            }

            return filteredEnum;
        }

        protected IEnumerable<TextFile> ProcessTemplate(ITemplateInfo templateInfo)
        {
            yield return this.ProcessTemplate(templateInfo, null, templateInfo.BaseFileName());
        }

        private const string RuntimeTemplatesNamespace = "RuntimeTemplates";

        private Func<ITextTemplatingEngineHost, string> PreProcessTemplate(ITemplateInfo templateInfo)
        {
            var templateContent = File.ReadAllText(templateInfo.FullPath);

            string language;
            string[] references;
            var className = templateInfo.TemplateName.Replace(".", "_");
            var dummyHost = new CustomT4Host(templateInfo, this.TemplatesDirectory, null, null);
            var generatedCode = this.T4Engine.PreprocessTemplate(templateContent, dummyHost, className, RuntimeTemplatesNamespace, out language, out references);

            var parameters = new CompilerParameters
            {
                OutputAssembly = templateInfo.TemplateName + ".dll",
                GenerateInMemory = false,
                GenerateExecutable = false,
                IncludeDebugInformation = true,
            };

            var assemblyLocations = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .Where(a => !a.IsDynamic)
                                    .Select(a => a.Location);
            parameters.ReferencedAssemblies.AddRange(assemblyLocations.ToArray());

            var provider = new CSharpCodeProvider();

            var results = provider.CompileAssemblyFromSource(parameters, generatedCode);

            if (results.Errors.Count > 0)
            {
                for (int i = 0; i < results.Output.Count; i++)
                {
                    Console.WriteLine(results.Output[i]);
                }

                for (int i = 0; i < results.Errors.Count; i++)
                {
                    Console.WriteLine(i.ToString() + ": " + results.Errors[i].ToString());
                }

                throw new InvalidOperationException("Template error.");
            }

            var assembly = results.CompiledAssembly;
            var templateClassType = assembly.GetType(RuntimeTemplatesNamespace + "." + className);

            dynamic templateClassInstance = Activator.CreateInstance(templateClassType);
            return (ITextTemplatingEngineHost host) =>
            {
                templateClassInstance.Host = host;
                return templateClassInstance.TransformText();
            };

        }

        protected TextFile ProcessTemplate(ITemplateInfo templateInfo, OdcmObject odcmObject, string fileName)
        {
            var host = TemplateProcessor.Host(templateInfo, this.TemplatesDirectory, odcmObject, this.CurrentModel);

            Func<ITextTemplatingEngineHost, string> preProcessedTemplate;

            if (!preProcessedTemplates.TryGetValue(templateInfo.FullPath, out preProcessedTemplate))
            {
                preProcessedTemplate = this.PreProcessTemplate(templateInfo);
                preProcessedTemplates.Add(templateInfo.FullPath, preProcessedTemplate);
            }

            var output = preProcessedTemplate(host);

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
