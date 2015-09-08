using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.CodeHelpers.CSharp
{
    public class CodeWriterCSharp : CodeWriterBase
    {
        public CodeWriterCSharp(OdcmModel model) : base(model) { }

        public override String WriteOpeningCommentLine()
        {
            return "/*******************************************************************************" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine()
        {
            return "******************************************************************************/" + this.NewLineCharacter;
        }

        public override string WriteInlineCommentChar()
        {
            return "// ";
        }

        public string jsonContentType = "application/json";
    }
}
