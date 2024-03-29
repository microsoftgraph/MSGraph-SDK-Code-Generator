// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: ComplexType.cs.tt

namespace Microsoft.Graph2.CallRecords
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The type MediaStream.
    /// </summary>
    [JsonConverter(typeof(Microsoft.Graph.DerivedTypeConverter<MediaStream>))]
    public partial class MediaStream
    {

        /// <summary>
        /// Gets or sets streamId.
        /// </summary>
        [JsonPropertyName("streamId")]
        public string StreamId { get; set; }
    
        /// <summary>
        /// Gets or sets startDateTime.
        /// </summary>
        [JsonPropertyName("startDateTime")]
        public DateTimeOffset? StartDateTime { get; set; }
    
        /// <summary>
        /// Gets or sets streamDirection.
        /// </summary>
        [JsonPropertyName("streamDirection")]
        public MediaStreamDirection? StreamDirection { get; set; }
    
        /// <summary>
        /// Gets or sets packetUtilization.
        /// </summary>
        [JsonPropertyName("packetUtilization")]
        public Int64? PacketUtilization { get; set; }
    
        /// <summary>
        /// Gets or sets wasMediaBypassed.
        /// </summary>
        [JsonPropertyName("wasMediaBypassed")]
        public bool? WasMediaBypassed { get; set; }
    
        /// <summary>
        /// Gets or sets lowVideoProcessingCapabilityRatio.
        /// </summary>
        [JsonPropertyName("lowVideoProcessingCapabilityRatio")]
        public Single? LowVideoProcessingCapabilityRatio { get; set; }
    
        /// <summary>
        /// Gets or sets averageAudioNetworkJitter.
        /// </summary>
        [JsonPropertyName("averageAudioNetworkJitter")]
        public Microsoft.Graph.Duration AverageAudioNetworkJitter { get; set; }
    
        /// <summary>
        /// Gets or sets additional data.
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets @odata.type.
        /// </summary>
        [JsonPropertyName("@odata.type")]
        public string ODataType { get; set; }
    
    }
}
