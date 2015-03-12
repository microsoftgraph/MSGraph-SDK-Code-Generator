using System;
using System.Collections.Generic;
using TemplateWriter.Templates;

namespace TemplateWriter.Strategies
{
    interface ITemplateProcessor
    {
        Dictionary<string, Action<Template>> Templates { get; set; }
    }
}
