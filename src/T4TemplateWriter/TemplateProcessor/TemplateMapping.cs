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
            TEnum type;
            if (!TryGetValue<TEnum>(templateName, key, out type))
            {
                type = defaultValue;
            }
            return type;
        }

        private bool TryGetValue<TEnum>(string templateName, string key, out TEnum type) where TEnum : struct  
        {
            bool success = false;
            type = default(TEnum);
            Dictionary<string, string> templateDictionary;
            this.mapping.TryGetValue(templateName, out templateDictionary);
            if (templateDictionary!= null)
            {

                TEnum outType = default(TEnum);
                string enumType;
                templateDictionary.TryGetValue(key, out enumType);
                success = Enum.TryParse(enumType, out outType);
                if (success)
                {
                    type = outType;
                }
            }
            return success;
        }
    }
}
