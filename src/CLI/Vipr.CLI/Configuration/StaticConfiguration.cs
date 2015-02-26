using System.Collections.Generic;
using TemplateWriter;

namespace Vipr.CLI.Configuration
{
    class StaticConfiguration : ITemplateConfiguration
    {
        public string PrimaryNamespaceName { get; set; }
        public IReadOnlyDictionary<string, string> Parameters { get; set; }
        public HashSet<string> Languages { get; set; }
        public string NamespacePrefix { get; set; }
        public StaticConfiguration()
        {
            Languages = new HashSet<string>();
        }
    }
}
