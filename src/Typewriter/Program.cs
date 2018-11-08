using CommandLine;
using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Vipr.Core;
using Vipr.Reader.OData.v4;

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

            switch (options.GenerationMode)
            {
                case GenerationMode.Files:
                    Generator.GenerateFiles(csdlContents, options);
                    break;
                case GenerationMode.Metadata:
                    Generator.WriteCleanAnnotatedMetadata(csdlContents, options);
                    break;
                case GenerationMode.Full:
                default:
                    Generator.GenerateFilesFromCleanMetadata(csdlContents, options);
                    break;
            }

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

        /// <summary>
        /// Generates code files from an edmx file.
        /// </summary>
        /// <param name="edmxString">The EDMX file as a string.</param>
        /// <param name="targetLanguage">Specifies the target language. Possible values are csharp, php, etc.</param>
        /// <returns></returns>
        //static private IEnumerable<TextFile> MetadataToClientSource(string edmxString, string targetLanguage)
        //{
        //    if (String.IsNullOrEmpty(edmxString))
        //        throw new ArgumentNullException("edmxString", "The EDMX file string contains no content.");

        //    var reader = new OdcmReader();
        //    var writer = new TemplateWriter(targetLanguage);
        //    writer.SetConfigurationProvider(new ConfigurationProvider());

        //    var model = reader.GenerateOdcmModel(new List<TextFile> { new TextFile("$metadata", edmxString) });

        //    return writer.GenerateProxy(model);
        //}
    }
}
