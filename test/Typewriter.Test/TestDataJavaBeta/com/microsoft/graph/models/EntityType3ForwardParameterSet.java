// Template Source: BaseMethodParameterSet.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.models;

import com.microsoft.graph.models.Recipient;
import com.microsoft.graph2.callrecords.models.Session;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import javax.annotation.Nonnull;
import javax.annotation.Nullable;
import com.google.gson.JsonObject;
import com.microsoft.graph.serializer.ISerializer;
import java.util.EnumSet;
import java.util.ArrayList;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Entity Type3Forward Parameter Set.
 * @deprecated entityType3 is deprecated. Please use singletonEntity1.
 */
@Deprecated
public class EntityType3ForwardParameterSet {
    /**
     * The to Recipients.
     * 
     */
    @SerializedName(value = "toRecipients", alternate = {"ToRecipients"})
    @Expose
	@Nullable
    public java.util.List<Recipient> toRecipients;

    /**
     * The single Recipient.
     * 
     */
    @SerializedName(value = "singleRecipient", alternate = {"SingleRecipient"})
    @Expose
	@Nullable
    public Recipient singleRecipient;

    /**
     * The multiple Sessions.
     * 
     */
    @SerializedName(value = "multipleSessions", alternate = {"MultipleSessions"})
    @Expose
	@Nullable
    public java.util.List<Session> multipleSessions;

    /**
     * The single Session.
     * 
     */
    @SerializedName(value = "singleSession", alternate = {"SingleSession"})
    @Expose
	@Nullable
    public Session singleSession;

    /**
     * The comment.
     * 
     */
    @SerializedName(value = "comment", alternate = {"Comment"})
    @Expose
	@Nullable
    public String comment;


    /**
     * Instiaciates a new EntityType3ForwardParameterSet
     */
    public EntityType3ForwardParameterSet() {}
    /**
     * Instiaciates a new EntityType3ForwardParameterSet
     * @param builder builder bearing the parameters to initialize from
     */
    protected EntityType3ForwardParameterSet(@Nonnull final EntityType3ForwardParameterSetBuilder builder) {
        this.toRecipients = builder.toRecipients;
        this.singleRecipient = builder.singleRecipient;
        this.multipleSessions = builder.multipleSessions;
        this.singleSession = builder.singleSession;
        this.comment = builder.comment;
    }
    /**
     * Gets a new builder for the body
     * @return a new builder
     */
    @Nonnull
    public static EntityType3ForwardParameterSetBuilder newBuilder() {
        return new EntityType3ForwardParameterSetBuilder();
    }
    /**
     * Fluent builder for the EntityType3ForwardParameterSet
     */
    public static final class EntityType3ForwardParameterSetBuilder {
        /**
         * The toRecipients parameter value
         */
        @Nullable
        protected java.util.List<Recipient> toRecipients;
        /**
         * Sets the ToRecipients
         * @param val the value to set it to
         * @return the current builder object
         */
        @Nonnull
        public EntityType3ForwardParameterSetBuilder withToRecipients(@Nullable final java.util.List<Recipient> val) {
            this.toRecipients = val;
            return this;
        }
        /**
         * The singleRecipient parameter value
         */
        @Nullable
        protected Recipient singleRecipient;
        /**
         * Sets the SingleRecipient
         * @param val the value to set it to
         * @return the current builder object
         */
        @Nonnull
        public EntityType3ForwardParameterSetBuilder withSingleRecipient(@Nullable final Recipient val) {
            this.singleRecipient = val;
            return this;
        }
        /**
         * The multipleSessions parameter value
         */
        @Nullable
        protected java.util.List<Session> multipleSessions;
        /**
         * Sets the MultipleSessions
         * @param val the value to set it to
         * @return the current builder object
         */
        @Nonnull
        public EntityType3ForwardParameterSetBuilder withMultipleSessions(@Nullable final java.util.List<Session> val) {
            this.multipleSessions = val;
            return this;
        }
        /**
         * The singleSession parameter value
         */
        @Nullable
        protected Session singleSession;
        /**
         * Sets the SingleSession
         * @param val the value to set it to
         * @return the current builder object
         */
        @Nonnull
        public EntityType3ForwardParameterSetBuilder withSingleSession(@Nullable final Session val) {
            this.singleSession = val;
            return this;
        }
        /**
         * The comment parameter value
         */
        @Nullable
        protected String comment;
        /**
         * Sets the Comment
         * @param val the value to set it to
         * @return the current builder object
         */
        @Nonnull
        public EntityType3ForwardParameterSetBuilder withComment(@Nullable final String val) {
            this.comment = val;
            return this;
        }
        /**
         * Instanciates a new EntityType3ForwardParameterSetBuilder
         */
        @Nullable
        protected EntityType3ForwardParameterSetBuilder(){}
        /**
         * Buils the resulting body object to be passed to the request
         * @return the body object to pass to the request
         */
        @Nonnull
        public EntityType3ForwardParameterSet build() {
            return new EntityType3ForwardParameterSet(this);
        }
    }
    /**
     * Gets the functions options from the properties that have been set
     * @return a list of function options for the request
     */
    @Nonnull
    public java.util.List<com.microsoft.graph.options.FunctionOption> getFunctionOptions() {
        final ArrayList<com.microsoft.graph.options.FunctionOption> result = new ArrayList<>();
        if(this.toRecipients != null) {
            result.add(new com.microsoft.graph.options.FunctionOption("toRecipients", toRecipients));
        }
        if(this.singleRecipient != null) {
            result.add(new com.microsoft.graph.options.FunctionOption("singleRecipient", singleRecipient));
        }
        if(this.multipleSessions != null) {
            result.add(new com.microsoft.graph.options.FunctionOption("multipleSessions", multipleSessions));
        }
        if(this.singleSession != null) {
            result.add(new com.microsoft.graph.options.FunctionOption("singleSession", singleSession));
        }
        if(this.comment != null) {
            result.add(new com.microsoft.graph.options.FunctionOption("comment", comment));
        }
        return result;
    }
    /**
     * The raw representation of this class
     */
    private JsonObject rawObject;

    /**
     * The serializer
     */
    private ISerializer serializer;

    /**
     * Gets the raw representation of this class
     *
     * @return the raw representation of this class
     */
    @Nullable
    public JsonObject getRawObject() {
        return rawObject;
    }

    /**
     * Gets serializer
     *
     * @return the serializer
     */
    @Nullable
    public ISerializer getSerializer() {
        return serializer;
    }

    /**
     * Sets the raw JSON object
     *
     * @param serializer the serializer
     * @param json the JSON object to set this object to
     */
    public void setRawObject(@Nonnull final ISerializer serializer, @Nonnull final JsonObject json) {
        this.serializer = serializer;
        rawObject = json;

    }
}
