// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.TemplateProcessor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.CSharp;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Microsoft.Graph.ODataTemplateWriter.PathWriters;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor.Enums;
    using Microsoft.VisualStudio.TextTemplating;
    using Vipr.Core;
    using Vipr.Core.CodeModel;
    using NLog;
    using Microsoft.Graph.ODataTemplateWriter.CodeHelpers.CSharp;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Emit;
    using System.Reflection;
    using Mono.TextTemplating;

    public class TemplateProcessor : ITemplateProcessor
    {
        private static CustomT4Host _host;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected static CustomT4Host Host(ITemplateInfo templateInfo, string templatesDirectory, OdcmObject odcmObject, OdcmModel odcmModel)
        {
            // Need to always set the host. Typically, this is run against a single platform when generating codefiels.
            // In test cases, we need to target multiple platforms. Since much of this code is static, we need to make sure 
            // reset the information provided to the template processor. This change fixes a bug when targeting 
            // multiple platforms in a test.
            _host = new CustomT4Host(templateInfo, templatesDirectory, odcmObject, odcmModel);

            return _host;
        }

        private Dictionary<SubProcessor, Func<ITemplateInfo, IEnumerable<TextFile>>> subProcessors;
        private readonly Dictionary<String, Func<ITextTemplatingEngineHost, string>> preProcessedTemplates;

        protected Dictionary<SubProcessor, Func<ITemplateInfo, IEnumerable<TextFile>>> SubProcessors
        {
            get
            {
                if (this.subProcessors == null)
                {
                    this.InitializeSubprocessors();
                }
                return this.subProcessors;
            }
        }

        protected void InitializeSubprocessors()
        {
            this.subProcessors = new Dictionary<SubProcessor, Func<ITemplateInfo, IEnumerable<TextFile>>>() {
                {SubProcessor.EntityType,                   this.ProcessEntityTypes},
                {SubProcessor.ComplexType,                  this.ProcessComplexTypes},
                {SubProcessor.EnumType,                     this.ProcessEnumTypes},
                {SubProcessor.EntityContainer,              this.ProcessEntityContainerType},
                {SubProcessor.MediaEntityType,              this.ProcessMediaEntityTypes},
                {SubProcessor.Property,                     this.ProcessProperties},
                {SubProcessor.StreamProperty,               this.ProcessStreamProperties},
                {SubProcessor.CollectionProperty,           this.ProcessCollections},
                {SubProcessor.NavigationCollectionProperty, this.ProcessNavigationCollections},
                {SubProcessor.CollectionReferenceProperty,  this.ProcessCollectionReferences},
                {SubProcessor.EntityReferenceType,          this.ProcessEntityReferenceProperties},
                {SubProcessor.Method,                       this.ProcessMethods},
                {SubProcessor.NonCollectionMethod,          this.ProcessNonCollectionMethods},
                {SubProcessor.CollectionMethod,             this.ProcessCollectionMethods},
                {SubProcessor.MethodWithBody,               this.ProcessMethodsWithBody},
                {SubProcessor.Other,                        this.ProcessTemplate},
            };
        }

        protected OdcmModel CurrentModel { get; set; }
        protected TemplatingEngine T4Engine { get; set; }
        protected IPathWriter PathWriter { get; set; }
        protected string TemplatesDirectory { get; set; }

        public TemplateProcessor(IPathWriter pathWriter, OdcmModel odcmModel, string templatesDirectory)
        {
            this.T4Engine = new TemplatingEngine();
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
            Func<ITemplateInfo, IEnumerable<TextFile>> subProcessor = this.ProcessTemplate;
            this.SubProcessors.TryGetValue(templateInfo.SubprocessorType, out subProcessor);
            logger.Info("Current subprocessor type: {0}", templateInfo.SubprocessorType);
            return subProcessor(templateInfo);
        }


        protected virtual IEnumerable<TextFile> ProcessEntityContainerType(ITemplateInfo templateInfo)
        {
            yield return this.ProcessTemplate(templateInfo, this.CurrentModel.EntityContainer);
        }

        protected virtual IEnumerable<TextFile> ProcessEnumTypes(ITemplateInfo templateInfo)
        {
            return this.ProcessTypes(templateInfo, this.CurrentModel.GetEnumTypes);
        }

        protected virtual IEnumerable<TextFile> ProcessComplexTypes(ITemplateInfo templateInfo)
        {
            return this.ProcessTypes(templateInfo, this.CurrentModel.GetComplexTypes);
        }

        protected virtual IEnumerable<TextFile> ProcessEntityTypes(ITemplateInfo templateInfo)
        {
            return this.ProcessTypes(templateInfo, this.CurrentModel.GetEntityTypes);
        }

        protected virtual IEnumerable<TextFile> ProcessCollections(ITemplateInfo templateInfo)
        {
            return this.ProcessProperties(templateInfo, this.CollectionProperties);
        }

        protected virtual IEnumerable<TextFile> ProcessMediaEntityTypes(ITemplateInfo templateInfo)
        {
            return this.ProcessTypes(templateInfo, this.CurrentModel.GetMediaEntityTypes);
        }

        protected virtual IEnumerable<TextFile> ProcessNavigationCollections(ITemplateInfo templateInfo)
        {
            return this.ProcessProperties(templateInfo, this.NavigationCollectionProperties);
        }

        protected virtual IEnumerable<TextFile> ProcessCollectionReferences(ITemplateInfo templateInfo)
        {
            return this.ProcessProperties(templateInfo, this.CollectionReferenceProperties);
        }

        protected virtual IEnumerable<TextFile> ProcessProperties(ITemplateInfo templateInfo)
        {
            return this.ProcessProperties(templateInfo, this.CurrentModel.GetProperties);
        }

        protected virtual IEnumerable<TextFile> ProcessEntityReferenceProperties(ITemplateInfo templateInfo)
        {
            return this.ProcessTypes(templateInfo, this.CurrentModel.GetEntityReferenceTypes);
        }

        protected virtual IEnumerable<TextFile> ProcessStreamProperties(ITemplateInfo templateInfo)
        {
            return this.ProcessProperties(templateInfo, this.CurrentModel.GetStreamProperties);
        }

        protected virtual IEnumerable<TextFile> ProcessMethods(ITemplateInfo templateInfo)
        {
            return this.ProcessMethods(templateInfo, this.CurrentModel.GetMethods);
        }

        protected virtual IEnumerable<TextFile> ProcessMethodsWithBody(ITemplateInfo templateInfo)
        {
            if (templateInfo.TemplateLanguage == "Java") 
                return new List<TextFile>();
            else
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

        private IEnumerable<TextFile> ProcessTypes(ITemplateInfo templateInfo, Func<IEnumerable<OdcmObject>> modelMethod)
        {
            return this.FilterOdcmEnumerable(templateInfo, modelMethod)
                    .Select(odcmType => this.ProcessTemplate(templateInfo, odcmType, className: odcmType.Name));
        }

        private IEnumerable<TextFile> ProcessProperties(ITemplateInfo templateInfo, Func<IEnumerable<OdcmObject>> modelMethod)
        {
            return this.FilterOdcmEnumerable(templateInfo, modelMethod)
                    .Select(property => property as OdcmProperty)
                    .Select(odcmProperty => this.ProcessTemplate(templateInfo, odcmProperty,
                                                                    className: odcmProperty.Class.Name,
                                                                    propertyName: odcmProperty.Name,
                                                                    propertyType: odcmProperty.Projection.Type.Name));
        }

        protected virtual IEnumerable<TextFile> ProcessMethods(ITemplateInfo templateInfo, Func<IEnumerable<OdcmMethod>> methods)
        {
            return this.FilterOdcmEnumerable(templateInfo, methods)
                    .Select(method => method as OdcmMethod)
                    .Select(odcmMethod => this.ProcessTemplate(templateInfo, odcmMethod,
                                                                    className: odcmMethod.Class.Name,
                                                                    methodName: odcmMethod.Name));
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
            var methods = this.CurrentModel.GetMethods().Where(method => method.IsCollection && method.ReturnType != null).ToList();
            return methods;
        }

        protected virtual IEnumerable<OdcmProperty> NavigationCollectionProperties()
        {
            return this.CurrentModel.GetProperties().Where(prop => prop.IsCollection && prop.IsNavigation() && !prop.IsReference());
        }
       
        protected virtual IEnumerable<OdcmProperty> CollectionReferenceProperties()
        {
            return this.CurrentModel.GetProperties().Where(prop => prop.IsReference() && prop.IsCollection);
        }

        protected virtual IEnumerable<OdcmProperty> CollectionProperties()
        {
            return this.CurrentModel.GetProperties().Where(prop => prop.IsCollection && !(prop.Projection.Type is OdcmPrimitiveType) && !prop.IsReference());
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

            var className = templateInfo.TemplateName.Replace(".", "_");
            var assembly = CompileAndLoadTemplateAssembly(templateInfo, templateContent, className);

            var templateClassType = assembly.GetType(RuntimeTemplatesNamespace + "." + className);

            dynamic templateClassInstance = Activator.CreateInstance(templateClassType);
            return (ITextTemplatingEngineHost host) =>
            {
                templateClassInstance.Host = host;
                return templateClassInstance.TransformText();
            };
        }
        private static Dictionary<string, Assembly> cachedAssemblies = new Dictionary<string, Assembly>();

        private Assembly CompileTemplateOrRetrieveFromCache(ITemplateInfo templateInfo, string templateContent, string className)
        {
            if (!cachedAssemblies.ContainsKey(className))
            {
                var assembly = CompileAndLoadTemplateAssembly(templateInfo, templateContent, className);
                cachedAssemblies.Add(className, assembly);
                return assembly;
            }
            return cachedAssemblies[className];
        }

        private Assembly CompileAndLoadTemplateAssembly(ITemplateInfo templateInfo, string templateContent, string className)
        {
            var dummyHost = new CustomT4Host(templateInfo, this.TemplatesDirectory, null, null);
            var generatedCode = this.T4Engine.PreprocessTemplate(templateContent, dummyHost, className, RuntimeTemplatesNamespace, out var language, out var references);

            var syntaxTree = CSharpSyntaxTree.ParseText(generatedCode);

            var refs = new List<PortableExecutableReference>();
            foreach (var reference in ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator))
            {
                refs.Add(MetadataReference.CreateFromFile(reference));
            }
            var compilation = CSharpCompilation.Create($"{templateInfo.TemplateName}.dll",
                                 syntaxTrees: new[] { syntaxTree },
                                 references: refs,
                                 options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var messages = new List<string>();
            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);
                if (!result.Success)
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                         diagnostic.IsWarningAsError ||
                         diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures.OrderBy(o => o.Location.GetLineSpan().StartLinePosition.Line))
                    {
                        messages.Add($"({diagnostic.Location.GetLineSpan().StartLinePosition.Line}) {diagnostic.Id}: {diagnostic.GetMessage()}");
                    }
                    throw new InvalidOperationException("Template error.");
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    var byteAssembly = ms.ToArray();
                    var assembly = Assembly.Load(byteAssembly);
                    messages.Add("Compilation successful");
                    return assembly;
                }
            }
        }

        private TextFile ProcessTemplate(ITemplateInfo templateInfo, OdcmObject odcmObject,
                string className = "", string propertyName = "", string methodName = "", string propertyType = "")
        {
            return this.ProcessTemplate(templateInfo, odcmObject,
                                        templateInfo.BaseFileName(this.CurrentModel.EntityContainer.Name,
                                                                  className,
                                                                  propertyName,
                                                                  methodName,
                                                                  propertyType));
        }

        protected TextFile ProcessTemplate(ITemplateInfo templateInfo, OdcmObject odcmObject, string fileName)
        {
            logger.Trace($"Generating file {fileName}");
            var host = TemplateProcessor.Host(templateInfo, this.TemplatesDirectory, odcmObject, this.CurrentModel);

            Func<ITextTemplatingEngineHost, string> preProcessedTemplate;

            if (!this.preProcessedTemplates.TryGetValue(templateInfo.FullPath, out preProcessedTemplate))
            {
                preProcessedTemplate = this.PreProcessTemplate(templateInfo);
                this.preProcessedTemplates.Add(templateInfo.FullPath, preProcessedTemplate);
            }

            var output = preProcessedTemplate(host);

            if (!string.IsNullOrEmpty(host.TemplateName))
            {
                fileName = host.TemplateName;
            }

            if (host.Errors != null && host.Errors.HasErrors)
            {
                var errors = this.LogErrors(host, templateInfo);
                logger.Error(errors);
                throw new InvalidOperationException(errors);
            }

            string @namespace = ConfigurationService.Settings.PrimaryNamespaceName;
            switch (odcmObject)
            {
                case OdcmType t:
                    @namespace = t.Namespace.GetNamespaceName();
                    break;
                case OdcmProperty p:
                    if (templateInfo.TemplateLanguage.Equals("java", StringComparison.OrdinalIgnoreCase))
                    {
                        @namespace = p.Type.Namespace.Name;
                        if (@namespace == "Edm")
                        {
                            if (p.Type.Name.Equals("Stream", StringComparison.OrdinalIgnoreCase))
                            {
                                @namespace = p.Class.Namespace.Name;
                            }
                            else
                            {
                                @namespace = "Microsoft.Graph";
                            }
                        }
                    }
                    else
                    {
                        @namespace = p?.Class.Namespace.GetNamespaceName();
                    }
                    break;
                default:
                    break;
            }

            var path = this.PathWriter.WritePath(templateInfo, @namespace, fileName);
            logger.Debug("Wrote template to path {0}", path);

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
