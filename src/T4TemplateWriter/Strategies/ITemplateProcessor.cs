using System;
using System.Collections.Generic;
using T4TemplateWriter.Templates;
using Vipr.Core;

namespace T4TemplateWriter.Strategies
{
    interface ITemplateProcessor
    {
        IDictionary<string, Func<Template, IEnumerable<TextFile>>> Templates { get; set; }
        IEnumerable<TextFile> Process(Template template);
    }
}
