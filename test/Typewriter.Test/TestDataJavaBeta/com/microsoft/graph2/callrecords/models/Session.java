// Template Source: BaseEntity.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph2.callrecords.models;
import com.microsoft.graph.serializer.ISerializer;
import com.microsoft.graph.serializer.IJsonBackedObject;
import com.microsoft.graph.serializer.AdditionalDataManager;
import java.util.EnumSet;
import com.microsoft.graph.http.BaseCollectionPage;
import com.microsoft.graph2.callrecords.models.Modality;
import com.microsoft.graph2.callrecords.models.Endpoint;
import com.microsoft.graph2.callrecords.models.FailureInfo;
import com.microsoft.graph.models.Entity;
import com.microsoft.graph2.callrecords.requests.SegmentCollectionPage;


import com.google.gson.JsonObject;
import com.google.gson.annotations.SerializedName;
import com.google.gson.annotations.Expose;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Session.
 */
public class Session extends Entity implements IJsonBackedObject {


    /**
     * The Modalities.
     * 
     */
    @SerializedName(value = "modalities", alternate = {"Modalities"})
    @Expose
	@Nullable
    public java.util.List<Modality> modalities;

    /**
     * The Start Date Time.
     * 
     */
    @SerializedName(value = "startDateTime", alternate = {"StartDateTime"})
    @Expose
	@Nullable
    public java.time.OffsetDateTime startDateTime;

    /**
     * The End Date Time.
     * 
     */
    @SerializedName(value = "endDateTime", alternate = {"EndDateTime"})
    @Expose
	@Nullable
    public java.time.OffsetDateTime endDateTime;

    /**
     * The Caller.
     * 
     */
    @SerializedName(value = "caller", alternate = {"Caller"})
    @Expose
	@Nullable
    public Endpoint caller;

    /**
     * The Callee.
     * 
     */
    @SerializedName(value = "callee", alternate = {"Callee"})
    @Expose
	@Nullable
    public Endpoint callee;

    /**
     * The Failure Info.
     * 
     */
    @SerializedName(value = "failureInfo", alternate = {"FailureInfo"})
    @Expose
	@Nullable
    public FailureInfo failureInfo;

    /**
     * The Segments.
     * 
     */
    @SerializedName(value = "segments", alternate = {"Segments"})
    @Expose
	@Nullable
    public com.microsoft.graph2.callrecords.requests.SegmentCollectionPage segments;


    /**
     * Sets the raw JSON object
     *
     * @param serializer the serializer
     * @param json the JSON object to set this object to
     */
    public void setRawObject(@Nonnull final ISerializer serializer, @Nonnull final JsonObject json) {


        if (json.has("segments")) {
            segments = serializer.deserializeObject(json.get("segments"), com.microsoft.graph2.callrecords.requests.SegmentCollectionPage.class);
        }
    }
}
