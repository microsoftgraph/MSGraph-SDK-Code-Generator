using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using TemplateWriter.Output;
using TemplateWriter.Settings;
using TemplateWriter.Templates;
using Vipr.Core.CodeModel;

namespace TemplateWriter.Strategies
{
    public class BaseTemplateProcessor : ITemplateProcessor
    {
        public string StrategyName = "default";

        public const string ComplexType = "ComplexType";
        public const string EntityType = "EntityType";
        public const string EnumType = "EnumType";
        public const string ODataBaseEntity = "ODataBaseEntity";
        public const string EntityCollectionOperation = "EntityCollectionOperations";
        public const string EntityFetcher = "EntityFetcher";
        public const string EntityOperations = "EntityOperations";
        public const string EntryPoint = "EntityClient";

        protected readonly IFileWriter FileWriter;
        protected readonly Engine Engine;
        protected readonly OdcmModel Model;

        private static CustomHost _hostIntance;

        public Dictionary<string, Action<Template>> Templates { get; set; }

        public string BaseFilePath { get; private set; }

        public BaseTemplateProcessor(IFileWriter fileWriter, OdcmModel model, string baseFilePath)
        {
            Engine = new Engine();
            Model = model;
            FileWriter = fileWriter;
            BaseFilePath = baseFilePath;

            Templates = new Dictionary<string, Action<Template>>(StringComparer.InvariantCultureIgnoreCase)
            {
                //Model
                {EntityType, EntityTypes},
                {ComplexType, ComplexTypes},
                {EnumType, EnumTypes},
                {ODataBaseEntity, BaseEntity},
                //OData
                {EntityCollectionOperation, EntityTypes},
                {EntityFetcher, EntityTypes},
                {EntityOperations, EntityTypes},
                //EntityContainer
                {EntryPoint, CreateEntryPoint},
            };
        }

        private void CreateEntryPoint(Template template)
        {
            var container = Model.EntityContainer;
            ProcessTemplate(template, container);
        }

        private void BaseEntity(Template template)
        {
            var host = new CustomHost(StrategyName, null)
            {
                TemplateFile = template.Path,
                Model = Model,
                BaseTemplatePath = BaseFilePath
            };

            var templateContent = File.ReadAllText(host.TemplateFile);
            var output = Engine.ProcessTemplate(templateContent, host);

            if (host.Errors != null && host.Errors.HasErrors)
            {
                var errors = LogErrors(host, template);
                throw new InvalidOperationException(errors);
            }

            FileWriter.WriteText(template, template.Name.ToCheckedCase(), output);
        }

        private void EnumTypes(Template template)
        {
            var enums = Model.GetEnumTypes();
            ProcessingAction(enums, template);
        }

        public void ComplexTypes(Template template)
        {
            var complexTypes = Model.GetComplexTypes();
            ProcessingAction(complexTypes, template);
        }

        public void EntityTypes(Template template)
        {
            var entityTypes = Model.GetEntityTypes();
            ProcessingAction(entityTypes, template);
        }

        public void ProcessingAction(IEnumerable<OdcmObject> source, Template template)
        {
            foreach (var complexType in source)
            {
                ProcessTemplate(template, complexType);
            }
        }

        protected CustomHost GetCustomHost(Template template, OdcmObject odcmObject)
        {
            if (_hostIntance == null)
                _hostIntance = new CustomHost(StrategyName, odcmObject);

            _hostIntance.BaseTemplatePath = BaseFilePath;
            _hostIntance.TemplateFile = template.Path;
            _hostIntance.Model = Model;
            _hostIntance.OdcmType = odcmObject;

            return _hostIntance;
        }

        protected virtual void ProcessTemplate(Template template, OdcmObject odcmObject)
        {
            var host = GetCustomHost(template, odcmObject);

            var templateContent = File.ReadAllText(host.TemplateFile);
            var output = Engine.ProcessTemplate(templateContent, host);

            if (host.Errors != null && host.Errors.HasErrors)
            {
                var errors = LogErrors(host, template);
                throw new InvalidOperationException(errors);
            }
            FileWriter.WriteText(template, odcmObject.Name.ToCheckedCase(), output);
        }

        public string LogErrors(CustomHost host, Template template)
        {
            var sb = new StringBuilder();
            if (host.Errors == null || host.Errors.Count <= 0) return sb.ToString();
            foreach (CompilerError error in host.Errors)
            {
                sb.AppendLine("Error template name: " + template.Name);
                sb.AppendLine("Error template" + host.TemplateFile);
                sb.AppendLine(error.ErrorText);
                sb.AppendLine("In line: " + error.Line);
                sb.AppendLine(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}