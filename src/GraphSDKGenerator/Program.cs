using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
using NLog;
using NLog.Config;
using NLog.Targets;
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
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            SetupLogging(opts.Verbosity);

            var csdlContents = MetadataResolver.GetMetadata(opts.Metadata);

            // fix edmx

            // filter out problem workloads

            var files = MetadataToClientSource(csdlContents, opts.Language);
            FileWriter.WriteAsync(files,opts.Output);

            stopwatch.Stop();
            Logger.Info($"Generation time: {stopwatch.Elapsed } seconds.");
        }

        private static void SetupLogging(VerbosityLevel verbosity)
        {
            var config = new LoggingConfiguration();

            config.AddTarget(new ConsoleTarget() {
                DetectConsoleAvailable =true,
                Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message}",
                Name = "Console"
            });
            
            switch (verbosity)
            {
                case VerbosityLevel.Minimal:
                    config.AddRule(LogLevel.Warn, LogLevel.Fatal, "Console" );
                    break;
                case VerbosityLevel.Info:
                    config.AddRule(LogLevel.Info, LogLevel.Fatal, "Console");
                    break;
                case VerbosityLevel.Debug:
                    config.AddRule(LogLevel.Debug, LogLevel.Fatal, "Console");
                    break;
                case VerbosityLevel.Trace:
                    config.AddRule(LogLevel.Trace, LogLevel.Fatal, "Console");
                    break;
                default:
                    config.AddRule(LogLevel.Warn, LogLevel.Fatal, "Console");
                    break;
            }

            LogManager.Configuration = config;  // Activate configuration
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
