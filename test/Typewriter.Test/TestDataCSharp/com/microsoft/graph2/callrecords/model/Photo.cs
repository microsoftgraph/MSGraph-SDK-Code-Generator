// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityType.cs.tt

namespace Microsoft.Graph2.CallRecords
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The type Photo.
    /// </summary>
    public partial class Photo : Microsoft.Graph.Entity
    {
    
        /// <summary>
        /// Gets or sets failure info.
        /// </summary>
        [JsonPropertyName("failureInfo")]
        public FailureInfo FailureInfo { get; set; }
    
        /// <summary>
        /// Gets or sets option.
        /// </summary>
        [JsonPropertyName("option")]
        public Option Option { get; set; }
    
    }
}

