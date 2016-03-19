// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.PathWriters
{
    using System;
    using System.IO;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;

    class ObjCPathWriter : PathWriterBase
    {

        public override string WritePath(ITemplateInfo template, string entityTypeName)
        {
            string prefix = ConfigurationService.Settings.NamespacePrefix;
            string coreFileName = this.TransformFileName(template, entityTypeName);

            return Path.Combine(
                template.OutputParentDirectory, 
                String.Format("{0}{1}",
                    prefix,
                    coreFileName
                )
            );
        }

    }
}
