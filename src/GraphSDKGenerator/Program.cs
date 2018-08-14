using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
using NLog;
using Vipr.Core;
using Vipr.Core.CodeModel;
using Vipr.Reader.OData.v4;

namespace GraphSDKGenerator
{
    class Program
    {
        internal static Logger Logger => LogManager.GetLogger("GraphSDKGenerator");

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(opts => GenerateSDK(opts))
                .WithNotParsed((errs) => HandleError(errs));
        }

        private static void GenerateSDK(Options opts)
        {
            var edmxContents = MetadataResolver.GetMetadata(opts.Metadata);

            // fix edmx

            // filter out problem workloads

            var files = MetadataToClientSource(edmxContents, opts.Language);
            FileWriter.WriteAsync(files,opts.Output);
        }

        private static void HandleError(IEnumerable<Error> errors)
        {
            foreach (Error item in errors)
            {
                Console.Write(item.ToString());
            }
        }

        static private IEnumerable<TextFile> MetadataToClientSource(string edmxString, string targetLanguage)
        {
            var reader = new OdcmReader();
            var writer = new TemplateWriter(targetLanguage);
            writer.SetConfigurationProvider(new ConfigurationProvider());

            var model = reader.GenerateOdcmModel(new List<TextFile> { new TextFile("$metadata", edmxString) });

            return writer.GenerateProxy(model);
        }
    }
}
