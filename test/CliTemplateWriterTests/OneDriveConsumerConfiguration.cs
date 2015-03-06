using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateWriter;

namespace CliTemplateWriterTests
{
    public class OneDriveConsumerConfiguration : ITemplateConfiguration
    {
        public string PrimaryNamespaceName { get; set; }

        public IReadOnlyDictionary<string, string> Parameters { get; set; }

        public HashSet<string> Languages { get; set; }

        public string NamespacePrefix { get; set; }

        public OneDriveConsumerConfiguration()
        {
            PrimaryNamespaceName = "oneDrive";
            Languages = new HashSet<string> { "java", "objectivec" };
            NamespacePrefix = "com";
        }
    }
}
