using System.IO;
using T4TemplateWriter.Settings;
using T4TemplateWriter.Templates;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Output
{
    class ObjectiveCPathWriter : BasePathWriter
    {
        public ObjectiveCPathWriter(OdcmModel model, TemplateWriterSettings configuration)
            : base(model, configuration)
        {
        }

        public new string FileExtension { get; set; }

        public override string WritePath(Template template, string fileName)
        {
            var destPath = string.Format("{0}{1}", ConfigurationService.Settings.OutputDirectory, Path.DirectorySeparatorChar);
            var identifier = FileName(template, fileName);
            FileExtension = template.ResourceName.Contains("header") ? ".h" : ".m";

            if (!DirectoryExists(destPath))
            {
                CreateDirectory(destPath);
            }

            var fullPath = Path.Combine(destPath, template.FolderName);

            if (!DirectoryExists(fullPath))
            {
                CreateDirectory(fullPath);
            }

            var filePath = Path.Combine(fullPath, string.Format("{0}{1}", identifier, FileExtension));
            return filePath;
        }

        protected override string FileName(Template template, string identifier)
        {
            return template.Name.Contains("Entity")
                ? (template.FolderName == "odata"
                    ? template.Name.Replace("Entity", identifier)
                    : identifier)
                    : identifier;
        }
    }
}