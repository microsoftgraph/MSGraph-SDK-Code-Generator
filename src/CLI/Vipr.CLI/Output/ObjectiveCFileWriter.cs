using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateWriter;
using Vipr.Core.CodeModel;

namespace Vipr.CLI.Output
{
	class ObjectiveCFileWriter : BaseFileWriter
	{
		public ObjectiveCFileWriter(OdcmModel model, IConfigArguments configuration) : base(model, configuration)
		{
		}

		public new string FileExtension { get; set; }

		public override void WriteText(Template template, string fileName, string text)
		{
			var destPath = string.Format("{0}{1}", Path.DirectorySeparatorChar, Configuration.BuilderArguments.OutputDir);

			var identifier = FileName(template, fileName);

			FileExtension = template.ResourceName.Contains("header") ? ".h" : ".m";

			var fullPath = Path.Combine(destPath, destPath);
			var filePath = Path.Combine(fullPath, string.Format("{0}{1}", identifier, FileExtension));

			if (!DirectoryExists(fullPath))
				CreateDirectory(fullPath);

			using (var writer = new StreamWriter(filePath, false, Encoding.ASCII))
			{
				writer.Write(text);
			}
		}
	}
}	