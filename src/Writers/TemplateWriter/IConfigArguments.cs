using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWriter
{
    public interface IConfigArguments
    {
        ITemplateConfiguration TemplateConfiguration { get; set; }
        BuilderArguments BuilderArguments { get; set; }
    }
}
