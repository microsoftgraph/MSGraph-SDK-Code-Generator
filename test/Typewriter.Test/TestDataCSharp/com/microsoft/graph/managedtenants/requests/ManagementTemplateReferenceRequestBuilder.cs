// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityReferenceRequestBuilder.cs.tt

namespace Microsoft.Graph.ManagedTenants
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The type ManagementTemplateReferenceRequestBuilder.
    /// </summary>
    public partial class ManagementTemplateReferenceRequestBuilder : Microsoft.Graph.BaseRequestBuilder, IManagementTemplateReferenceRequestBuilder
    {
        /// <summary>
        /// Constructs a new ManagementTemplateReferenceRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="Microsoft.Graph.IBaseClient"/> for handling requests.</param>
        public ManagementTemplateReferenceRequestBuilder(
            string requestUrl,
            Microsoft.Graph.IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public IManagementTemplateReferenceRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public IManagementTemplateReferenceRequest Request(IEnumerable<Microsoft.Graph.Option> options)
        {
            return new ManagementTemplateReferenceRequest(this.RequestUrl, this.Client, options);
        }
    }
}
