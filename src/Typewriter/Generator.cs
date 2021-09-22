using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Vipr.Core;
using Vipr.Reader.OData.v4;

[assembly: InternalsVisibleTo("GraphODataTemplateWriter.Test")]

namespace Typewriter
{
    internal static class Generator
    {
        internal static Logger Logger => LogManager.GetLogger("Generator");

        /// <summary>
        /// Generate code files from the input metadata and do not preprocess.
        /// </summary>
        /// <param name="csdlContents">Metadata to process</param>
        /// <param name="options">The options bag</param>
        static internal void GenerateFiles(string csdlContents, Options options)
        {
            var filesToWrite = MetadataToClientSource(csdlContents, options.Language, options.Properties, options.EndpointVersion);
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
            return AnnotationHelper.ApplyAnnotationsToCsdl(options, pathToCleanMetadata);
        }

        /// <summary>
        /// Transform CSDL with XSLT.
        /// </summary>
        /// <param name="csdlContents">The CSDL to transform.</param>
        /// <param name="options">The options bag that must contain the -transform -t parameter.</param>
        /// <returns>The location of the generated file.</returns>
        static internal string Transform(string csdlContents, Options options)
        {
            var outputDirectoryPath = options.Output;

            if (!string.IsNullOrWhiteSpace(outputDirectoryPath) && !Directory.Exists(outputDirectoryPath))
                Directory.CreateDirectory(outputDirectoryPath);
            if (string.IsNullOrWhiteSpace(outputDirectoryPath))
                outputDirectoryPath = Environment.CurrentDirectory;

            var pathToCleanMetadata = Path.Combine(outputDirectoryPath, "cleanMetadata.xml");

            using (var reader = new StringReader(csdlContents))
            using (var doc = XmlReader.Create(reader))
            using (XmlTextWriter writer = new XmlTextWriter(pathToCleanMetadata, Encoding.UTF8))
            {
                writer.Indentation = 2;
                writer.Formatting = Formatting.Indented;

                XslCompiledTransform transform = new XslCompiledTransform();
                XsltSettings settings = new XsltSettings();
                settings.EnableScript = true;
                transform.Load(options.Transform, settings, null);

                if (options.RemoveAnnotations == false)
                {   
                    XsltArgumentList xsltargs = new();
                    xsltargs.AddParam("remove-capability-annotations", string.Empty, options.RemoveAnnotations.ToString());

                    // Execute the transformation, keep capability annotations, writes the transformed file.
                    transform.Transform(doc, xsltargs, writer);
                    Logger.Info($"Transformed metadata with capability annotations written to {pathToCleanMetadata}");
                }
                else
                {
                    // Execute the transformation, writes the transformed file.
                    transform.Transform(doc, writer);
                    Logger.Info($"Transformed metadata written to {pathToCleanMetadata}");
                }
            }

            return pathToCleanMetadata;
        }

        /// <summary>
        /// Transform CSDL with XSLT and add documentation.
        /// </summary>
        /// <param name="csdlContents">The CSDL to transform.</param>
        /// <param name="options">The options bag that must contain the -transform -t parameter,
        /// and the -docs -d parameter.</param>
        static internal void TransformWithDocs(string csdlContents, Options options)
        {
            var pathToCleanMetadata = Transform(csdlContents, options);

            string csdlWithDocAnnotations = AnnotationHelper.ApplyAnnotationsToCsdl(options, pathToCleanMetadata);
            string outputMetadataFilename = options.OutputMetadataFileName ?? "cleanMetadataWithDescriptions";
            string metadataFileName = string.Concat(outputMetadataFilename, options.EndpointVersion, ".xml");
            FileWriter.WriteMetadata(csdlWithDocAnnotations, metadataFileName, options.Output);

            Logger.Info($"Transformed metadata with documentation written to {metadataFileName}");
        }

        /// <summary>
        /// Generates code files from an edmx file.
        /// </summary>
        /// <param name="edmxString">The EDMX file as a string.</param>
        /// <param name="targetLanguage">Specifies the target language. Possible values are csharp, php, etc.</param>
        /// <returns></returns>
        static private IEnumerable<TextFile> MetadataToClientSource(string edmxString, string targetLanguage, IEnumerable<string> properties, string endpointVersion = "v1.0")
        {
            if (String.IsNullOrEmpty(edmxString))
                throw new ArgumentNullException("edmxString", "The EDMX file string contains no content.");

            var reader = new OdcmReader
            {
                AddCastPropertiesForNavigationProperties = targetLanguage.Equals("java", StringComparison.InvariantCultureIgnoreCase)
            };
            var writer = new TemplateWriter(targetLanguage, properties, endpointVersion);
            writer.SetConfigurationProvider(new ConfigurationProvider());

            var model = reader.GenerateOdcmModel(new List<TextFile> { new TextFile("$metadata", edmxString) });

            return writer.GenerateProxy(model);
        }
    }
}
