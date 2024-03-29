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
    /// The type Workbook Chart.
    /// </summary>
    public partial class WorkbookChart : Entity
    {
    
        /// <summary>
        /// Gets or sets height.
        /// </summary>
        [JsonPropertyName("height")]
        public double? Height { get; set; }
    
        /// <summary>
        /// Gets or sets left.
        /// </summary>
        [JsonPropertyName("left")]
        public double? Left { get; set; }
    
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    
        /// <summary>
        /// Gets or sets top.
        /// </summary>
        [JsonPropertyName("top")]
        public double? Top { get; set; }
    
        /// <summary>
        /// Gets or sets width.
        /// </summary>
        [JsonPropertyName("width")]
        public double? Width { get; set; }
    
    }
}

