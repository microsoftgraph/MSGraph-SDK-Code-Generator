using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWriter
{
    public static class ConfigurationService
    {
        private static ITemplateConfiguration _configuration;

        static ConfigurationService()
        {
            _configuration = new StubConfiguration();
        }

        public static void Initialize(ITemplateConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string PrimaryNamespaceName { get { return _configuration.PrimaryNamespaceName; } }
    }


    public class StubConfiguration : ITemplateConfiguration
    {
        public string PrimaryNamespaceName { get; set; }
        public IReadOnlyDictionary<string, string> Parameters { get; set; }
        public HashSet<string> Languages { get; set; }

        public string NamespacePrefix { get; set; }

        public StubConfiguration()
        {
            PrimaryNamespaceName = "Microsoft.OutlookServices";
            Languages = new HashSet<string> { "java", "objectivec" };
            NamespacePrefix = "com";
        }
    }
}
