// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.PathWriters
{
    using System;
    using System.IO;
    using Microsoft.Graph.ODataTemplateWriter.CodeHelpers.ObjC;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;

    class ObjCPathWriter : PathWriterBase
    {
        public override string WritePath(ITemplateInfo template, string @namespace, string entityTypeName)
        {
            string coreFileName = this.TransformFileName(template, entityTypeName);
            string prefix = TypeHelperObjC.GetNamespacePrefix(@namespace);
            return Path.Combine(template.OutputParentDirectory, prefix + coreFileName);
        }

        public override string WritePath(ITemplateInfo template, string entityTypeName)
        {
            return WritePath(template, ConfigurationService.Settings.PrimaryNamespaceName, entityTypeName);
        }

    }
}
