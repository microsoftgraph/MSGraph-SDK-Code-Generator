using System.IO;
using System.Linq;
using System.Text;
using T4TemplateWriter.Settings;
using T4TemplateWriter.Templates;
using TemplateWriter;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Output
{
    public class JavaFileWriter : BaseFileWriter
    {
        public JavaFileWriter(OdcmModel model, TemplateWriterSettings configuration)
            : base(model, configuration)
        {
        }

        public new string FileExtension
        {
            get { return ".java"; }
        }

        public override void WriteText(Template template, string fileName, string text)
        {
            // var destPath = string.Format("{0}{1}", Path.DirectorySeparatorChar, Configuration.OutputDirectory);
            var destPath = ConfigurationService.Settings.OutputDirectory;
            var @namespace = template.TemplateType == TemplateType.Model ? CreateNamespace(string.Empty).ToLower()
                                                                         : CreateNamespace(template.FolderName).ToLower();

            var pathFromNamespace = CreatePathFromNamespace(@namespace);
            var identifier = FileName(template, fileName);
            var fullPath = Path.Combine(destPath, pathFromNamespace);

            if (!DirectoryExists(fullPath)) {
                CreateDirectory(fullPath);
            }
            
            var filePath = Path.Combine(fullPath, string.Format("{0}{1}", identifier, FileExtension));

            using (var writer = new StreamWriter(filePath, false, Encoding.ASCII))
            {
                writer.Write(text);
            }
        }

        private string CreateNamespace(string folderName)
        {
            var @namespace = Model.GetNamespace();
            var prefix = Configuration.NamespacePrefix;

            if (string.IsNullOrEmpty(folderName))
            {
                return string.IsNullOrEmpty(prefix) ? @namespace
                                                    : string.Format("{0}.{1}", prefix, @namespace);
            }

            return string.IsNullOrEmpty(prefix) ? string.Format("{0}.{1}", @namespace, folderName)
                                                : string.Format("{0}.{1}.{2}", prefix, @namespace, folderName);
        }

        private string CreatePathFromNamespace(string @namespace)
        {
            var splittedPaths = @namespace.Split('.');

            var destinationPath = splittedPaths.Aggregate(string.Empty, (current, path) =>
                                  current + string.Format("{0}{1}", path, Path.DirectorySeparatorChar));



            return destinationPath;
        }
    }
}