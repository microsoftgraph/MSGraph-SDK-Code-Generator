using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Typewriter.Test
{
    // supported test languages
    public enum TestLanguage
    {
        CSharp,
        Java
    }

    public static class MultipleNamespacesTestRunner
    {
        private const string OutputDirectoryPrefix = "OutputDirectory";
        private const string TestDataDirectoryPrefix = "TestData";
        private const string MetadataDirectoryName = "Metadata";

        public static void Run(TestLanguage language)
        {
            // Arrange
            var languageStr = language.ToString();
            var outputDirectoryName = OutputDirectoryPrefix + languageStr;
            var testDataDirectoryName = TestDataDirectoryPrefix + languageStr;

            var currentDirectory = Directory.GetCurrentDirectory();
            var outputDirectory = Path.Combine(currentDirectory, outputDirectoryName);
            var dataDirectory = Path.Combine(currentDirectory, testDataDirectoryName);
            var metadataFile = Path.Combine(currentDirectory, MetadataDirectoryName, "MetadataMultipleNamespaces.xml");
            var typewriterParameters = $"-v Info -m {metadataFile} -o {outputDirectory} -g Files -l {languageStr}";

            // Act
            if (Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, recursive: true); // clean up any previous runs
            }

            Program.Main(typewriterParameters.Split(' '));

            // Assert
            var testOutputBuilder = new StringBuilder();
            var errorCounter = 0;
            foreach (var (expectedFilePath, actualOutputFilePath) in GetFilePaths(language, dataDirectory, testDataDirectoryName, outputDirectoryName))
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
                    $"If the changes are expected, please replace the contents of {testDataDirectoryName} with the contents of {outputDirectoryName}.",
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
        /// <param name="testDataDirectoryName">test data directory name, e.g. TestDataCSharp</param>
        /// <param name="outputDirectoryName">output directory name, e.g. OutputDirectoryCSharp</param>
        /// <returns></returns>
        private static IEnumerable<(string, string)> GetFilePaths(TestLanguage language, string dataDirectory, string testDataDirectoryName, string outputDirectoryName)
        {
            string extension = string.Empty;
            switch (language)
            {
                case TestLanguage.CSharp:
                    extension = "*.cs";
                    break;
                case TestLanguage.Java:
                    extension = "*.java";
                    break;
            }

            return from file in new DirectoryInfo(dataDirectory).GetFiles(extension, SearchOption.AllDirectories)
                   let actualOutputFilePath = file.FullName.Replace(testDataDirectoryName, outputDirectoryName)
                   let expectedFilePath = file.FullName
                   select (expectedFilePath, actualOutputFilePath);
        }
    }
}
