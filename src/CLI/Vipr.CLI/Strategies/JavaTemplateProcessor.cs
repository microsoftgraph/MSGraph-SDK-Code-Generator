using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using TemplateWriter;
using Vipr.CLI.Output;
using Vipr.Core.CodeModel;

namespace Vipr.CLI.Strategies
{
    public class JavaTemplateProcessor : BaseTemplateProcessor
    {
        public JavaTemplateProcessor(IFileWriter fileWriter, OdcmModel model, string baseFilePath) : base(fileWriter, model, baseFilePath)
        {
            StrategyName = "Java";

        }
    }
}