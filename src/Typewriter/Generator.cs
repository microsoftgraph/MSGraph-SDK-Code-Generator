using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Vipr.Core;
using Vipr.Reader.OData.v4;

[assembly: InternalsVisibleTo("GraphODataTemplateWriter.Test")]

namespace Typewriter
{
    internal static class Generator
    {
        /// <summary>
        /// Generate code files from the input metadata and do not preprocess.
        /// </summary>
        /// <param name="csdlContents">Metadata to process</param>
        /// <param name="options">The options bag</param>
        static internal void GenerateFiles(string csdlContents, Options options)
        {
            var filesToWrite = MetadataToClientSource(csdlContents, options.Language, options.Properties);
            FileWriter.WriteAsync(filesToWrite, options.Output);
        }

        /// <summary>
        /// Generate a metadata file that has been cleaned and doc annotations added to it.
        /// </summary>
        /// <param name="csdlContents">Metadata to process</param>
        /// <param name="options">The options bag</param>
        static internal void WriteCleanAnnotatedMetadata(string csdlContents, Options options)
        {
            string csdlWithDocAnnotations = CleanMetadata(csdlContents, options);
            string metadataFileName = string.Concat(options.OutputMetadataFileName, options.EndpointVersion, ".xml");
            FileWriter.WriteMetadata(csdlWithDocAnnotations, metadataFileName, options.Output);
        }

        /// <summary>
        /// Clean and annotate the input metadata and then generate code files.
        /// </summary>
        /// <param name="csdlContents">Metadata to process</param>
        /// <param name="options">The options bag</param>
        static internal void GenerateFilesFromCleanMetadata(string csdlContents, Options options)
        {
            string csdlWithDocAnnotations = CleanMetadata(csdlContents, options);

            // Create code files from the CSDL with annotations for the target platform and write those files to disk.
            GenerateFiles(csdlWithDocAnnotations, options);
        }

        static private string CleanMetadata(string csdlContents, Options options)
        {
            // Clean up EDMX to work with the generators assumptions.
            string processedCsdlContents = MetadataPreprocessor.CleanMetadata(csdlContents);

            // Create clean metadata and provide a path to it.
            string pathToCleanMetadata = FileWriter.WriteMetadata(processedCsdlContents, "cleanMetadata.xml");

            // Inject documentation annotations into the CSDL using ApiDoctor and get back the file as a string.
            return AnnotationHelper.ApplyAnnotationsToCsdl(options, pathToCleanMetadata).Result;
        }

        /// <summary>
        /// Generates code files from an edmx file.
        /// </summary>
        /// <param name="edmxString">The EDMX file as a string.</param>
        /// <param name="targetLanguage">Specifies the target language. Possible values are csharp, php, etc.</param>
        /// <returns></returns>
        static private IEnumerable<TextFile> MetadataToClientSource(string edmxString, string targetLanguage, IEnumerable<string> properties)
        {
            if (String.IsNullOrEmpty(edmxString))
                throw new ArgumentNullException("edmxString", "The EDMX file string contains no content.");

            var reader = new OdcmReader();
            var writer = new TemplateWriter(targetLanguage, properties);
            writer.SetConfigurationProvider(new ConfigurationProvider());

            var model = reader.GenerateOdcmModel(new List<TextFile> { new TextFile("$metadata", edmxString) });

            return writer.GenerateProxy(model);
        }
    }
}
