using System;
using System.Collections.Generic;

namespace TemplateWriter
{
    public interface ITemplateTempLocationFileWriter
    {
        IList<Template> WriteUsing(Type sourceType, BuilderArguments arguments);
    }
}