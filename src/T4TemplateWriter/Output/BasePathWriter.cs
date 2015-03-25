using System.IO;
using T4TemplateWriter.Settings;
using T4TemplateWriter.Templates;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Output
{
    public class BasePathWriter : IPathWriter
    {
        protected readonly OdcmModel Model;
        protected readonly TemplateWriterSettings Configuration;

        public string FileExtension
        {
            get { return ".txt"; }
        }

        public BasePathWriter(OdcmModel model, TemplateWriterSettings configuration)
        {
            Model = model;
            Configuration = configuration;
        }

        protected virtual string FileName(Template template, string identifier)
        {
            return template.FolderName == "odata" ? template.Name.Replace("Entity", identifier)
                                                  : identifier;
        }

        public virtual string WritePath(Template template, string fileName)
        {
            var destPath = string.Format("{0}{1}", ConfigurationService.Settings.OutputDirectory, Path.DirectorySeparatorChar);
            var identifier = FileName(template, fileName);
            var filePath = Path.Combine(destPath, string.Format("{0}{1}", identifier, FileExtension));
            return filePath;
        }

        /// <summary>
        /// Creates a directory structure based on the given parameter
        /// </summary>
        /// <param name="directoryPath"></param>
        public void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
        }

        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
    }
}