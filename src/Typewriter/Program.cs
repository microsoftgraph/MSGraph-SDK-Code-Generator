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
using Vipr.Reader.OData.v4;
using ApiDoctor.Publishing;

namespace Typewriter
{
    class Program
    {
        internal static Logger Logger => LogManager.GetLogger("Typewriter");

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(opts => GenerateSDK(opts))
                .WithNotParsed((errs) => HandleError(errs));
        }

        private static void GenerateSDK(Options options)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            SetupLogging(options.Verbosity);

            string csdlContents = MetadataResolver.GetMetadata(options.Metadata);

            // Clean up EDMX to work with the generators assumptions.
            string processedCsdlContents = MetadataPreprocessor.CleanMetadata(csdlContents);

            // Inject documentation annotations into the CSDL using ApiDoctor.
            string csdlWithDocAnnotations = AnnotationHelper.ApplyAnnotationsToCsdl(processedCsdlContents, options);
            
            var files = MetadataToClientSource(csdlWithDocAnnotations, options.Language);
            FileWriter.WriteAsync(files, options.Output);

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
