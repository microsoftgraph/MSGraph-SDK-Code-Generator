// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.TemplateProcessor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Microsoft.Graph.ODataTemplateWriter.CodeHelpers;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.VisualStudio.TextTemplating;
    using Vipr.Core.CodeModel;

    public class CustomT4Host : ITextTemplatingEngineHost
    {
        // see https://msdn.microsoft.com/en-us/library/bb126579(v=vs.110).aspx

        public CustomT4Host(ITemplateInfo templateInfo, String templatesDirectory, OdcmObject currentType, OdcmModel currentModel)
        {
            this.TemplateHostStats = new TemplateHostStats();
            this.Reset(templateInfo, templatesDirectory, currentType, currentModel);
        }

        public void Reset(ITemplateInfo templateInfo, String templatesDirectory, OdcmObject currentType, OdcmModel currentModel)
        {
            this.TemplateFile = templateInfo.FullPath;
            this.TemplatesDirectory = templatesDirectory;
            this.CurrentType = currentType;
            this.CurrentModel = currentModel;
            this.TemplateName = null;
            this.TemplateInfo = templateInfo;
        }
        public String TemplateFile { get; set; }
        public String TemplatesDirectory { get; set; }
        public OdcmObject CurrentType { get; set; }
        public OdcmModel CurrentModel { get; set; }
        public string TemplateName { get; set; }
        internal TemplateHostStats TemplateHostStats { get; set; }

        public ITemplateInfo TemplateInfo { get; set; }

        private CodeWriterBase _codeWriter;
        public CodeWriterBase CodeWriter
        {
            get
            {
                if (this._codeWriter == null)
                {
                    String writerClassName = String.Format("Vipr.T4TemplateWriter.CodeHelpers.{0}.CodeWriter{0}",
                        ConfigurationService.Settings.TargetLanguage);
                    this._codeWriter = (CodeWriterBase)Activator.CreateInstance(Type.GetType(writerClassName), new object[] { this.CurrentModel });
                }
                return this._codeWriter;
            }
        }
        public String Language
        {
            get
            {
                return ConfigurationService.Settings.TargetLanguage;
            }
        }

        /*
         *  ITextTemplatingEngineHost 
         */
        public String FileExtension { get; set; }

        private Encoding _fileEncoding = Encoding.UTF8;
        public Encoding FileEncoding
        {
            get { return this._fileEncoding; }
            set { this._fileEncoding = value; }
        }

        public CompilerErrorCollection Errors { get; private set; }

        private readonly List<String> _standardAssemblyReferences = new List<string>() {
              Assembly.GetExecutingAssembly().Location,
              typeof(List<>).Assembly.Location,
              typeof(Uri).Assembly.Location,
              typeof(System.Reflection.Binder).Assembly.Location,
              typeof(Microsoft.CSharp.RuntimeBinder.Binder).Assembly.Location,
              typeof(IDynamicMetaObjectProvider).Assembly.Location,
              typeof(ITextTemplatingEngineHost).Assembly.Location,
              typeof(OdcmClass).Assembly.Location,
              typeof(CustomT4Host).Assembly.Location
        };

        private readonly List<String> _standardImports = new List<String>() {
               "System",
               "System.Linq",
               "System.Text",
               "System.Collections.Generic",
               "System.Dynamic",
               "Microsoft.CSharp.RuntimeBinder",
               "Vipr.Core.CodeModel",
               "Vipr.T4TemplateWriter",
               "Vipr.T4TemplateWriter.Extensions",
               "Vipr.T4TemplateWriter.Settings",
               "Vipr.T4TemplateWriter.CodeHelpers." + ConfigurationService.Settings.TargetLanguage
        };


        public void AddAssemblyReference(Assembly assembly)
        {
            var location = assembly.Location;
            if (!this._standardAssemblyReferences.Contains(location))
            {
                this._standardAssemblyReferences.Add(assembly.Location);
            }
        }

        public IList<String> StandardAssemblyReferences { get { return this._standardAssemblyReferences; } }

        public IList<String> StandardImports { get { return this._standardImports; } }

        public bool LoadIncludeText(string requestFileName, out string content, out string location)
        {
            location = String.Empty;
            content = String.Empty;

            if (File.Exists(requestFileName))
            {
                location = requestFileName;
                content = File.ReadAllText(location);
                return true;
            }

            var candidates = Directory.GetFiles(this.TemplatesDirectory, "*" + requestFileName + "*", SearchOption.AllDirectories);

            if (candidates.Length >= 1)
            {
                foreach(var canidate in candidates)
                {
                    var canidateFileName = Path.GetFileName(canidate);
                    if (canidateFileName.Equals(requestFileName))
                    {
                        location = canidate;
                        content = File.ReadAllText(location);
                        return true;
                    }
                }
            }

            else if (candidates.Length < 1)
            {
                // file not found
            }

            return false;
        }


        public Object GetHostOption(String optionName)
        {
            object returnObject;
            switch (optionName)
            {
                case "CacheAssemblies":
                    returnObject = true;
                    break;
                default:
                    returnObject = null;
                    break;
            }

            return returnObject;
        }

        public string ResolveAssemblyReference(string assemblyReference)
        {
            if (File.Exists(assemblyReference))
            {
                return assemblyReference;
            }

            string candidate = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assemblyReference);

            if (File.Exists(candidate))
            {
                return candidate;
            }

            return string.Empty;
        }

        public Type ResolveDirectiveProcessor(string processorName)
        {
            // No unique directive processors provided.
            throw new Exception("Directive Processor not found");
        }

        public string ResolvePath(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("the file name cannot be null");
            }

            if (File.Exists(fileName))
            {
                return fileName;
            }

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), fileName);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            return fileName;
        }

        public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            if (directiveId == null)
            {
                throw new ArgumentNullException("the directiveId cannot be null");
            }
            if (processorName == null)
            {
                throw new ArgumentNullException("the processorName cannot be null");
            }
            if (parameterName == null)
            {
                throw new ArgumentNullException("the parameterName cannot be null");
            }
            return String.Empty;
        }

        public void SetFileExtension(string extension)
        {
            this.FileExtension = extension;
        }

        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
            this.FileEncoding = encoding;
        }

        public void LogErrors(CompilerErrorCollection errors)
        {
            this.Errors = errors;
        }

        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            return AppDomain.CurrentDomain;
            // return AppDomain.CreateDomain("Generation App Domain");
        }
    }
}
