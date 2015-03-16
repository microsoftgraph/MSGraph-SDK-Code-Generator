using System;
using System.Collections.Generic;
using T4TemplateWriter.Settings;

namespace T4TemplateWriter.Templates
{
    public interface ITemplateSourceReader
    {
        IList<Template> Read(Type targetType, TemplateWriterSettings config);
    }
}