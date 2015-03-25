using System.IO;
using System.Linq;
using T4TemplateWriter.Settings;
using T4TemplateWriter.Templates;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Output
{
    public class JavaPathWriter : BasePathWriter
    {
        public JavaPathWriter(OdcmModel model, TemplateWriterSettings configuration)
            : base(model, configuration)
        {
        }

        public new string FileExtension
        {
            get { return ".java"; }
        }

        public override string WritePath(Template template, string fileName)
        {
            // Originally we specified an output folder in the local TemplateWriterSettings.json
            // config file. But now, we will rely on --outputPath from CLI instead.
            // So here, we only build up the part of the path required for the source code.
            var @namespace = template.TemplateType == TemplateType.Model ? CreateNamespace(string.Empty).ToLower()
                                                                         : CreateNamespace(template.FolderName).ToLower();

            var pathFromNamespace = CreatePathFromNamespace(@namespace);
            var identifier = FileName(template, fileName);

            var filePath = Path.Combine(pathFromNamespace, string.Format("{0}{1}", identifier, FileExtension));
            return filePath;
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