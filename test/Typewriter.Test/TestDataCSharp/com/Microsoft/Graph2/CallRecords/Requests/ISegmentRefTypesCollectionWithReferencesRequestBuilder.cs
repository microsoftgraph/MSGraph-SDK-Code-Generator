// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: IEntityCollectionWithReferencesRequestBuilder.cs.tt
namespace Microsoft.Graph2.CallRecords
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The interface ISegmentRefTypesCollectionWithReferencesRequestBuilder.
    /// </summary>
    public partial interface ISegmentRefTypesCollectionWithReferencesRequestBuilder : Microsoft.Graph.IBaseRequestBuilder
    {
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        ISegmentRefTypesCollectionWithReferencesRequest Request();

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        ISegmentRefTypesCollectionWithReferencesRequest Request(IEnumerable<Microsoft.Graph.Option> options);

        /// <summary>
        /// Gets an <see cref="Microsoft.Graph.IEntityType3WithReferenceRequestBuilder"/> for the specified Microsoft.Graph.EntityType3.
        /// </summary>
        /// <param name="id">The ID for the Microsoft.Graph.EntityType3.</param>
        /// <returns>The <see cref="Microsoft.Graph.IEntityType3WithReferenceRequestBuilder"/>.</returns>
        Microsoft.Graph.IEntityType3WithReferenceRequestBuilder this[string id] { get; }
        
        /// <summary>
        /// Gets an <see cref="ISegmentRefTypesCollectionReferencesRequestBuilder"/> for the references in the collection.
        /// </summary>
        /// <returns>The <see cref="ISegmentRefTypesCollectionReferencesRequestBuilder"/>.</returns>
        ISegmentRefTypesCollectionReferencesRequestBuilder References { get; }

    }
}
