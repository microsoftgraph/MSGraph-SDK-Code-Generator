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

        private IList<Dictionary<string, string>> mapping;
        private string templatesDirectory;
        private FileNameCasing defaultCasing;
        private SubProcessor defaultSubProcessor;
        private Template defaultTemplate;

        public TemplateInfoProvider(IList<Dictionary<string,string>> mapping, 
                                    string templatesDirectory,
                                    FileNameCasing defaultNameCasing = FileNameCasing.UpperCamel, 
                                    SubProcessor defaultSubProcessor = SubProcessor.Other,
                                    Template defaultTempalte = Template.Other)
        {
            this.mapping = mapping;
            this.templatesDirectory = templatesDirectory;
            this.defaultCasing = defaultNameCasing;
            this.defaultSubProcessor = defaultSubProcessor;
            this.defaultTemplate = defaultTemplate;
        }

        public IEnumerable<ITemplateInfo> Templates()
        {

            var templates = this.ReadTemplateFiles();
            foreach (var templateInfo in mapping)
            {
                string templateName = null;
                if (templateInfo.TryGetValue("Template", out templateName) && templates.ContainsKey(templateName))
                {
                    yield return this.Create(templates[templateName], templateInfo);
                }
            }
        }

        private Dictionary<string, string> ReadTemplateFiles()
        {
            var templates = new Dictionary<string, string>();
            foreach (string path in (Directory.EnumerateFiles(this.templatesDirectory, "*.*.tt", SearchOption.AllDirectories)))
            {
                // Remove the .tt and then remove the file type extension
                var templateName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path));
                templates[templateName] = path;
            }
            return templates;
        }

        public ITemplateInfo Create(string fullPath, Dictionary<string, string> templateDictionary)
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
            fileInfo.TemplateType = this.GetTemplate(templateDictionary);
            fileInfo.SubprocessorType = this.GetSubProcessor(templateDictionary);
            fileInfo.Casing = this.GetFileNameCasing(templateDictionary);


            IEnumerable<string> includedObjects = IncludedObjects(templateDictionary);
            IEnumerable<string> excludedObjects = ExcludedObjects(templateDictionary);
            IEnumerable<string> matchingDescriptions = MatchingDescriptions(templateDictionary);

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

           string nameFormat;
	       if (templateDictionary.TryGetValue("Name", out nameFormat))
	       {
               fileInfo.NameFormat = nameFormat;
	       }
            return fileInfo;
        }

        /// <summary>
        /// Gets the casing for the given template.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private FileNameCasing GetFileNameCasing(Dictionary<string,string> template)
        {
            return GetEnum<FileNameCasing>(template, "Case",  this.defaultCasing);
        }

        /// <summary>
        /// Gets the subprocessor type for the given template.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private SubProcessor GetSubProcessor(Dictionary<string, string> template)
        {
            return GetEnum<SubProcessor>(template, "SubProcessor", this.defaultSubProcessor);
        }
        
        /// <summary>
        /// Gets the TemplateType for the given template.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private Template GetTemplate(Dictionary<string, string> template)
        {
            return GetEnum<Template>(template, "Type", this.defaultTemplate);
        }

        /// <summary>
        /// Gets the list of strings in the "Matches" list from the template.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private IEnumerable<string>MatchingDescriptions(Dictionary<string, string> template)
        {
            return GetConfigList(template, "Matches");
        }

        /// <summary>
        /// Gets the included types list from the TemplateInfo dictionary.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private IEnumerable<string> IncludedObjects(Dictionary<string, string> template)
        {
            return GetConfigList(template, "Include");
        }

        /// <summary>
        /// Gets the excluded types list from the TemplateInfo dictionary.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private IEnumerable<string> ExcludedObjects(Dictionary<string, string> template)
        {
            return GetConfigList(template, "Exclude");
        }

        /// <summary>
        /// Gets a list from the config file.  All lists are ';' delmited.
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="typeListName"></param>
        /// <returns>An enumerable of the ";" delimited list.</returns>
        private IEnumerable<string> GetConfigList(Dictionary<string, string> template, string listName)
        {
            string list;
            // gets <typeListName> : "semicolon; seperated; types" from the dictionary.
            if (template.TryGetValue(listName, out list))
            {
                foreach(var type in list.Split(';'))
                {
                    yield return type;
                }
            }
        }

        /// <summary>
        /// Gets the specfic enum for the given template and key
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="templateName"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns>An anum value</returns>
        private TEnum GetEnum<TEnum>(Dictionary<string, string> template, string key, TEnum defaultValue) where TEnum : struct
        {
            TEnum type;
            if (!TryGetValue<TEnum>(template, key, out type))
            {
                type = defaultValue;
            }
            return type;
        }

        /// <summary>
        /// Gets the correct Enum type from the given template
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="templateName"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns>true if successful false if not</returns>
        private bool TryGetValue<TEnum>(Dictionary<string, string> template, string key, out TEnum type) where TEnum : struct  
        {
            bool success = false;
            type = default(TEnum);
            TEnum outType = default(TEnum);
            string enumType;
            template.TryGetValue(key, out enumType);
            success = Enum.TryParse(enumType, out outType);
            if (success)
            {
                type = outType;
            }
            return success;
        }

    }

}
