using System;
using System.Collections.Generic;
using System.IO;
using T4TemplateWriter.Settings;

namespace T4TemplateWriter.Templates
{
    public class TemplateTempLocationFileWriter : ITemplateTempLocationFileWriter
    {
        private readonly ITemplateSourceReader _templateSourceReader;

        public TemplateTempLocationFileWriter(ITemplateSourceReader templateSourceReader)
        {
            _templateSourceReader = templateSourceReader;
        }

        public IList<Template> WriteUsing(Type sourceType, TemplateWriterSettings config)
        {
            var writtenTemplates = new List<Template>();
            var templates = _templateSourceReader.Read(sourceType, config);

            foreach (var template in templates)
            {
                var fullpath = Path.GetTempFileName();
                using (var stream = sourceType.Assembly.GetManifestResourceStream(template.ResourceName))
                {
                    if (stream != null)
                    {
                        CopyStream(stream, fullpath);
                    }
                }
                template.Path = fullpath;
                writtenTemplates.Add(template);
            }
            return writtenTemplates;
        }

        private void CopyStream(Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }
    }
}