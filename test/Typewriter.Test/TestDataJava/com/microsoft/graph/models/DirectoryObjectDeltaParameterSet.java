// Template Source: BaseMethodParameterSet.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.models;

import com.microsoft.graph.models.DirectoryObject;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import javax.annotation.Nonnull;
import javax.annotation.Nullable;
import com.google.gson.JsonObject;
import java.util.EnumSet;
import java.util.ArrayList;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Directory Object Delta Parameter Set.
 */
public class DirectoryObjectDeltaParameterSet {
    /**
     * The token.
     * 
     */
    @SerializedName(value = "token", alternate = {"Token"})
    @Expose
	@Nullable
    public String token;

    /**
     * The second Token.
     * 
     */
    @SerializedName(value = "secondToken", alternate = {"SecondToken"})
    @Expose
	@Nullable
    public String secondToken;


    /**
     * Instiaciates a new DirectoryObjectDeltaParameterSet
     */
    public DirectoryObjectDeltaParameterSet() {}
    /**
     * Instiaciates a new DirectoryObjectDeltaParameterSet
     * @param builder builder bearing the parameters to initialize from
     */
    protected DirectoryObjectDeltaParameterSet(@Nonnull final DirectoryObjectDeltaParameterSetBuilder builder) {
        this.token = builder.token;
        this.secondToken = builder.secondToken;
    }
    /**
     * Gets a new builder for the body
     * @return a new builder
     */
    @Nonnull
    public static DirectoryObjectDeltaParameterSetBuilder newBuilder() {
        return new DirectoryObjectDeltaParameterSetBuilder();
    }
    /**
     * Fluent builder for the DirectoryObjectDeltaParameterSet
     */
    public static final class DirectoryObjectDeltaParameterSetBuilder {
        /**
         * The token parameter value
         */
        @Nullable
        protected String token;
        /**
         * Sets the Token
         * @param val the value to set it to
         * @return the current builder object
         */
        @Nonnull
        public DirectoryObjectDeltaParameterSetBuilder withToken(@Nullable final String val) {
            this.token = val;
            return this;
        }
        /**
         * The secondToken parameter value
         */
        @Nullable
        protected String secondToken;
        /**
         * Sets the SecondToken
         * @param val the value to set it to
         * @return the current builder object
         */
        @Nonnull
        public DirectoryObjectDeltaParameterSetBuilder withSecondToken(@Nullable final String val) {
            this.secondToken = val;
            return this;
        }
        /**
         * Instanciates a new DirectoryObjectDeltaParameterSetBuilder
         */
        @Nullable
        protected DirectoryObjectDeltaParameterSetBuilder(){}
        /**
         * Buils the resulting body object to be passed to the request
         * @return the body object to pass to the request
         */
        @Nonnull
        public DirectoryObjectDeltaParameterSet build() {
            return new DirectoryObjectDeltaParameterSet(this);
        }
    }
    /**
     * Gets the functions options from the properties that have been set
     * @return a list of function options for the request
     */
    @Nonnull
    public java.util.List<com.microsoft.graph.options.FunctionOption> getFunctionOptions() {
        final ArrayList<com.microsoft.graph.options.FunctionOption> result = new ArrayList<>();
        if(this.token != null) {
            result.add(new com.microsoft.graph.options.FunctionOption("token", token));
        }
        if(this.secondToken != null) {
            result.add(new com.microsoft.graph.options.FunctionOption("secondToken", secondToken));
        }
        return result;
    }
}
