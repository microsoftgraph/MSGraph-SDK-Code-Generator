// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityType.cs.tt

namespace Microsoft.Graph.ManagedTenants
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The type Management Template.
    /// </summary>
    public partial class ManagementTemplate : Microsoft.Graph.Entity
    {
    
        /// <summary>
        /// Gets or sets description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }
    
        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    
        /// <summary>
        /// Gets or sets version.
        /// </summary>
        [JsonPropertyName("version")]
        public Int32? Version { get; set; }
    
        /// <summary>
        /// Gets or sets management template collections.
        /// </summary>
        [JsonPropertyName("managementTemplateCollections")]
        public IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesPage ManagementTemplateCollections { get; set; }

        /// <summary>
        /// Gets or sets managementTemplateCollectionsNextLink.
        /// </summary>
        [JsonPropertyName("managementTemplateCollections@odata.nextLink")]
        [JsonConverter(typeof(NextLinkConverter))]
        public string ManagementTemplateCollectionsNextLink { get; set; }
    
    }
}

