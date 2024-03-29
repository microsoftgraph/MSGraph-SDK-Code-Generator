// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: IEntityCollectionReferencesRequest.cs.tt

namespace Microsoft.Graph2.CallRecords
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;

    /// <summary>
    /// The interface ISegmentRefTypesCollectionReferencesRequest.
    /// </summary>
    public partial interface ISegmentRefTypesCollectionReferencesRequest : Microsoft.Graph.IBaseRequest
    {
        /// <summary>
        /// Adds the specified Microsoft.Graph.EntityType3 to the collection via POST.
        /// </summary>
        /// <param name="entityType3">The Microsoft.Graph.EntityType3 to add.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        System.Threading.Tasks.Task AddAsync(Microsoft.Graph.EntityType3 entityType3, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the specified Microsoft.Graph.EntityType3 to the collection via POST and returns a <see cref="GraphResponse{Microsoft.Graph.EntityType3}"/> object of the request.
        /// </summary>
        /// <param name="entityType3">The Microsoft.Graph.EntityType3 to add.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        System.Threading.Tasks.Task<GraphResponse> AddResponseAsync(Microsoft.Graph.EntityType3 entityType3, CancellationToken cancellationToken = default);

    }
}
