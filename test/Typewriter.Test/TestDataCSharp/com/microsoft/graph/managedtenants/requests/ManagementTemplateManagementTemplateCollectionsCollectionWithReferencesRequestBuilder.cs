// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityCollectionWithReferencesRequestBuilder.cs.tt
namespace Microsoft.Graph.ManagedTenants
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The type ManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequestBuilder.
    /// </summary>
    public partial class ManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequestBuilder : Microsoft.Graph.BaseRequestBuilder, IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequestBuilder
    {

        /// <summary>
        /// Constructs a new ManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="Microsoft.Graph.IBaseClient"/> for handling requests.</param>
        public ManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequestBuilder(
            string requestUrl,
            Microsoft.Graph.IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest Request(IEnumerable<Microsoft.Graph.Option> options)
        {
            return new ManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest(this.RequestUrl, this.Client, options);
        }

        /// <summary>
        /// Gets an <see cref="IManagementTemplateCollectionWithReferenceRequestBuilder"/> for the specified ManagementTemplateManagementTemplateCollection.
        /// </summary>
        /// <param name="id">The ID for the ManagementTemplateManagementTemplateCollection.</param>
        /// <returns>The <see cref="IManagementTemplateCollectionWithReferenceRequestBuilder"/>.</returns>
        public IManagementTemplateCollectionWithReferenceRequestBuilder this[string id]
        {
            get
            {
                return new ManagementTemplateCollectionWithReferenceRequestBuilder(this.AppendSegmentToRequestUrl(id), this.Client);
            }
        }

        
        /// <summary>
        /// Gets an <see cref="IManagementTemplateManagementTemplateCollectionsCollectionReferencesRequestBuilder"/> for the references in the collection.
        /// </summary>
        /// <returns>The <see cref="IManagementTemplateManagementTemplateCollectionsCollectionReferencesRequestBuilder"/>.</returns>
        public IManagementTemplateManagementTemplateCollectionsCollectionReferencesRequestBuilder References
        {
            get
            {
                return new ManagementTemplateManagementTemplateCollectionsCollectionReferencesRequestBuilder(this.AppendSegmentToRequestUrl("$ref"), this.Client);
            }
        }

    }
}