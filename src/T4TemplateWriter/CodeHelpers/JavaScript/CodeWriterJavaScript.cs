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
            return "# ------------------------------------------------------------------------------" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine()
        {
            return "# ------------------------------------------------------------------------------" + this.NewLineCharacter;
        }

        public override string WriteInlineCommentChar()
        {
            return "# ";
        }

        public override String NewLineCharacter
        {
            get { return "\n"; }
        }

        public IEnumerable<OdcmProperty> EntityAllProperties(OdcmClass obj)
        {
            if (obj.Base != null)
            {
                return EntityAllProperties(obj.Base).Concat(obj.Properties);
            }
            else
            {
                return obj.Properties;
            }

        }

        public IEnumerable<OdcmProperty> EntityProperties(OdcmClass obj)
        {
            return EntityAllProperties(obj).Where(prop => !prop.IsLink).ToList();
        }

        public IEnumerable<OdcmProperty> EntityNavigationProperties(OdcmClass obj)
        {
            return EntityAllProperties(obj).Where(prop => prop.IsLink).ToList();
        }

        public String FullTypeName(OdcmProperty prop)
        {
            return (prop.IsCollection) ? "Collection(" + prop.Type.FullName + ")" : prop.Type.FullName;
        }
    }

}
