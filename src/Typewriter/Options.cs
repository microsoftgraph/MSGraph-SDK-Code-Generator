using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Option('o', "output", Default= "./com/microsoft/graph", HelpText = "Path to output folder")]
        public string Output { get; set; }
    }
}
