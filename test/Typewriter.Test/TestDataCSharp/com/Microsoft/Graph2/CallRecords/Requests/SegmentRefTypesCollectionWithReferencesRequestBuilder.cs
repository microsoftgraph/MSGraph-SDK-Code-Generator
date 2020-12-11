// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityCollectionWithReferencesRequestBuilder.cs.tt
namespace Microsoft.Graph2.CallRecords
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The type SegmentRefTypesCollectionWithReferencesRequestBuilder.
    /// </summary>
    public partial class SegmentRefTypesCollectionWithReferencesRequestBuilder : Microsoft.Graph.BaseRequestBuilder, ISegmentRefTypesCollectionWithReferencesRequestBuilder
    {

        /// <summary>
        /// Constructs a new SegmentRefTypesCollectionWithReferencesRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="Microsoft.Graph.IBaseClient"/> for handling requests.</param>
        public SegmentRefTypesCollectionWithReferencesRequestBuilder(
            string requestUrl,
            Microsoft.Graph.IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public ISegmentRefTypesCollectionWithReferencesRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public ISegmentRefTypesCollectionWithReferencesRequest Request(IEnumerable<Microsoft.Graph.Option> options)
        {
            return new SegmentRefTypesCollectionWithReferencesRequest(this.RequestUrl, this.Client, options);
        }

        /// <summary>
        /// Gets an <see cref="Microsoft.Graph.IEntityType3WithReferenceRequestBuilder"/> for the specified SegmentMicrosoft.Graph.EntityType3.
        /// </summary>
        /// <param name="id">The ID for the SegmentMicrosoft.Graph.EntityType3.</param>
        /// <returns>The <see cref="Microsoft.Graph.IEntityType3WithReferenceRequestBuilder"/>.</returns>
        public Microsoft.Graph.IEntityType3WithReferenceRequestBuilder this[string id]
        {
            get
            {
                return new Microsoft.Graph.EntityType3WithReferenceRequestBuilder(this.AppendSegmentToRequestUrl(id), this.Client);
            }
        }

        
        /// <summary>
        /// Gets an <see cref="ISegmentRefTypesCollectionReferencesRequestBuilder"/> for the references in the collection.
        /// </summary>
        /// <returns>The <see cref="ISegmentRefTypesCollectionReferencesRequestBuilder"/>.</returns>
        public ISegmentRefTypesCollectionReferencesRequestBuilder References
        {
            get
            {
                return new SegmentRefTypesCollectionReferencesRequestBuilder(this.AppendSegmentToRequestUrl("$ref"), this.Client);
            }
        }

    }
}
