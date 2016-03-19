namespace Vipr.T4TemplateWriter.CodeHelpers.Python
{
    using System;
    using Vipr.Core.CodeModel;
    using Vipr.T4TemplateWriter.Settings;

    public class CodeWriterPython : CodeWriterBase
    {

        public CodeWriterPython() : base()
        {
        }

        public CodeWriterPython(OdcmModel model) : base(model)
        {
        }

        public string GetPrefix()
        {
            if (this.CurrentModel != null)
            {
                return ConfigurationService.Settings.NamespacePrefix + this.CurrentModel.EntityContainer.Name;
            }
            else
            {
                return ConfigurationService.Settings.NamespacePrefix;
            }
        }

        public string[] GeneratedHeader {
            get {
                return new string[] {
                    "",
                    " This file was generated and any changes will be overwritten."
                };
            }
        }

        public override String WriteOpeningCommentLine() {
            return "# -*- coding: utf-8 -*- " + this.NewLineCharacter + "\'\'\'" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine() {
            return "\'\'\'" + this.NewLineCharacter;
        }

        public override string WriteInlineCommentChar() {
            return "# ";
        }
    }
}
