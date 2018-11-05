using CommandLine;

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

    class Options
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
        public GenerationMode GenerateMode { get; set; }
    }
}