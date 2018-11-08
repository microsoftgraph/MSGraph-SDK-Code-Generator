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

            // Generate the files from the input metadata and do not preprocess.
            if (options.GenerateMode == GenerationMode.Files)
            {
                var codeFiles = MetadataToClientSource(csdlContents, options.Language);
                FileWriter.WriteAsync(codeFiles, options.Output);

                stopwatch.Stop();
                Logger.Info($"Generation time: {stopwatch.Elapsed } seconds.");

                return;
            }

            // Clean up EDMX to work with the generators assumptions.
            string processedCsdlContents = MetadataPreprocessor.CleanMetadata(csdlContents);

            // Create clean metadata and provide a path to it.
            string pathToCleanMetadata = FileWriter.WriteMetadata(processedCsdlContents, "cleanMetadata.xml");

            // Inject documentation annotations into the clean CSDL using ApiDoctor and get back the file as a string.
            string csdlWithDocAnnotations = AnnotationHelper.ApplyAnnotationsToCsdl(options, pathToCleanMetadata).Result;

            // Output the clean and annotated metadata. 
            if (options.GenerateMode == GenerationMode.Metadata)
            {
                FileWriter.WriteMetadata(csdlWithDocAnnotations, "cleanMetadataWithDescriptions_v10.xml", options.Output);

                stopwatch.Stop();
                Logger.Info($"Generation time: {stopwatch.Elapsed } seconds.");

                return;
            }
            
            // Default GenerationMode.Full.
            // Create code files from the CSDL with annotations for the target platform and write those files to disk.
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

        /// <summary>
        /// Generates code files from an edmx file.
        /// </summary>
        /// <param name="edmxString">The EDMX file as a string.</param>
        /// <param name="targetLanguage">Specifies the target language. Possible values are csharp, php, etc.</param>
        /// <returns></returns>
        static private IEnumerable<TextFile> MetadataToClientSource(string edmxString, string targetLanguage)
        {
            if (String.IsNullOrEmpty(edmxString))
                throw new ArgumentNullException("edmxString", "The EDMX file string contains no content.");

            var reader = new OdcmReader();
            var writer = new TemplateWriter(targetLanguage);
            writer.SetConfigurationProvider(new ConfigurationProvider());

            var model = reader.GenerateOdcmModel(new List<TextFile> { new TextFile("$metadata", edmxString) });

            return writer.GenerateProxy(model);
        }
    }
}
