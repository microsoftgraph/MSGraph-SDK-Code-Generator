using System.Collections.Generic;
using TemplateWriter;

namespace CliTemplateWriterTests
{
    public class GraphConfiguration : ITemplateConfiguration
    {
        public string PrimaryNamespaceName { get; set; }
        public IReadOnlyDictionary<string, string> Parameters { get; set; }
        public HashSet<string> Languages { get; set; }

        public string NamespacePrefix { get; set; }

        public GraphConfiguration()
        {
            PrimaryNamespaceName = "Microsoft.Graph";
            Languages = new HashSet<string> { "java", "objectivec" };
            NamespacePrefix = "com";
        }
    }
}