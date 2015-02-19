using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateWriter;

namespace Vipr.CLI.Strategies
{
    interface ITemplateProcessor
    {
        Dictionary<string, Action<Template>> Templates { get; set; }
    }
}
