# Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
# Licensed under the MIT License. See LICENSE in the source repository root for license information.

using T4TemplateWriter.Templates;

namespace T4TemplateWriter.Output
{
    public interface IPathWriter
    {
        string WritePath(Template template, string odcmObject);

        void CreateDirectory(string directoryPath);

        bool DirectoryExists(string directoryPath);

        string FileExtension { get; }
    }
}
