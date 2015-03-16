using System;
using System.Collections.Generic;
using T4TemplateWriter.Templates;

namespace T4TemplateWriter.Strategies
{
    interface ITemplateProcessor
    {
        Dictionary<string, Action<Template>> Templates { get; set; }
    }
}
