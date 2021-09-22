using ApiDoctor.Publishing.CSDL;
using ApiDoctor.Validation;
using ApiDoctor.Validation.Error;
using ApiDoctor.Validation.OData;
using ApiDoctor.Validation.OData.Transformation;
using NLog;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Typewriter
{
    /// <summary>
    /// Creates a CSDL file with documentation annotations sourced from documentation.
    /// </summary>
    internal class DocAnnotationWriter : ApiDoctor.Publishing.CSDL.CsdlWriter
    {
        private static Logger Logger => LogManager.GetLogger("DocAnnotationWriter");
        
        private readonly CsdlWriterOptions options;

        internal DocAnnotationWriter(DocSet docSet, CsdlWriterOptions options) : base(docSet, options)
        {
            this.options = options; // Can change the base access modifier so we could use it. 
        }

        public string PublishToString(IssueLogger issues)
        {
            string outputFilenameSuffix = "";

            Logger.Info("Begin creating metadata file with documentation annotations.");

            // Step 1: Generate an EntityFramework OM from the documentation and/or template file
            EntityFramework framework = CreateEntityFrameworkFromDocs(issues);
            if (null == framework)
                return string.Empty;

            // Step 1a: Apply an transformations that may be defined in the documentation
            if (!string.IsNullOrEmpty(options.TransformOutput))
            {
                PublishSchemaChangesConfigFile transformations = DocSet.TryLoadConfigurationFiles<PublishSchemaChangesConfigFile>(options.DocumentationSetPath).Where(x => x.SchemaChanges.TransformationName == options.TransformOutput).FirstOrDefault();
                if (null == transformations)
                {
                    throw new KeyNotFoundException($"Unable to locate a transformation set named {options.TransformOutput}. Aborting.");
                }

                string[] versionsToPublish = options.Version?.Split(new char[] { ',', ' ' });
                framework.ApplyTransformation(transformations.SchemaChanges, versionsToPublish);
                if (!string.IsNullOrEmpty(options.Version))
                {
                    outputFilenameSuffix += $"-{options.Version}";
                }
            }

            if (options.Sort)
            {
                // Sorts the objects in collections, so that we have consistent output regardless of input
                framework.SortObjectGraph();
            }

            if (options.ValidateSchema)
            {
                framework.ValidateSchemaTypes();
            }

            // Step 2: Generate XML representation of EDMX
            string xmlData = ODataParser.Serialize<EntityFramework>(framework, options.AttributesOnNewLines);

            Logger.Info("Finish creating metadata file with documentation annotations.");

            return xmlData;
        }
    }

    internal static class AnnotationHelper
    {
        private static Logger Logger => LogManager.GetLogger("AnnotationHelper");

        /// <summary>
        /// Applies annotations to CSDL file.
        /// </summary>
        /// <param name="options">The typewriter input options.</param>
        /// <param name="pathToCleanMetadata">Optional. Contains the path to a clean metadata to use when applying annotations. Overrides Option.Metadata.</param>
        /// <returns>An annotated metadata file.</returns>
        internal static string ApplyAnnotationsToCsdl(Options options, string pathToCleanMetadata = null)
        {
            DocSet docs = GetDocSet(options, new IssueLogger());

            var csdlWriterOptions = new CsdlWriterOptions()
            {
                DocumentationSetPath = Path.Join(options.DocsRoot, "api-reference", options.EndpointVersion),
                Annotations = AnnotationOptions.Properties,
                SkipMetadataGeneration = true,
                Formats = MetadataFormat.EdmxInput
            };

            // We only intend to use the source metadata when we don't pass in a CSDL.
            if (string.IsNullOrEmpty(pathToCleanMetadata))
                csdlWriterOptions.SourceMetadataPath = options.Metadata;
            else
                csdlWriterOptions.SourceMetadataPath = pathToCleanMetadata;

            DocAnnotationWriter docWriter = new DocAnnotationWriter(docs, csdlWriterOptions);

            return docWriter.PublishToString(new IssueLogger());
        }


        private static DocSet GetDocSet(Options options, IssueLogger issues)
        {
            Logger.Info("Opening documentation from {0}", options.DocsRoot);
            DocSet docSet = null;

            try
            {
                docSet = new DocSet(options.DocsRoot);
            }
            catch (FileNotFoundException ex)
            {
                Logger.Error(ex.Message);
                return null;
            }

            Logger.Info("Parsing documentation files");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            docSet.ScanDocumentation(string.Empty, issues);

            // Json transformation for empty base types should default to null
            // but they default to empty string. Clean those up here.
            foreach (var resource in docSet.Resources)
            {
                if (resource.BaseType == string.Empty)
                {
                    resource.BaseType = null;
                }
            }

            stopwatch.Stop();
            Logger.Info($"Took {stopwatch.Elapsed} to parse {docSet.Files.Length} source files.");

            return docSet;
        }
    }
}
