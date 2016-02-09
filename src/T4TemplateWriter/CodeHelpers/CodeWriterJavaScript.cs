using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.CodeHelpers.JavaScript
{
    public class CodeWriterJavaScript : CodeWriterBase
    {
        public CodeWriterJavaScript() : base() { }

        public CodeWriterJavaScript(OdcmModel model) : base(model) { }

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
