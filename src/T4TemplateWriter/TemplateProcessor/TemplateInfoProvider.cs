using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    /// <summary>
    /// Configures and creates the ITemplateInfo from the json config file.
    /// </summary>
    class TemplateInfoProvider : ITemplateInfoProvider
    {

        private Dictionary<string, Dictionary<string, string>> mapping;
        private FileNameCasing defaultCasing;
        private SubProcessor defaultSubProcessor;
        private Template defaultTemplate;

        public TemplateInfoProvider(Dictionary<string, Dictionary<string, string>> mapping, 
                                    FileNameCasing defaultNameCasing = FileNameCasing.UpperCamel, 
                                    SubProcessor defaultSubProcessor = SubProcessor.Other,
                                    Template defaultTempalte = Template.Other)
        {
            this.mapping = mapping;
            this.defaultCasing = defaultNameCasing;
            this.defaultSubProcessor = defaultSubProcessor;
            this.defaultTemplate = defaultTemplate;
        }

        public ITemplateInfo Create(string fullPath)
        {
            var fileInfo = new TemplateFileInfo();
            fileInfo.FullPath = fullPath;

            // <rootPath>/<grandparent>/<parent>/<fileName>.<fileExtension>.tt
            fileInfo.TemplateName = Path.GetFileNameWithoutExtension(fullPath);  // <fileName>.<fileExtension>
            fileInfo.FileExtension = Path.GetExtension(fileInfo.TemplateName).Substring(1);  // <fileExtension>

            fileInfo.TemplateBaseName = Path.GetFileNameWithoutExtension(fileInfo.TemplateName); // <fileName>

            string parentPath = Path.GetDirectoryName(fullPath);  // <rootPath>/<grandparent>/<parent>
            string parentName = Path.GetFileNameWithoutExtension(parentPath);
            string grandparentPath = Path.GetDirectoryName(parentPath);  // <rootPath>/<grandparent>
            string grandparentName = Path.GetFileNameWithoutExtension(grandparentPath);  // <grandparent>

            fileInfo.OutputParentDirectory = parentName;
            fileInfo.TemplateLanguage = grandparentName;
            fileInfo.TemplateType = this.GetTemplate(fileInfo.TemplateBaseName);
            fileInfo.SubprocessorType = this.GetSubProcessor(fileInfo.TemplateBaseName);
            fileInfo.Casing = this.GetFileNameCasing(fileInfo.TemplateBaseName);


            IEnumerable<string> includedObjects = IncludedObjects(fileInfo.TemplateBaseName);
            IEnumerable<string> excludedObjects = ExcludedObjects(fileInfo.TemplateBaseName);
            IEnumerable<string> matchingDescriptions = MatchingDescriptions(fileInfo.TemplateBaseName);

            //TODO aclev: these are mutally exclusive and should throw here if they are both set
            if (includedObjects.Count() != 0)
            {
                fileInfo.IncludedObjects = includedObjects;
            }
            else if (excludedObjects.Count() != 0)
            {
                fileInfo.ExcludedObjects = excludedObjects;
            }

            if (matchingDescriptions.Count() != 0)
            {
                fileInfo.ObjectDescriptions = matchingDescriptions;
            }

            var templateDictionary = TemplateDictionary(fileInfo.TemplateBaseName);
            if (templateDictionary != null)
            {
                string nameFormat;
	            if (templateDictionary.TryGetValue("Name", out nameFormat))
	            {
	                fileInfo.NameFormat = nameFormat;
	            }
            }

            return fileInfo;
        }

        private FileNameCasing GetFileNameCasing(string templateName)
        {
            return GetEnum<FileNameCasing>(templateName, "Case",  this.defaultCasing);
        }

        /// <summary>
        /// Gets the subprocessor type for the given template.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private SubProcessor GetSubProcessor(string templateName)
        {
            return GetEnum<SubProcessor>(templateName, "SubProcessor", this.defaultSubProcessor);
        }
        
        /// <summary>
        /// Gets the TemplateType for the given template.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private Template GetTemplate(string templateName)
        {
            return GetEnum<Template>(templateName, "Type", this.defaultTemplate);
        }

        private IEnumerable<string>MatchingDescriptions(string templateName)
        {
            return GetConfigList(templateName, "Matches");
        }

        /// <summary>
        /// Gets the included types list from the TemplateInfo dictionary.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private IEnumerable<string> IncludedObjects(string templateName)
        {
            return GetConfigList(templateName, "Include");
        }

        /// <summary>
        /// Gets the excluded types list from the TemplateInfo dictionary.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private IEnumerable<string> ExcludedObjects(string templateName)
        {
            return GetConfigList(templateName, "Exclude");
        }

        /// <summary>
        /// Gets a list from the config file.  All lists will be ';' delmited.
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="typeListName"></param>
        /// <returns></returns>
        private IEnumerable<string> GetConfigList(string templateName, string listName)
        {
            var templateDictionary = TemplateDictionary(templateName);
            if (templateDictionary != null)
            {
                string list;
                // gets <typeListName> : "semicolon; seperated; types" from the dictionary.
                if (templateDictionary.TryGetValue(listName, out list))
                {
                    foreach(var type in list.Split(';'))
                    {
                        yield return type;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the dictionary from the config file for the given templateName.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private Dictionary<string, string> TemplateDictionary(string templateName)
        {
            Dictionary<string, string> templateDictionary = null;
            this.mapping.TryGetValue(templateName, out templateDictionary);
            return templateDictionary;
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

        /// <summary>
        /// Gets the correct Enum type from the given template
        /// </summary>
        /// <example>
        ///  SubProcessorType processorType;
        ///  if (TryGetValuye<SubProcessorType>(<templateName>, "SubProcessorType", out processorType))
        ///  {
        ///  }
        /// </example>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="templateName"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool TryGetValue<TEnum>(string templateName, string key, out TEnum type) where TEnum : struct  
        {
            bool success = false;
            type = default(TEnum);
            var templateDictionary = TemplateDictionary(templateName);
            if (templateDictionary != null)
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
