using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWriter
{
    public static class ConfigurationService
    {
        private static ITemplateConfiguration _configuration;

        public static void Initialize(ITemplateConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string PrimaryNamespaceName { get { return _configuration.PrimaryNamespaceName; } }
    }
}
