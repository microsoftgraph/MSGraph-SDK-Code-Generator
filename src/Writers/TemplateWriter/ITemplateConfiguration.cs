using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWriter
{
    public interface ITemplateConfiguration
    {
        string PrimaryNamespaceName { get; set; }

        IReadOnlyDictionary<string, string> Parameters { get; set; }

        HashSet<string> Languages { get; set; }
        string NamespacePrefix { get; set; }
    }
}
