﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Typewriter.Test
{
    /// <summary>
    /// End to end tests that check the results of running Typewriter from the CLI.
    /// </summary>
    [TestClass]
    public class GeneratorTests
    {
        public string testMetadata;
        // The second segment is generated from the namespace in the target metadata file.
        public string generatedOutputUrl = @"\com\Graph";

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

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @"\src\Microsoft-graph.d.ts");
            Assert.IsTrue(fileInfo.Exists);
        }

        [TestMethod]
        public void Validate_PHP_Generated_Beta_Namespace_Tests()
        {
            const string testNamespace = "TEST.NAMESPACE";
            const string outputDirectory = "output";
            
            Options options = new Options()
            {
                Output = outputDirectory,
                Language = "PHP",
                Properties = new List<string>() { $"php.namespace:{testNamespace}" },
                GenerationMode = GenerationMode.Files
            };

            Generator.GenerateFiles(testMetadata, options);

            FileInfo fileInfo = new FileInfo(outputDirectory + generatedOutputUrl + @"\Model\Entity.php");
            Assert.IsTrue(fileInfo.Exists, "The expected file was not found.");

            // Check that the namespace applied at the CLI was added to the document.
            IEnumerable<string> lines = File.ReadLines(fileInfo.FullName);
            bool isExpectedNamespaceSet = false;
            foreach (var line in lines)
            {
                if (line.Contains($"namespace {testNamespace}"))
                {
                    isExpectedNamespaceSet = true;
                    break;
                }
            } 
            Assert.IsTrue(isExpectedNamespaceSet, $"The expected namespace, {testNamespace}, was not set in the generated test file.");
        }
    }
}
