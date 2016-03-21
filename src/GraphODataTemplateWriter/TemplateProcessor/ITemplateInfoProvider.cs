// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.TemplateProcessor
{
    using System.Collections.Generic;

    public interface ITemplateInfoProvider
    {
        /// <summary>
        /// Creates an IEnumerable of templates.
        /// </summary>
        /// <returns> an IEnumerable of templates.</returns>
        IEnumerable<ITemplateInfo> Templates();
    }
}
