using System.IO;
using System.Linq;
using System.Text;
using Vipr.Core.CodeModel;

namespace TemplateWriter.Output
{
    public class JavaFileWriter : BaseFileWriter
    {
        public JavaFileWriter(OdcmModel model, IConfigArguments configuration)
            : base(model, configuration)
        {
        }

        public new string FileExtension
        {
            get { return ".java"; }
        }

        public override void WriteText(Template template, string fileName, string text)
        {
            var destPath = string.Format("{0}{1}", Path.DirectorySeparatorChar, Configuration.BuilderArguments.OutputDir);

            var @namespace = template.TemplateType == TemplateType.Model ? CreateNamespace(string.Empty).ToLower()
                                                                         : CreateNamespace(template.FolderName).ToLower();

            var pathFromNamespace = CreatePathFromNamespace(@namespace);
            var identifier = FileName(template, fileName);
            var fullPath = Path.Combine(destPath, pathFromNamespace);
            var filePath = Path.Combine(fullPath, string.Format("{0}{1}", identifier, FileExtension));

            using (var writer = new StreamWriter(filePath, false, Encoding.ASCII))
            {
                writer.Write(text);
            }
        }

        private string CreateNamespace(string folderName)
        {
            var @namespace = Model.GetNamespace();
            var prefix = Configuration.TemplateConfiguration.NamespacePrefix;

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
                                  current + string.Format("{0}{1}", Path.DirectorySeparatorChar, path));

            if (!DirectoryExists(destinationPath))
            {
                CreateDirectory(destinationPath);
            }

            return destinationPath;
        }
    }
}