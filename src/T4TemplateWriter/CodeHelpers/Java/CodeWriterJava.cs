namespace Vipr.T4TemplateWriter.CodeHelpers.Java
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Vipr.Core.CodeModel;

    public class CodeWriterJava : CodeWriterBase
    {
        public CodeWriterJava() : base() { }

        public CodeWriterJava(OdcmModel model) : base(model) { }

        public override String WriteOpeningCommentLine()
        {
            return "// ------------------------------------------------------------------------------" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine()
        {
            return "// ------------------------------------------------------------------------------" + this.NewLineCharacter;
        }

        public override string WriteInlineCommentChar()
        {
            return "// ";
        }
    }
}
