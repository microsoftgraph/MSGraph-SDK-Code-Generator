// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: IEntityCollectionRequestBuilder.cs.tt
namespace Microsoft.Graph.ManagedTenants
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The interface IManagedTenantManagementTemplateCollectionsCollectionRequestBuilder.
    /// </summary>
    public partial interface IManagedTenantManagementTemplateCollectionsCollectionRequestBuilder : Microsoft.Graph.IBaseRequestBuilder
    {
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        IManagedTenantManagementTemplateCollectionsCollectionRequest Request();

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        IManagedTenantManagementTemplateCollectionsCollectionRequest Request(IEnumerable<Microsoft.Graph.Option> options);

        /// <summary>
        /// Gets an <see cref="IManagementTemplateCollectionRequestBuilder"/> for the specified ManagementTemplateCollection.
        /// </summary>
        /// <param name="id">The ID for the ManagementTemplateCollection.</param>
        /// <returns>The <see cref="IManagementTemplateCollectionRequestBuilder"/>.</returns>
        IManagementTemplateCollectionRequestBuilder this[string id] { get; }

        
    }
}