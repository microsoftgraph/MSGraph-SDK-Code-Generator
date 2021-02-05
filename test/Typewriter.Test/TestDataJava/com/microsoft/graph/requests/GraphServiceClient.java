// Template Source: BaseClient.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.requests;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.requests.EntityType3CollectionRequestBuilder;
import com.microsoft.graph.requests.EntityType3RequestBuilder;
import com.microsoft.graph.requests.SingletonEntity1RequestBuilder;
import com.microsoft.graph.requests.SingletonEntity2RequestBuilder;
import com.microsoft.graph2.callrecords.requests.SingletonEntity1RequestBuilder;
import java.util.Arrays;
import java.util.EnumSet;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;
import com.microsoft.graph.core.IBaseClient;
import com.microsoft.graph.core.BaseClient;
import com.microsoft.graph.http.IHttpProvider;
import com.microsoft.graph.authentication.IAuthenticationProvider;
import com.microsoft.graph.logger.ILogger;
import com.microsoft.graph.serializer.ISerializer;
import okhttp3.OkHttpClient;
import okhttp3.Request;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Graph Service Client.
 * @param <NativeRequestType> type of a request for the native http client
 */
public class GraphServiceClient<NativeRequestType> extends BaseClient<NativeRequestType> implements IBaseClient<NativeRequestType> {
    /**
     * Restricted constructor
     */
    protected GraphServiceClient() {
        setServiceRoot("https://graph.microsoft.com/v1.0");
    }
    /**
     * Gets the service SDK version if the service SDK is in use, null otherwise
     * @return the service SDK version if the service SDK is in use, null otherwise
     */
    @Override
    @Nullable
    public String getServiceSDKVersion() {
        return com.microsoft.graph.info.Constants.VERSION_NAME;
    }
    /**
     * Gets the builder to start configuring the client
     *
     * @return builder to start configuring the client
     */
    @Nonnull
    public static Builder<OkHttpClient, Request> builder() {
        return builder(OkHttpClient.class, Request.class);
    }

    /**
     * Gets the builder to start configuring the client
     *
     * @param <nativeClient> the type of the native http client
     * @param <nativeRequest> the type of the native http request
     * @param nativeClientClass the class of the native http client
     * @param nativeRequestClass the class of the native http request
     * @return builder to start configuring the client
     */
    @Nonnull
    public static <nativeClient, nativeRequest> Builder<nativeClient, nativeRequest> builder(Class<nativeClient> nativeClientClass, Class<nativeRequest> nativeRequestClass) {
        return new Builder<>();
    }
    /**
     * Builder to help configure the Graph service client
     * @param <NativeRequestType> type of a request for the native http client
     */
    public static class Builder<httpClientType, NativeRequestType> extends BaseClient.Builder<httpClientType, NativeRequestType> {
        /**
         * Sets the serializer.
         *
         * @param serializer
         *            the serializer
         * @return the instance of this builder
         */
        @Nonnull
        @Override
        public Builder<httpClientType, NativeRequestType> serializer(@Nonnull final ISerializer serializer) {
            super.serializer(serializer);
            return this;
        }

        /**
         * Sets the httpProvider
         *
         * @param httpProvider
         *            the httpProvider
         * @return the instance of this builder
         */
        @Nonnull
        @Override
        public Builder<httpClientType, NativeRequestType> httpProvider(@Nonnull final IHttpProvider httpProvider) {
            super.httpProvider(httpProvider);
            return this;
        }

        /**
         * Sets the logger
         *
         * @param logger
         *            the logger
         * @return the instance of this builder
         */
        @Nonnull
        @Override
        public Builder<httpClientType, NativeRequestType> logger(@Nonnull final ILogger logger) {
            super.logger(logger);
            return this;
        }

        /**
         * Sets the http client
         *
         * @param client the http client
         *
         * @return the instance of this builder
         */
        @Nonnull
        @Override
        public Builder<httpClientType, NativeRequestType> httpClient(@Nonnull final httpClientType client) {
            super.httpClient(client);
            return this;
        }

        /**
         * Sets the authentication provider
         *
         * @param auth the authentication provider
         * @return the instance of this builder
         */
        @Nonnull
        @Override
        public Builder<httpClientType, NativeRequestType> authenticationProvider(@Nonnull final IAuthenticationProvider auth) {
            super.authenticationProvider(auth);
            return this;
        }

        /**
         * Builds and returns the Graph service client.
         *
         * @return the Graph service client object
         * @throws ClientException
         *             if there was an exception creating the client
         */
        @Nonnull
        @Override
        public GraphServiceClient buildClient() throws ClientException {
            return buildClient(new GraphServiceClient());
        }
    }

    /**
     * Gets the collection of TestTypes objects
     *
     * @return the request builder for the collection of TestTypes objects
     * @deprecated entityType3 is deprecated. Please use singletonEntity1.
     */
    @Deprecated
    @Nonnull
    public EntityType3CollectionRequestBuilder testTypes() {
        return new EntityType3CollectionRequestBuilder(getServiceRoot() + "/testTypes", this, null);
    }

    /**
     * Gets a single TestTypes
     *
     * @param id the id of the TestTypes to retrieve
     * @return the request builder for the TestTypes object
     * @deprecated entityType3 is deprecated. Please use singletonEntity1.
     */
    @Deprecated
    @Nonnull
    public EntityType3RequestBuilder testTypes(@Nonnull final String id) {
        return new EntityType3RequestBuilder(getServiceRoot() + "/testTypes/" + id, this, null);
    }

    /**
     * Gets the GraphServiceRequestBuilder
     *
     * @return the SingletonEntity1
     */
    @Nonnull
    public SingletonEntity1RequestBuilder singletonProperty1() {
        return new SingletonEntity1RequestBuilder(getServiceRoot() + "/singletonProperty1", this, null);
    }

    /**
     * Gets the GraphServiceRequestBuilder
     *
     * @return the SingletonEntity2
     */
    @Nonnull
    public SingletonEntity2RequestBuilder singletonProperty2() {
        return new SingletonEntity2RequestBuilder(getServiceRoot() + "/singletonProperty2", this, null);
    }

    /**
     * Gets the GraphServiceRequestBuilder
     *
     * @return the SingletonEntity1
     */
    @Nonnull
    public SingletonEntity1RequestBuilder singletonProperty3() {
        return new SingletonEntity1RequestBuilder(getServiceRoot() + "/singletonProperty3", this, null);
    }
}
