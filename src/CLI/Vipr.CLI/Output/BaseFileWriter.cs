using System.IO;
using System.Text;
using TemplateWriter;
using Vipr.Core.CodeModel;

namespace Vipr.CLI.Output
{
    public class BaseFileWriter : IFileWriter
    {
        protected readonly OdcmModel Model;
        protected readonly IConfigArguments Configuration;

        public string FileExtension
        {
            get { return ".txt"; }
        }

        public BaseFileWriter(OdcmModel model, IConfigArguments configuration)
        {
            Model = model;
            Configuration = configuration;
        }

        protected static string FileName(Template template, string identifier)
        {
            return template.FolderName == "odata" ? template.Name.Replace("Entity", identifier)
                                                  : identifier;
        }

        public virtual void WriteText(Template template, string fileName, string text)
        {
            var destPath = string.Format("{0}{1}", Path.DirectorySeparatorChar, Configuration.BuilderArguments.OutputDir);
            var identifier = FileName(template, fileName);
            var fullPath = Path.Combine(destPath, destPath);
            var filePath = Path.Combine(fullPath, string.Format("{0}{1}", identifier, FileExtension));

            using (var writer = new StreamWriter(filePath, false, Encoding.ASCII))
            {
                writer.Write(text);
            }
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