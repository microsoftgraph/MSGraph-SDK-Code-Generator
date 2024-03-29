// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityType.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The type Security.
    /// </summary>
    public partial class Security : Entity
    {
    
        /// <summary>
        /// Gets or sets alerts_v2.
        /// </summary>
        [JsonPropertyName("alerts_v2")]
        public ISecurityAlerts_v2CollectionPage Alerts_v2 { get; set; }

        /// <summary>
        /// Gets or sets alerts_v2NextLink.
        /// </summary>
        [JsonPropertyName("alerts_v2@odata.nextLink")]
        [JsonConverter(typeof(NextLinkConverter))]
        public string Alerts_v2NextLink { get; set; }
    
        /// <summary>
        /// Gets or sets alerts.
        /// </summary>
        [JsonPropertyName("alerts")]
        public ISecurityAlertsCollectionPage Alerts { get; set; }

        /// <summary>
        /// Gets or sets alertsNextLink.
        /// </summary>
        [JsonPropertyName("alerts@odata.nextLink")]
        [JsonConverter(typeof(NextLinkConverter))]
        public string AlertsNextLink { get; set; }
    
    }
}

