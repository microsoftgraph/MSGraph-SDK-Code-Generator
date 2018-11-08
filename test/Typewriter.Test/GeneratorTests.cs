using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Typewriter.Test
{
    [TestClass]
    [Ignore] // Work is needed to generate from the test project.
    public class GeneratorTests
    {
        public string testMetadata;

        /// <summary>
        /// Load metadata from file into a string so we can validate MetadataPreprocessor.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            testMetadata = Typewriter.Test.Properties.Resources.dirtyMetadata;
        }

        [TestMethod]
        public void GenerateFilesTest()
        {
            const string outputDirectory = "output"; 

            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "TypeScript"
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + @"\com\microsoft\graph\src\Microsoft-graph.d.ts");
            Assert.IsTrue(fileInfo.Exists);
        }
    }
}
