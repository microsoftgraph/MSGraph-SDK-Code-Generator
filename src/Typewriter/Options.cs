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
    }
}