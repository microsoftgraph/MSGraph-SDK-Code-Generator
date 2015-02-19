using System;
using System.Collections.Generic;
using TemplateWriter;

namespace Vipr.CLI
{
    public interface ITemplateTempLocationFileWriter
    {
        IList<Template> WriteUsing(Type sourceType, BuilderArguments arguments);
    }
}