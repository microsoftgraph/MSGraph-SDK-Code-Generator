using System;
using System.Collections.Generic;
using TemplateWriter;

namespace Vipr.CLI
{
    public interface ITemplateSourceReader
    {
        IList<Template> Read(Type targetType, BuilderArguments arguments);
    }
}