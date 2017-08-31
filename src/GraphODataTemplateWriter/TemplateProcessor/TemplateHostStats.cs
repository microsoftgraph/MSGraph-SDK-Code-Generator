// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.TemplateProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class TemplateHostStats
    {
        private readonly Dictionary<string, List<string>> ProcessedTemplates = new Dictionary<string, List<string>>();

        public void RecordProcessed(ITemplateInfo template, string type, string file)
        {
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
                + this.ProcessedTemplates.ToArray()
                    .Aggregate(string.Empty,
                              (s, kvp) => String.Format("{0} generated {1} files. {2}{3}", kvp.Key, kvp.Value.Count(), Environment.NewLine, s)
                    );
        }
    }
}
