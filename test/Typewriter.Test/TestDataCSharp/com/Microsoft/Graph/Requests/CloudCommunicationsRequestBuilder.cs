// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityRequestBuilder.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The type CloudCommunicationsRequestBuilder.
    /// </summary>
    public partial class CloudCommunicationsRequestBuilder : EntityRequestBuilder, ICloudCommunicationsRequestBuilder
    {

        /// <summary>
        /// Constructs a new CloudCommunicationsRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        public CloudCommunicationsRequestBuilder(
            string requestUrl,
            IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public new ICloudCommunicationsRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public new ICloudCommunicationsRequest Request(IEnumerable<Option> options)
        {
            return new CloudCommunicationsRequest(this.RequestUrl, this.Client, options);
        }
    
        /// <summary>
        /// Gets the request builder for Calls.
        /// </summary>
        /// <returns>The <see cref="ICloudCommunicationsCallsCollectionRequestBuilder"/>.</returns>
        public ICloudCommunicationsCallsCollectionRequestBuilder Calls
        {
            get
            {
                return new CloudCommunicationsCallsCollectionRequestBuilder(this.AppendSegmentToRequestUrl("calls"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for CallRecords.
        /// </summary>
        /// <returns>The <see cref="ICloudCommunicationsCallRecordsCollectionRequestBuilder"/>.</returns>
        public ICloudCommunicationsCallRecordsCollectionRequestBuilder CallRecords
        {
            get
            {
                return new CloudCommunicationsCallRecordsCollectionRequestBuilder(this.AppendSegmentToRequestUrl("callRecords"), this.Client);
            }
        }
    
    }
}
