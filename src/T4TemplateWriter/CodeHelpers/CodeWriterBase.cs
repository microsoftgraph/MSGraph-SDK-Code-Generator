// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Vipr.Core.CodeModel;

    abstract public class CodeWriterBase
    {
        public OdcmModel CurrentModel
        {
            get;
            set;
        }

        public CodeWriterBase() : this(null)
        {
        }

        public CodeWriterBase(OdcmModel model)
        {
            this.CurrentModel = model;
        }
        public virtual String NewLineCharacter
        {
            get { return Environment.NewLine; }
        }

        public static String Write(params String[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (String arg in args)
            {
                sb.Append(arg);
            }
            return sb.ToString();
        }

        abstract public String WriteOpeningCommentLine();

        abstract public String WriteClosingCommentLine();

        abstract public String WriteInlineCommentChar();

        public String WriteHeader(IEnumerable<string> additionalHeader = null)
        {
            return Write(new String[] {
                this.WriteOpeningCommentLine(),
                string.Join(this.NewLineCharacter,
                    ConfigurationService.Settings.LicenseHeader
                        .Select(line => this.WriteInlineCommentChar() + line)
                        .ToArray()),
                this.NewLineCharacter,
                additionalHeader != null
                    ? string.Join(this.NewLineCharacter,
                        additionalHeader
                            .Select(line => this.WriteInlineCommentChar() + line)
                            .ToArray()) + this.NewLineCharacter
                    : "",
                this.WriteClosingCommentLine()
              });

        }
    }
}
