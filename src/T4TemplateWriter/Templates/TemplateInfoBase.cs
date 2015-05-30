using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vipr.T4TemplateWriter {
    abstract public class TemplateInfoBase : ITemplateInfo {

        abstract public String Id { get; }
        abstract public String TemplateLanguage { get; set; }
        abstract public String TemplateName { get; set; }
        abstract public TemplateType TemplateType { get; set; }

        virtual protected bool Equals(TemplateInfoBase other) {
            return (this.Id == other.Id);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TemplateInfoBase) obj);
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(TemplateInfoBase left, TemplateInfoBase right) {
            return Equals(left, right);
        }

        public static bool operator !=(TemplateInfoBase left, TemplateInfoBase right) {
            return !Equals(left, right);
        }

        public override string ToString() {
            var dirSep = Path.DirectorySeparatorChar;
            return (this.TemplateLanguage + dirSep + this.TemplateType.ToString() + dirSep + this.TemplateName.ToString());
        }
    }
}
