// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityCollectionRequestBuilder.cs.tt
namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The type CloudCommunicationsCallRecordsCollectionRequestBuilder.
    /// </summary>
    public partial class CloudCommunicationsCallRecordsCollectionRequestBuilder : BaseRequestBuilder, ICloudCommunicationsCallRecordsCollectionRequestBuilder
    {
        /// <summary>
        /// Constructs a new CloudCommunicationsCallRecordsCollectionRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        public CloudCommunicationsCallRecordsCollectionRequestBuilder(
            string requestUrl,
            IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public ICloudCommunicationsCallRecordsCollectionRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public ICloudCommunicationsCallRecordsCollectionRequest Request(IEnumerable<Option> options)
        {
            return new CloudCommunicationsCallRecordsCollectionRequest(this.RequestUrl, this.Client, options);
        }

        /// <summary>
        /// Gets an <see cref="Microsoft.Graph2.CallRecords.ICallRecordRequestBuilder"/> for the specified CloudCommunicationsMicrosoft.Graph2.CallRecords.CallRecord.
        /// </summary>
        /// <param name="id">The ID for the CloudCommunicationsMicrosoft.Graph2.CallRecords.CallRecord.</param>
        /// <returns>The <see cref="Microsoft.Graph2.CallRecords.ICallRecordRequestBuilder"/>.</returns>
        [System.Runtime.CompilerServices.IndexerName("ThisItem")]
        public Microsoft.Graph2.CallRecords.ICallRecordRequestBuilder this[string id]
        {
            get
            {
                return new Microsoft.Graph2.CallRecords.CallRecordRequestBuilder(this.AppendSegmentToRequestUrl(id), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for CallRecordItem.
        /// </summary>
        /// <returns>The <see cref="Microsoft.Graph2.CallRecords.ICallRecordItemRequestBuilder"/>.</returns>
        public Microsoft.Graph2.CallRecords.ICallRecordItemRequestBuilder Item(
            string name = null)
        {
            return new Microsoft.Graph2.CallRecords.CallRecordItemRequestBuilder(
                this.AppendSegmentToRequestUrl("microsoft.graph2.callRecords.item"),
                this.Client,
                name);
        }
    }
}
