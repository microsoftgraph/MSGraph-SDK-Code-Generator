using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    class TemplateMapping : ITemplateMapping
    {
        private Dictionary<string, Dictionary<string, string>> mapping;
        public TemplateMapping(Dictionary<string, Dictionary<string, string>> mapping)
        {
            this.mapping = mapping;
        }

        public SubProcessorType GetSubProcessorType(string templateName)
        {
            return GetEnum<SubProcessorType>(templateName, "SubProcessor", SubProcessorType.Other);
        }

        public TemplateType GetTemplateType(string templateName)
        {
            return GetEnum<TemplateType>(templateName, "Type", TemplateType.Other);
        }

        private TEnum GetEnum<TEnum>(string templateName, string key, TEnum defaultValue) where TEnum : struct
        {
            TEnum type = defaultValue;
            TryGetValue<TEnum>(templateName, key, type);
            return type;
        }

        private bool TryGetValue<TEnum>(string templateName, string key, out TEnum type) where TEnum : struct  
        {
            TEnum originalType = type;
            Dictionary<string, string> templateDictionary;
            this.mapping.TryGetValue(templateName, out templateDictionary);
            if (templateDictionary!= null)
            {
                string enumType;
                templateDictionary.TryGetValue(key, enumType);
                Enum.TryParse(subProcessorType, out type);
            }
        }
        return type != originalType;
    }
}
