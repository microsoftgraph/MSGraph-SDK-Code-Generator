// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: Templates\CSharp\Model\ComplexType.cs.tt

namespace Microsoft.Graph2.CallRecords
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// The type ClientUserAgent.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class ClientUserAgent : UserAgent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientUserAgent"/> class.
        /// </summary>
        public ClientUserAgent()
        {
            this.ODataType = "microsoft.graph2.callRecords.clientUserAgent";
        }

        /// <summary>
        /// Gets or sets platform.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "platform", Required = Newtonsoft.Json.Required.Default)]
        public ClientPlatform? Platform { get; set; }
    
        /// <summary>
        /// Gets or sets productFamily.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "productFamily", Required = Newtonsoft.Json.Required.Default)]
        public ProductFamily? ProductFamily { get; set; }
    
    }
}
