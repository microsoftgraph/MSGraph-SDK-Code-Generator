// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityCollectionPage.cs.tt

namespace Microsoft.Graph
{
    using System;

    /// <summary>
    /// The type SecurityAlerts_v2CollectionPage.
    /// </summary>
    public partial class SecurityAlerts_v2CollectionPage : CollectionPage<Microsoft.Graph.SecurityNamespace.Alert>, ISecurityAlerts_v2CollectionPage
    {
        /// <summary>
        /// Gets the next page <see cref="ISecurityAlerts_v2CollectionRequest"/> instance.
        /// </summary>
        public ISecurityAlerts_v2CollectionRequest NextPageRequest { get; private set; }

        /// <summary>
        /// Initializes the NextPageRequest property.
        /// </summary>
        public void InitializeNextPageRequest(IBaseClient client, string nextPageLinkString)
        {
            if (!string.IsNullOrEmpty(nextPageLinkString))
            {
                this.NextPageRequest = new SecurityAlerts_v2CollectionRequest(
                    nextPageLinkString,
                    client,
                    null);
            }
        }
    }
}
