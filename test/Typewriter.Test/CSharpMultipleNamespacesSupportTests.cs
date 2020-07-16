using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Typewriter.Test
{
    [TestClass]
    public class CSharpMultipleNamespaceSupportTests
    {
        private const string OutputDirectoryName = "OutputDirectory";
        private const string TestDataCSharpDirectoryName = "TestDataCSharp";
        private const string MetadataDirectoryName = "Metadata";

        [TestMethod]
        public void Test()
        {
            // Arrange
            var currentDirectory = Directory.GetCurrentDirectory();
            var outputDirectory = Path.Combine(currentDirectory, OutputDirectoryName);
            var dataDirectory = Path.Combine(currentDirectory, TestDataCSharpDirectoryName);
            var metadataFile = Path.Combine(currentDirectory, MetadataDirectoryName, "MetadataMultipleNamespaces.xml");
            var typewriterParameters = $"-v Info -m {metadataFile} -o {outputDirectory} -g Files";

            // Act
            if (Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, recursive: true); // clean up any previous runs
            }

            Program.Main(typewriterParameters.Split(' '));

            // Assert
            var testOutputBuilder = new StringBuilder();
            var errorCounter = 0;
            foreach (var (expectedFilePath, actualOutputFilePath) in GetFilePaths(dataDirectory))
            {
                if (File.Exists(actualOutputFilePath))
                {
                    CompareFiles(testOutputBuilder, expectedFilePath, actualOutputFilePath, ref errorCounter);
                }
                else
                {
                    testOutputBuilder.AppendLine();
                    testOutputBuilder.AppendLine($"{++errorCounter}. Output file is not generated: {actualOutputFilePath}");
                }
            }

            if (errorCounter > 0)
            {
                string message = string.Join(Environment.NewLine,
                    "A diff between following folders are strongly encouraged to see if the changes are intended:",
                    dataDirectory,
                    outputDirectory,
                    string.Empty,
                    $"If the changes are expected, please replace the contents of {TestDataCSharpDirectoryName} with the contents of {OutputDirectoryName}.",
                    string.Empty,
                    "Details of failures:");

                Assert.Fail(message + testOutputBuilder.ToString());
            }
        }

        /// <summary>
        /// Compares the contents of expected file to actual output from Typewriter.
        /// </summary>
        /// <param name="testOutputBuilder">String builder to append into the test output</param>
        /// <param name="expectedFilePath">Path to the expected file</param>
        /// <param name="actualOutputFilePath">Path to the actual output file from Typewriter</param>
        /// <param name="errorCounter">Error counter for the test run</param>
        private static void CompareFiles(StringBuilder testOutputBuilder, string expectedFilePath, string actualOutputFilePath, ref int errorCounter)
        {
            var expectedFileContents = File.ReadAllText(expectedFilePath);
            var actualFileContents = File.ReadAllText(actualOutputFilePath);
            if (expectedFileContents != actualFileContents)
            {
                testOutputBuilder.AppendLine();
                testOutputBuilder.AppendLine($"{++errorCounter}. File contents do not match.");
                testOutputBuilder.AppendLine($"\tExpected file: {expectedFilePath}");
                testOutputBuilder.AppendLine($"\tActual file: {actualOutputFilePath}");
            }
        }

        /// <summary>
        /// Takes a data directory, traverses the entire subdirectory structure,
        /// extracts full file paths as expected file paths,
        /// converts expected file paths into actual output file paths as well for a later diff.
        /// </summary>
        /// <param name="dataDirectory">Data directory full path</param>
        /// <returns>Pairs of expected and actual file paths as an enumerable</returns>
        private static IEnumerable<(string, string)> GetFilePaths(string dataDirectory)
        {
            return from file in new DirectoryInfo(dataDirectory).GetFiles("*.cs", SearchOption.AllDirectories)
                   let actualOutputFilePath = file.FullName.Replace(TestDataCSharpDirectoryName, OutputDirectoryName)
                   let expectedFilePath = file.FullName
                   select (expectedFilePath, actualOutputFilePath);
        }
    }
}
