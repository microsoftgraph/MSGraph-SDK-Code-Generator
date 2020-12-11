// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: MethodRequestBody.cs.tt

namespace Microsoft.Graph2.CallRecords
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;

    /// <summary>
    /// The type SegmentForwardRequestBody.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class SegmentForwardRequestBody
    {
    
        /// <summary>
        /// Gets or sets ToRecipients.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ToRecipients", Required = Newtonsoft.Json.Required.Default)]
        public IEnumerable<Microsoft.Graph.Recipient> ToRecipients { get; set; }
    
        /// <summary>
        /// Gets or sets SingleRecipient.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "SingleRecipient", Required = Newtonsoft.Json.Required.Default)]
        public Microsoft.Graph.Recipient SingleRecipient { get; set; }
    
        /// <summary>
        /// Gets or sets MultipleSessions.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "MultipleSessions", Required = Newtonsoft.Json.Required.Default)]
        public IEnumerable<Session> MultipleSessions { get; set; }
    
        /// <summary>
        /// Gets or sets SingleSession.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "SingleSession", Required = Newtonsoft.Json.Required.Default)]
        public Session SingleSession { get; set; }
    
        /// <summary>
        /// Gets or sets Comment.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Comment", Required = Newtonsoft.Json.Required.Default)]
        public string Comment { get; set; }
    
    }
}
