using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Typewriter.Test
{
    /// <summary>
    /// End to end tests that check the results of running Typewriter from the CLI.
    /// IMPORTANT: Typewriter MUST be built before as the templates need to be compiled before running the tests.
    /// </summary>
    [TestFixture]
    public class Given_a_valid_metadata_file_to_Typewriter
    {
        public string testMetadata;
        // The second segment is generated from the namespace in the target metadata file.
        public string generatedOutputUrl = @$"{Path.DirectorySeparatorChar}com{Path.DirectorySeparatorChar}microsoft{Path.DirectorySeparatorChar}graph";

        /// <summary>
        /// Load metadata from file into a string so we can validate MetadataPreprocessor.
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            testMetadata = Typewriter.Test.Properties.Resources.dirtyMetadata;
        }

        [Test]
        public void It_generates_a_typings_file()
        {
            const string outputDirectory = "output"; 

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "TypeScript"
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @$"{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}Microsoft-graph.d.ts");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected {fileInfo.FullName}. File was not found.");
        }

        [Test]
        public void It_transforms_metadata()
        {
            const string outputDirectory = "output";

            Options options = new Options()
            {
                Output = outputDirectory,
                GenerationMode = GenerationMode.Transform,
                Transform = "https://raw.githubusercontent.com/microsoftgraph/msgraph-metadata/master/transforms/csdl/preprocess_csdl.xsl"

            };

            Generator.Transform(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + @$"{Path.DirectorySeparatorChar}cleanMetadata.xml");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);

            bool hasThumbnailAnnotationBeenAdded = false; // Expect true
            bool hasHasStreamAttribute = false; // Expect false
            bool hasContainsTargetBeenSet = false; // Expect true

            // Check the document for these values.
            foreach (var line in lines)
            {
                if (line.Contains(@"<Annotation Term=""Org.OData.Core.V1.LongDescription"" String=""navigable"" />"))
                {
                    hasThumbnailAnnotationBeenAdded = true;
                }
                if (line.Contains("HasStream"))
                {
                    hasHasStreamAttribute = true;
                }
                if (line.Contains(@"<NavigationProperty Name=""plans"" Type=""Collection(microsoft.graph.plannerPlan)"" ContainsTarget=""true"" />"))
                {
                    hasContainsTargetBeenSet = true;
                }
            }

            ClassicAssert.IsTrue(hasThumbnailAnnotationBeenAdded, $"The expected LongDescription annotation wasn't set in the transformed cleaned metadata.");
            ClassicAssert.IsFalse(hasHasStreamAttribute, $"The HasStream attribute was't removed from the metadata.");
            ClassicAssert.IsTrue(hasContainsTargetBeenSet, $"The expected ContainsTarget attribute wasn't set in the transformed cleaned metadata.");
        }

        [Test]
        [Ignore("Current xslt transform adds global capability annotations")]
        [TestCase(true, false, false)]
        [TestCase(false, true, false)]
        [TestCase(true, false, true)]
        [TestCase(false, true, true)]
        public void It_transforms_metadata_and_keeps_annotations_and_includes_errors(bool shouldRemoveCapabilityAnnotation, bool shouldFindCapabilityAnnotation, bool addinnererrordescription)
        {
            const string outputDirectory = "output";
            const string outputBaseFileName = "cleanMetadata";

            Options options = new Options()
            {
                Output = outputDirectory,
                GenerationMode = GenerationMode.Transform,
                Transform = "https://raw.githubusercontent.com/microsoftgraph/msgraph-metadata/master/transforms/csdl/preprocess_csdl.xsl",
                RemoveAnnotations = shouldRemoveCapabilityAnnotation,
                AddInnerErrorDescription = addinnererrordescription,
            };

            Generator.Transform(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + @$"{Path.DirectorySeparatorChar}{outputBaseFileName}.xml");
            ClassicAssert.IsTrue(fileInfo.Exists, $"Expected: {fileInfo.FullName}. File was not found.");

            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);

            bool hasCapabilityAnnotations = lines.Any(x => x.Contains("Org.OData.Capabilities")); // Expect false

            ClassicAssert.AreEqual(shouldFindCapabilityAnnotation, hasCapabilityAnnotations, $"Expected to find capability annotations: {shouldFindCapabilityAnnotation}. Actually found capability annotations: {hasCapabilityAnnotations}");
        }
    }
}
