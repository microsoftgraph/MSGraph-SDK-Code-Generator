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
    /// The type ServiceUserAgent.
    /// </summary>
    [JsonConverter(typeof(Microsoft.Graph.DerivedTypeConverter<ServiceUserAgent>))]
    public partial class ServiceUserAgent : UserAgent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceUserAgent"/> class.
        /// </summary>
        public ServiceUserAgent()
        {
            this.ODataType = "microsoft.graph2.callRecords.serviceUserAgent";
        }

        /// <summary>
        /// Gets or sets role.
        /// </summary>
        [JsonPropertyName("role")]
        public ServiceRole? Role { get; set; }
    
    }
}
