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
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>
    /// The type Schedule.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class Schedule : Entity
    {
    
		///<summary>
		/// The Schedule constructor
		///</summary>
        public Schedule()
        {
            this.ODataType = "microsoft.graph.schedule";
        }
	
        /// <summary>
        /// Gets or sets enabled.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "enabled", Required = Newtonsoft.Json.Required.Default)]
        public bool? Enabled { get; set; }
    
        /// <summary>
        /// Gets or sets times off.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "timesOff", Required = Newtonsoft.Json.Required.Default)]
        public IScheduleTimesOffCollectionPage TimesOff { get; set; }
    
        /// <summary>
        /// Gets or sets time off requests.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "timeOffRequests", Required = Newtonsoft.Json.Required.Default)]
        public IScheduleTimeOffRequestsCollectionPage TimeOffRequests { get; set; }
    
    }
}

