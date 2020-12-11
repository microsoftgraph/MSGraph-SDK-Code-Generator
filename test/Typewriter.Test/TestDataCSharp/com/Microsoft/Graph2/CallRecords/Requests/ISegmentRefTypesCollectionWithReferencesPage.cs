// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: IEntityCollectionWithReferencesPage.cs.tt

namespace Microsoft.Graph2.CallRecords
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// The interface ISegmentRefTypesCollectionWithReferencesPage.
    /// </summary>
    [JsonConverter(typeof(Microsoft.Graph.InterfaceConverter<SegmentRefTypesCollectionWithReferencesPage>))]
    public interface ISegmentRefTypesCollectionWithReferencesPage : Microsoft.Graph.ICollectionPage<Microsoft.Graph.EntityType3>
    {
        /// <summary>
        /// Gets the next page <see cref="ISegmentRefTypesCollectionWithReferencesRequest"/> instance.
        /// </summary>
        ISegmentRefTypesCollectionWithReferencesRequest NextPageRequest { get; }

        /// <summary>
        /// Initializes the NextPageRequest property.
        /// </summary>
        void InitializeNextPageRequest(Microsoft.Graph.IBaseClient client, string nextPageLinkString);
    }
}
