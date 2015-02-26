using System;
using System.Collections.Generic;

namespace TemplateWriter
{
    public class BuilderArguments
    {
        public string Language { get; set; }

        public string OutputDir { get; set; }
        public string InputFile { get; set; }
        public string[] Plugins { get; set; }
        public bool ShowHelp { get; set; }
    }
}