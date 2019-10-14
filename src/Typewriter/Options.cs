// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

using CommandLine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GraphODataTemplateWriter.Test")]

namespace Typewriter
{
    public enum VerbosityLevel
    {
        Minimal,
        Info,
        Debug, 
        Trace
    }

    /// <summary>
    /// Specifies how Typewriter will processes the input metadata and what type of outputs it produces.
    /// </summary>
    public enum GenerationMode
    {
        /// <summary>
        /// (default) Produces the output code files by cleaning the input metadata, parsing the docs, and adding annotations before generating the output files.
        /// </summary>
        Full,
        /// <summary>
        /// Produces an output metadata file by cleaning metadata, documentation parsing, and adding doc annotations.
        /// </summary>
        Metadata,
        /// <summary>
        /// Uses the input metadata and only generates code files for the target platform. It bypasses the cleaning, doc parsing, and adding doc annotations.
        /// </summary>
        Files
    }

    public class Options
    {
        [Option('l', "language", Default = "CSharp", HelpText = "The target language for the generated code files. The values can be: Android, Java, ObjC, CSharp, PHP, Python, TypeScript, or GraphEndpointList")]
        public string Language { get; set; }

        [Option('m', "metadata", Default = "https://graph.microsoft.com/v1.0/$metadata", HelpText = "Location of metadata.  Local file path or URL")]
        public string Metadata { get; set; }

        [Option('v', "verbosity", Default= VerbosityLevel.Minimal, HelpText = "Log verbosity level")]
        public VerbosityLevel Verbosity { get; set; }

        [Option('o', "output", Default= ".", HelpText = "Path to output folder")]
        public string Output { get; set; }

        [Option('d', "docs", Default = ".", HelpText = "Path to the root of the documentation repo folder")]
        public string DocsRoot { get; set; }

        [Option('g', "generationmode", Default = GenerationMode.Full, HelpText = "Specifies the generation mode. The values can be: Full, Metadata, or Files. Full generation mode produces " +
            "the output code files by cleaning the input metadata, parsing the documentation, and adding annotations before generating the output files. Metadata generation mode" +
            "produces an output metadata file by cleaning metadata, documentation parsing, and adding documentation annotations. Files generation mode produces code files from" +
            "an input metadata and bypasses the cleaning, documentation parsing, and adding documentation annotations.")]
        public GenerationMode GenerationMode { get; set; }

        [Option('f', "outputMetadataFileName", Default = "cleanMetadataWithDescriptions", HelpText = "The output metadata filename. Only applicable for GenerationMode.Metadata.")]
        public string OutputMetadataFileName { get; set; }

        [Option('e', "endpointVersion", Default = "v1.0", HelpText = "The endpoint version. Expected values are 'v1.0' and 'beta'. Only applicable for GenerationMode.Metadata.")]
        public string EndpointVersion { get; set; }

        [Option('p', "properties",  HelpText = "A space separated list of properties in the form of 'key:value'. These properties can be accessed in the " +
            "templates from the TemplateWriterSettings object returned by ConfigurationService.Settings. The suggested convention for specifying a key should be " +
            "the targeted template language name and the property name. For example, php.namespace:Microsoft\\Graph\\Beta\\Model would be a property to be consumed in the PHP templates.")]
        public IEnumerable<string> Properties { get; set; }
    }
}