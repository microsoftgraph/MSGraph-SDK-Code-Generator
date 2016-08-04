// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using LibGit2Sharp;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Vipr.Core.CodeModel;

    abstract public class CodeWriterBase
    {
        private static String CommitHash { get; set; }

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
                WriteInlineCommentChar() + this.NewLineCharacter,
                WriteInlineCommentChar() + "CodeGen: " + GetGitCommit() + this.NewLineCharacter,
                this.WriteClosingCommentLine()
              });
        }
        
        public static String GetGitCommit()
        {
            if (CodeWriterBase.CommitHash == null)
            {
                using (var repo = new Repository(Repository.Discover(Directory.GetCurrentDirectory())))
                {
                    var diff =
                        repo.Diff.Compare<Patch>(repo.Head.Tip.Tree, DiffTargets.WorkingDirectory | DiffTargets.Index);
                    bool dirty = !String.IsNullOrEmpty(diff.Content);

                    Commit tip = repo.Head.Tip;
                    CodeWriterBase.CommitHash = tip.Sha + (dirty ? "*" : "");

                    if (dirty)
                    {
                        MessageBox.Show(
                            "Your git status is dirty, please commit and re-generate before publishing to SDK's! This will help ensure that we can identify the exact code that generated a given iteration of the SDK.",
                            "WARNING",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }

            return CodeWriterBase.CommitHash;
        }
    }
}
