using System;
using System.IO;
using TemplateWriter.Output;
using TemplateWriter.Templates;
using Vipr.Core.CodeModel;

namespace TemplateWriter.Strategies
{
	public class ObjectiveCTemplateProcessor : BaseTemplateProcessor
	{
		public ObjectiveCTemplateProcessor(IFileWriter fileWriter, OdcmModel model, string baseFilePath)
			: base(fileWriter, model, baseFilePath)
		{
			StrategyName = "ObjectiveC";
			Templates.Add("Models", ProcessSimpleFile);
			Templates.Add("Protocols", ProcessSimpleFile);
			Templates.Add("ODataEntities", ProcessSimpleFile);
			Templates.Add("EntityCollectionFetcher", EntityTypes);
			Templates.Add("EntryPoint", ProcessEntryPoint);
		}

		void ProcessSimpleFile(Template template)
		{
			ProcessTemplate(template, null);
		}

		void ProcessEntryPoint(Template template)
		{
			var host = GetCustomHost(template, Model.EntityContainer);

			var templateContent = File.ReadAllText(host.TemplateFile);
			var output = Engine.ProcessTemplate(templateContent, host);

			if (host.Errors != null && host.Errors.HasErrors)
			{
				var errors = LogErrors(host, template);
				throw new InvalidOperationException(errors);
			}

			FileWriter.WriteText(template, string.Format("{0}{1}{2}", "MS",
				host.Model.EntityContainer.Name, "Client"), output);
		}

		protected override void ProcessTemplate(Template template, OdcmObject odcmObject)
		{
			var host = GetCustomHost(template, odcmObject);

			var templateContent = File.ReadAllText(host.TemplateFile);
			var output = Engine.ProcessTemplate(templateContent, host);

			if (host.Errors != null && host.Errors.HasErrors)
			{
				var errors = LogErrors(host, template);
				throw new InvalidOperationException(errors);
			}

			FileWriter.WriteText(template, string.Format("{0}{1}{2}", "MS", 
				//TODO: Prefix should be in the configuration
				host.Model.EntityContainer.Name, odcmObject == null 
				? template.Name : odcmObject.Name ) , output);
		}
	}
}