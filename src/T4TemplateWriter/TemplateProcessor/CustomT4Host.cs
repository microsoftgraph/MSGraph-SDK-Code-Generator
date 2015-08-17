// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio.TextTemplating;

using Vipr.T4TemplateWriter.TemplateProcessor;
using Vipr.T4TemplateWriter.Settings;
using Vipr.Core.CodeModel;
using Vipr.T4TemplateWriter.CodeHelpers;

namespace Vipr.T4TemplateWriter
{
    public class CustomT4Host : ITextTemplatingEngineHost
    {
        // see https://msdn.microsoft.com/en-us/library/bb126579(v=vs.110).aspx

        public CustomT4Host(ITemplateInfo templateInfo, String templatesDirectory, OdcmObject currentType, OdcmModel currentModel)
        {
            this.Reset(templateInfo, templatesDirectory, currentType, currentModel);
        }

        public void Reset(ITemplateInfo templateInfo, String templatesDirectory, OdcmObject currentType, OdcmModel currentModel)
        {
            this.TemplateFile = templateInfo.FullPath;
            this.TemplatesDirectory = templatesDirectory;
            this.CurrentType = currentType;
            this.CurrentModel = currentModel;
        }
        public String TemplateFile { get; set; }
        public String TemplatesDirectory { get; set; }
        public OdcmObject CurrentType { get; set; }
        public OdcmModel CurrentModel { get; set; }

        private CodeWriterBase _codeWriter;
        public CodeWriterBase CodeWriter
        {
            get
            {
                if (_codeWriter == null)
                {
                    String writerClassName = String.Format("Vipr.T4TemplateWriter.CodeHelpers.{0}.CodeWriter{0}",
                        ConfigurationService.Settings.TargetLanguage);
                    _codeWriter = (CodeWriterBase)Activator.CreateInstance(Type.GetType(writerClassName), new object[] { this.CurrentModel });
                }
                return _codeWriter;
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
            get { return _fileEncoding; }
            set { _fileEncoding = value; }
        }

        public CompilerErrorCollection Errors { get; private set; }

        private List<String> _standardAssemblyReferences = new List<string>() {
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

        private List<String> _standardImports = new List<String>() {
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
            if (!_standardAssemblyReferences.Contains(location))
            {
                _standardAssemblyReferences.Add(assembly.Location);
            }
        }

        public IList<String> StandardAssemblyReferences { get { return _standardAssemblyReferences; } }

        public IList<String> StandardImports { get { return _standardImports; } }

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

            var candidates = Directory.GetFiles(TemplatesDirectory, "*" + requestFileName + "*", SearchOption.AllDirectories);

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

            string candidate = Path.Combine(Path.GetDirectoryName(TemplateFile), fileName);
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
            Errors = errors;
        }

        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            return AppDomain.CurrentDomain;
            // return AppDomain.CreateDomain("Generation App Domain");
        }
    }
}
