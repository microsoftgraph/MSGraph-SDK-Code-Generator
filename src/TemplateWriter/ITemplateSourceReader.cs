using System;
using System.Collections.Generic;

namespace TemplateWriter
{
    public interface ITemplateSourceReader
    {
        IList<Template> Read(Type targetType, BuilderArguments arguments);
    }
}