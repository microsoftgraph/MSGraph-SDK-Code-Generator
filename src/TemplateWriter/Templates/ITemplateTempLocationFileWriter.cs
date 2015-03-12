using System;
using System.Collections.Generic;
using TemplateWriter.Settings;

namespace TemplateWriter.Templates
{
    public interface ITemplateTempLocationFileWriter
    {
        IList<Template> WriteUsing(Type sourceType, TemplateWriterSettings config);
    }
}