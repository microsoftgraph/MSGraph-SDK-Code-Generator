using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vipr.Core.CodeModel;

namespace Vipr.T4TemplateWriter.CodeHelpers.Java
{
    public class CodeWriterJava : CodeWriterBase
    {

        public CodeWriterJava() : base() { }
        public CodeWriterJava(OdcmModel model) : base(model) { }

        public override String WriteOpeningCommentLine()
        {
            return "/*******************************************************************************\n";
        }

        public override String WriteClosingCommentLine()
        {
            return "\n******************************************************************************/";
        }

        public override string WriteInlineCommentChar()
        {
            return "// ";
        }
    }
}
