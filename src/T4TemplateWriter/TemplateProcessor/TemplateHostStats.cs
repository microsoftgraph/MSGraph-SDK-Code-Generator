using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    internal class TemplateHostStats
    {
        private Dictionary<string, List<string>> ProcessedTemplates = new Dictionary<string, List<string>>();

        public void RecordProcessed(ITemplateInfo template, string type, string file)
        {
            Console.WriteLine("{0}\t{1}\t->\t\t{2}", template.TemplateName, type, file);
            if (!this.ProcessedTemplates.ContainsKey(template.TemplateName))
            {
                this.ProcessedTemplates.Add(template.TemplateName, new List<string> { file });
            }
            else
            {
                this.ProcessedTemplates[template.TemplateName].Add(file);
            }
        }

        public void RecordProcessedNothing(ITemplateInfo template)
        {
            if (!this.ProcessedTemplates.ContainsKey(template.TemplateName))
            {
                this.ProcessedTemplates.Add(template.TemplateName, new List<string>());
            }
        }

        public override string ToString()
        {
            return "Template Host Stats" + Environment.NewLine + Environment.NewLine
                + ProcessedTemplates.ToArray()
                    .Aggregate(string.Empty,
                              (s, kvp) => String.Format("{0} generated {1} files. {2}{3}", kvp.Key, kvp.Value.Count(), Environment.NewLine, s)
                    );
        }
    }
}
