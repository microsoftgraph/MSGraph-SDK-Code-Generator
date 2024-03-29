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
    /// The type ManagementTemplateCollectionManagementTemplatesCollectionWithReferencesRequestBuilder.
    /// </summary>
    public partial class ManagementTemplateCollectionManagementTemplatesCollectionWithReferencesRequestBuilder : Microsoft.Graph.BaseRequestBuilder, IManagementTemplateCollectionManagementTemplatesCollectionWithReferencesRequestBuilder
    {

        /// <summary>
        /// Constructs a new ManagementTemplateCollectionManagementTemplatesCollectionWithReferencesRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="Microsoft.Graph.IBaseClient"/> for handling requests.</param>
        public ManagementTemplateCollectionManagementTemplatesCollectionWithReferencesRequestBuilder(
            string requestUrl,
            Microsoft.Graph.IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public IManagementTemplateCollectionManagementTemplatesCollectionWithReferencesRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public IManagementTemplateCollectionManagementTemplatesCollectionWithReferencesRequest Request(IEnumerable<Microsoft.Graph.Option> options)
        {
            return new ManagementTemplateCollectionManagementTemplatesCollectionWithReferencesRequest(this.RequestUrl, this.Client, options);
        }

        /// <summary>
        /// Gets an <see cref="IManagementTemplateWithReferenceRequestBuilder"/> for the specified ManagementTemplateCollectionManagementTemplate.
        /// </summary>
        /// <param name="id">The ID for the ManagementTemplateCollectionManagementTemplate.</param>
        /// <returns>The <see cref="IManagementTemplateWithReferenceRequestBuilder"/>.</returns>
        public IManagementTemplateWithReferenceRequestBuilder this[string id]
        {
            get
            {
                return new ManagementTemplateWithReferenceRequestBuilder(this.AppendSegmentToRequestUrl(id), this.Client);
            }
        }

        
        /// <summary>
        /// Gets an <see cref="IManagementTemplateCollectionManagementTemplatesCollectionReferencesRequestBuilder"/> for the references in the collection.
        /// </summary>
        /// <returns>The <see cref="IManagementTemplateCollectionManagementTemplatesCollectionReferencesRequestBuilder"/>.</returns>
        public IManagementTemplateCollectionManagementTemplatesCollectionReferencesRequestBuilder References
        {
            get
            {
                return new ManagementTemplateCollectionManagementTemplatesCollectionReferencesRequestBuilder(this.AppendSegmentToRequestUrl("$ref"), this.Client);
            }
        }

    }
}
