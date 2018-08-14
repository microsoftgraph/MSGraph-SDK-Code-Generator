using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace GraphSDKGenerator
{
    class Options
    {
        [Option('l', "language", Default = "CSharp", HelpText = "Desired output language")]
        public string Language { get; set; }

        [Option('m', "metadata", Default = "https://graph.microsoft.com/v1.0/$metadata", HelpText = "Location of metadata.  Local file path or URL")]
        public string Metadata { get; set; }

        [Option('v', "verbose", HelpText = "Output Verbose logs")]
        public bool Verbose { get; set; }

        [Option('o', "output", HelpText = "Path to output folder")]
        public string Output { get; set; }
    }
}
