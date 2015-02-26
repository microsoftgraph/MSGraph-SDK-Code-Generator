using System;
using System.Collections.Generic;

namespace TemplateWriter.Strategies
{
    interface ITemplateProcessor
    {
        Dictionary<string, Action<Template>> Templates { get; set; }
    }
}
