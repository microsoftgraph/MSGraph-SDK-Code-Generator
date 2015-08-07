using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    public enum SubProcessorType
    {
        EntityType,
        ComplexType,
        EnumType,
        EntityContainer,
        Other
    }

    public interface ITemplateMapping
    {
        SubProcessorType GetSubProcessorType(string templateName);

        TemplateType GetTemplateType(string templateName);

    }
}
