// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: IEntityRequestBuilder.cs.tt

namespace Microsoft.Graph.ManagedTenants
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The interface IManagedTenantRequestBuilder.
    /// </summary>
    public partial interface IManagedTenantRequestBuilder : Microsoft.Graph.IEntityRequestBuilder
    {
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        new IManagedTenantRequest Request();

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        new IManagedTenantRequest Request(IEnumerable<Microsoft.Graph.Option> options);
    
        /// <summary>
        /// Gets the request builder for ManagementTemplateCollections.
        /// </summary>
        /// <returns>The <see cref="IManagedTenantManagementTemplateCollectionsCollectionRequestBuilder"/>.</returns>
        IManagedTenantManagementTemplateCollectionsCollectionRequestBuilder ManagementTemplateCollections { get; }

        /// <summary>
        /// Gets the request builder for ManagementTemplates.
        /// </summary>
        /// <returns>The <see cref="IManagedTenantManagementTemplatesCollectionRequestBuilder"/>.</returns>
        IManagedTenantManagementTemplatesCollectionRequestBuilder ManagementTemplates { get; }
    
    }
}
