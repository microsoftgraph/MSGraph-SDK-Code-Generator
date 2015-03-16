using System;
using System.Collections.Generic;
using T4TemplateWriter.Settings;

namespace T4TemplateWriter.Templates
{
    public interface ITemplateTempLocationFileWriter
    {
        IList<Template> WriteUsing(Type sourceType, TemplateWriterSettings config);
    }
}