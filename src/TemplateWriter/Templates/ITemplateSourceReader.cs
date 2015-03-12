using System;
using System.Collections.Generic;
using TemplateWriter.Settings;

namespace TemplateWriter.Templates
{
    public interface ITemplateSourceReader
    {
        IList<Template> Read(Type targetType, TemplateWriterSettings config);
    }
}