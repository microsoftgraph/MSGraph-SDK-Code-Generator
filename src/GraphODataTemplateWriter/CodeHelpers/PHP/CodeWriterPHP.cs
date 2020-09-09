// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.PHP
{
    using Settings;
    using System;
    using System.Collections.Generic;
    using Vipr.Core.CodeModel;

    public class CodeWriterPHP : CodeWriterBase
    {
        public CodeWriterPHP() : base() { }

        public CodeWriterPHP(OdcmModel model) : base(model) { }

        public override String WriteOpeningCommentLine()
        {
            return "<?php" + this.NewLineCharacter + "/**" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine()
        {
            return "*/";
        }

        public override string WriteInlineCommentChar()
        {
            return "* ";
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

        public IEnumerable<string> GetDocBlock(string fileName)
        {
            yield return this.NewLineCharacter + "* " + fileName + " File" + this.NewLineCharacter +
                                "* PHP version 7" + this.NewLineCharacter +
                                "*" + this.NewLineCharacter +
                                "* @category  Library" + this.NewLineCharacter +
                                "* @package   Microsoft.Graph" + this.NewLineCharacter +
                                "* @copyright © Microsoft Corporation. All rights reserved." + this.NewLineCharacter +
                                "* @license   https://opensource.org/licenses/MIT MIT License" + this.NewLineCharacter +
                                "* @link      https://graph.microsoft.com";
        }

        public String GetClassBlock(string fileName, string category)
        {
            return "/**" + this.NewLineCharacter +
                    "* " + fileName + " class" + this.NewLineCharacter +
                    "*" + this.NewLineCharacter +
                    "* @category  " + category + this.NewLineCharacter +
                    "* @package   Microsoft.Graph" + this.NewLineCharacter +
                    "* @copyright © Microsoft Corporation. All rights reserved." + this.NewLineCharacter +
                    "* @license   https://opensource.org/licenses/MIT MIT License" + this.NewLineCharacter +
                    "* @link      https://graph.microsoft.com" + this.NewLineCharacter +
                    "*/";
        }
    }
}
