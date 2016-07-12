// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Python
{
    using System;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Vipr.Core.CodeModel;

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
            return "# -*- coding: utf-8 -*- " + this.NewLineCharacter + "\"\"\"" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine() {
            return "\"\"\"" + this.NewLineCharacter;
        }

        public override string WriteInlineCommentChar() {
            return "# ";
        }
    }
}
