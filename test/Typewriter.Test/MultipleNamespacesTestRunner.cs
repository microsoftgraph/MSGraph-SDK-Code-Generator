﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework.Legacy;

namespace Typewriter.Test
{
    // supported test languages
    public enum TestLanguage
    {
        TypeScript,
    }

    public static class MultipleNamespacesTestRunner
    {
        private const string OutputDirectoryPrefix = "OutputDirectory";
        private const string TestDataDirectoryPrefix = "TestData";
        private const string MetadataDirectoryName = "Metadata";

        // contains microsoft.graph and microsoft.graph.callRecords
        // TypeScript rely on the assumption that all namespaces will be a subnamespace to microsoft.graph
        // and generation process creates a single file with nested namespaces
        private const string MetadataWithSubNamespacesFile = "MetadataWithSubNamespaces.xml";

        public static void Run(TestLanguage language, bool isBeta = false)
        {
            string getMetadataFile(TestLanguage testLanguage)
            {
                switch (testLanguage)
                {
                    case TestLanguage.TypeScript:
                        return MetadataWithSubNamespacesFile;
                    default:
                        throw new ArgumentException("unexpected test language", nameof(testLanguage));
                }
            }

            // Arrange
            var languageStr = language.ToString();
            var directoryPostfix = languageStr + (isBeta ? "Beta" : string.Empty);
            var outputDirectoryName = OutputDirectoryPrefix + directoryPostfix;
            var testDataDirectoryName = TestDataDirectoryPrefix + directoryPostfix;

            var currentDirectory = Path.GetDirectoryName(typeof(MultipleNamespacesTestRunner).Assembly.Location); //Directory.GetCurrentDirectory();
            var outputDirectory = Path.Combine(currentDirectory, outputDirectoryName);
            var dataDirectory = Path.Combine(currentDirectory, testDataDirectoryName);
            var metadataFile = Path.Combine(currentDirectory, MetadataDirectoryName, getMetadataFile(language));

            var csdlContents = MetadataResolver.GetMetadata(metadataFile);
            var options = new Options
            {
                Verbosity = VerbosityLevel.Info,
                Output = outputDirectory,
                GenerationMode = GenerationMode.Files,
                Language = languageStr
            };
            options.EndpointVersion = isBeta ? "beta" : "v1.0"; // fixes generation test as the endpoint contains the version and the default options are not applied in this testing mode

            if (isBeta)
            {
                if (TestLanguage.TypeScript == language)
                {
                    options.Properties = new List<string> { "typescript.namespacePostfix:beta" };
                }
            }

            // Act
            if (Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, recursive: true); // clean up any previous runs
            }

            Generator.GenerateFiles(csdlContents, options);

            var outputDirectoryInfo = new DirectoryInfo(outputDirectory);
            var dataDirectoryInfo = new DirectoryInfo(dataDirectory);

            ClassicAssert.AreEqual(dataDirectoryInfo.GetFiles("*.*", SearchOption.AllDirectories).Length,
                outputDirectoryInfo.GetFiles("*.*", SearchOption.AllDirectories).Length,
                $@"Number of generated files don't match with number of expected files! Compare these two folders:
{dataDirectory}
{outputDirectory}");

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
            var expectedFileContents = File.ReadAllText(expectedFilePath).Replace("\r", "");
            var actualFileContents = File.ReadAllText(actualOutputFilePath).Replace("\r", "");
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
        /// <param name="testDataDirectoryName">test data directory name, e.g. TestDataTypeScript</param>
        /// <param name="outputDirectoryName">output directory name, e.g. OutputDirectoryTypeScript</param>
        /// <returns></returns>
        private static IEnumerable<(string, string)> GetFilePaths(TestLanguage language, string dataDirectory, string testDataDirectoryName, string outputDirectoryName)
        {
            HashSet<string> extensions = new HashSet<string>();
            switch (language)
            {
                case TestLanguage.TypeScript:
                    extensions.Add(".ts");
                    break;
                default:
                    throw new ArgumentException("unexpected test language", nameof(language));
            }

            return from file in new DirectoryInfo(dataDirectory)
                       .EnumerateFiles("*", SearchOption.AllDirectories)
                       .Where(f => extensions.Contains(f.Extension))
                   let actualOutputFilePath = file.FullName.Replace(testDataDirectoryName, outputDirectoryName)
                   let expectedFilePath = file.FullName
                   select (expectedFilePath, actualOutputFilePath);
        }
    }
}
