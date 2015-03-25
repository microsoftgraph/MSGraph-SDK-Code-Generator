using System;
using System.Collections.Generic;
using System.IO;
using T4TemplateWriter.Extensions;
using T4TemplateWriter.Output;
using T4TemplateWriter.Templates;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Strategies
{
    public class ObjectiveCTemplateProcessor : BaseTemplateProcessor
    {
        public ObjectiveCTemplateProcessor(IPathWriter pathWriter, OdcmModel model, string baseFilePath)
            : base(pathWriter, model, baseFilePath)
        {
            StrategyName = "ObjectiveC";
            Templates.Add("Models", ProcessSimpleFile);
            Templates.Add("Protocols", ProcessSimpleFile);
            Templates.Add("ODataEntities", ProcessSimpleFile);
            Templates.Add("EntityCollectionFetcher", EntityTypes);
            Templates.Add("EntryPoint", ProcessEntryPoint);
        }

        IEnumerable<TextFile> ProcessSimpleFile(Template template)
        {
            return ProcessTemplate(template, null);
        }

        IEnumerable<TextFile> ProcessEntryPoint(Template template)
        {
            var host = GetCustomHost(template, Model.EntityContainer);

            var templateContent = File.ReadAllText(host.TemplateFile);
            var output = Engine.ProcessTemplate(templateContent, host);

            if (host.Errors != null && host.Errors.HasErrors)
            {
                var errors = LogErrors(host, template);
                throw new InvalidOperationException(errors);
            }

            //PathWriter.WritePath(template, string.Format("{0}{1}{2}", "MS",
            //host.Model.EntityContainer.Name, "Client"), output);

            return new TextFile("", output).ToIEnumerable();
        }

        protected override IEnumerable<TextFile> ProcessTemplate(Template template, OdcmObject odcmObject)
        {
            var host = GetCustomHost(template, odcmObject);

            var templateContent = File.ReadAllText(host.TemplateFile);
            var output = Engine.ProcessTemplate(templateContent, host);

            if (host.Errors != null && host.Errors.HasErrors)
            {
                var errors = LogErrors(host, template);
                throw new InvalidOperationException(errors);
            }

            PathWriter.WritePath(template, string.Format("{0}{1}{2}", "MS",
                //TODO: Prefix should be in the configuration
                host.Model.EntityContainer.Name, odcmObject == null
                ? template.Name : odcmObject.Name));

            return new TextFile("", output).ToIEnumerable();
        }
    }
}