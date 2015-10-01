using System;
using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.CodeHelpers.CSharp
{
    public class CodeWriterCSharp : CodeWriterBase
    {
        public CodeWriterCSharp(OdcmModel model) : base(model) { }

        public override String WriteOpeningCommentLine()
        {
            return "// ------------------------------------------------------------------------------" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine()
        {
            return "// ------------------------------------------------------------------------------";
        }

        public override string WriteInlineCommentChar()
        {
            return "// ";
        }

        public string jsonContentType = "application/json";
    }
}
