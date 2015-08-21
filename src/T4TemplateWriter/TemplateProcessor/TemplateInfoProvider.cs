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

        /// <summary>
        /// Creates a TemplateInfoProvider
        /// </summary>
        /// <param name="mapping">A the mapping of templates to subprocessors fromt he config file.</param>
        /// <param name="templatesDirectory">The tempaltes directory, this should be the Platform specific directory</param>
        /// <param name="defaultNameCasing"></param>
        /// <param name="defaultSubProcessor"></param>
        /// <param name="defaultTempalte"></param>
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
                    foreach (var templatePath in templates[templateName])
                    {
                        yield return this.Create(templatePath, templateInfo);
                    }
                }
            }
        }

        /// <summary>
        /// Reads all of the templates available from the given directory.
        /// </summary>
        /// <returns>A dictionary mapping of template name to path</returns>
        private Dictionary<string, IList<string>> ReadTemplateFiles()
        {
            var templates = new Dictionary<string, IList<string>>();
            foreach (string path in (Directory.EnumerateFiles(this.templatesDirectory, "*.*.tt", SearchOption.AllDirectories)))
            {
                // Remove the .tt and then remove the file type extension
                var templateName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path));
                IList<string> templatePaths = null;
                if (templates.TryGetValue(templateName, out templatePaths))
                {
                    templatePaths.Add(path);
                }
                else
                {
                    templates[templateName] = new List<string>() { path };
                }
            }
            return templates;
        }

        /// <summary>
        /// Creates an ITemplateInfo with the given path to a template file and the dictioanry from the config file.
        /// </summary>
        /// <param name="fullPath">The path to the template file</param>
        /// <param name="templateDictionary">the template dictionary from the config file</param>
        /// <returns>A new ITemplateInfo</returns>
        private ITemplateInfo Create(string fullPath, Dictionary<string, string> templateDictionary)
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


            IEnumerable<string> includedObjects = this.IncludedObjects(templateDictionary);
            IEnumerable<string> excludedObjects = this.ExcludedObjects(templateDictionary);
            IEnumerable<string> ignoreDescriptions = this.IgnoreDescriptions(templateDictionary);
            IEnumerable<string> matchingDescriptions = this.MatchingDescriptions(templateDictionary);

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
                fileInfo.MatchingDescriptions = matchingDescriptions;
            }
            if (ignoreDescriptions.Count() != 0)
            {
                fileInfo.IgnoreDescriptions = ignoreDescriptions;
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
        /// <param name="template">The template dictionary from the configuration file</param>
        /// <returns></returns>
        private FileNameCasing GetFileNameCasing(Dictionary<string,string> template)
        {
            return GetEnum<FileNameCasing>(template, "Case",  this.defaultCasing);
        }

        /// <summary>
        /// Gets the subprocessor type for the given template.
        /// </summary>
        /// <param name="template">The template dictionary from the configuration file</param>
        /// <returns></returns>
        private SubProcessor GetSubProcessor(Dictionary<string, string> template)
        {
            return GetEnum<SubProcessor>(template, "SubProcessor", this.defaultSubProcessor);
        }
        
        /// <summary>
        /// Gets the TemplateType for the given template.
        /// </summary>
        /// <param name="template">The template dictionary from the configuration file</param>
        /// <returns></returns>
        private Template GetTemplate(Dictionary<string, string> template)
        {
            return GetEnum<Template>(template, "Type", this.defaultTemplate);
        }

        /// <summary>
        /// Gets the list of strings in the "Ignore" list from the template.
        /// </summary>
        /// <param name="template">Teh template dirctionary from the configuration file</param>
        /// <returns></returns>
        private IEnumerable<string>IgnoreDescriptions(Dictionary<string, string> template)
        {
            return GetConfigList(template, "Ignore");
        }

        /// <summary>
        /// Gets the list of strings in the "Matches" list from the template.
        /// </summary>
        /// <param name="template">The template dictionary from the configuration file</param>
        /// <returns></returns>
        private IEnumerable<string>MatchingDescriptions(Dictionary<string, string> template)
        {
            return GetConfigList(template, "Matches");
        }

        /// <summary>
        /// Gets the included types list from the TemplateInfo dictionary.
        /// </summary>
        /// <param name="template">The template dictionary from the configuration file</param>
        /// <returns></returns>
        private IEnumerable<string> IncludedObjects(Dictionary<string, string> template)
        {
            return GetConfigList(template, "Include");
        }

        /// <summary>
        /// Gets the excluded types list from the TemplateInfo dictionary.
        /// </summary>
        /// <param name="template">The template dictionary from the configuration file</param>
        /// <returns></returns>
        private IEnumerable<string> ExcludedObjects(Dictionary<string, string> template)
        {
            return GetConfigList(template, "Exclude");
        }

        /// <summary>
        /// Gets a list from the config file.  All lists are ';' delmited.
        /// </summary>
        /// <param name="template">The template dictionary from the configuration file</param>
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
        /// <param name="template">The template dictionary from the configuration file</param>
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
        /// <param name="template">The template dictionary from the configuration file</param>
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
