/*
# Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
# Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿
*/

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using T4TemplateWriter.Extensions;
using T4TemplateWriter.Output;
using T4TemplateWriter.Templates;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Strategies
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

        protected readonly IPathWriter PathWriter;
        protected readonly Engine Engine;
        protected readonly OdcmModel Model;

        private static CustomHost _hostIntance;

        public IDictionary<string, Func<Template, IEnumerable<TextFile>>> Templates { get; set; }

        public IEnumerable<TextFile> Process(Template template)
        {
            Func<Template, IEnumerable<TextFile>> action;
            return Templates.TryGetValue(template.Name, out action) ? action(template) : null;
        }

        public string BaseFilePath { get; private set; }

        public BaseTemplateProcessor(IPathWriter pathWriter, OdcmModel model, string baseFilePath)
        {
            Engine = new Engine();
            Model = model;
            PathWriter = pathWriter;
            BaseFilePath = baseFilePath;

            Templates = new Dictionary<string, Func<Template, IEnumerable<TextFile>>>(StringComparer.InvariantCultureIgnoreCase)
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

        private IEnumerable<TextFile> CreateEntryPoint(Template template)
        {
            var container = Model.EntityContainer;
            return ProcessTemplate(template, container);
        }

        private IEnumerable<TextFile> BaseEntity(Template template)
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

            var path = PathWriter.WritePath(template, template.Name.ToCheckedCase());
            return new TextFile(path, output).ToIEnumerable();
        }

        private IEnumerable<TextFile> EnumTypes(Template template)
        {
            var enums = Model.GetEnumTypes();
            return ProcessingAction(enums, template);
        }

        public IEnumerable<TextFile> ComplexTypes(Template template)
        {
            var complexTypes = Model.GetComplexTypes();
            return ProcessingAction(complexTypes, template);
        }

        public IEnumerable<TextFile> EntityTypes(Template template)
        {
            var entityTypes = Model.GetEntityTypes();
            return ProcessingAction(entityTypes, template);
        }

        public IEnumerable<TextFile> ProcessingAction(IEnumerable<OdcmObject> source, Template template)
        {
            return source.SelectMany(complexType => ProcessTemplate(template, complexType));
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

        protected virtual IEnumerable<TextFile> ProcessTemplate(Template template, OdcmObject odcmObject)
        {
            var host = GetCustomHost(template, odcmObject);

            var templateContent = File.ReadAllText(host.TemplateFile);
            var output = Engine.ProcessTemplate(templateContent, host);

            if (host.Errors != null && host.Errors.HasErrors)
            {
                var errors = LogErrors(host, template);
                throw new InvalidOperationException(errors);
            }

            var path = PathWriter.WritePath(template, odcmObject.Name.ToCheckedCase());
            return new TextFile(path, output).ToIEnumerable();
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
