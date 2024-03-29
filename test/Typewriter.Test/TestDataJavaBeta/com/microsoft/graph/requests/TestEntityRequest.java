// Template Source: BaseEntityRequest.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.requests;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.models.TestEntity;
import java.util.Arrays;
import java.util.EnumSet;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;
import com.microsoft.graph.core.IBaseClient;
import com.microsoft.graph.http.BaseRequest;
import com.microsoft.graph.http.HttpMethod;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Test Entity Request.
 */
public class TestEntityRequest extends BaseRequest<TestEntity> {
	
    /**
     * The request for the TestEntity
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public TestEntityRequest(@Nonnull final String requestUrl, @Nonnull final IBaseClient<?> client, @Nullable final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions, TestEntity.class);
    }

    /**
     * Gets the TestEntity from the service
     *
     * @return a future with the result
     */
    @Nonnull
    public java.util.concurrent.CompletableFuture<TestEntity> getAsync() {
        return sendAsync(HttpMethod.GET, null);
    }

    /**
     * Gets the TestEntity from the service
     *
     * @return the TestEntity from the request
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    @Nullable
    public TestEntity get() throws ClientException {
       return send(HttpMethod.GET, null);
    }

    /**
     * Delete this item from the service
     *
     * @return a future with the deletion result
     */
    @Nonnull
    public java.util.concurrent.CompletableFuture<TestEntity> deleteAsync() {
        return sendAsync(HttpMethod.DELETE, null);
    }

    /**
     * Delete this item from the service
     * @return the resulting response if the service returns anything on deletion
     *
     * @throws ClientException if there was an exception during the delete operation
     */
    @Nullable
    public TestEntity delete() throws ClientException {
        return send(HttpMethod.DELETE, null);
    }

    /**
     * Patches this TestEntity with a source
     *
     * @param sourceTestEntity the source object with updates
     * @return a future with the result
     */
    @Nonnull
    public java.util.concurrent.CompletableFuture<TestEntity> patchAsync(@Nonnull final TestEntity sourceTestEntity) {
        return sendAsync(HttpMethod.PATCH, sourceTestEntity);
    }

    /**
     * Patches this TestEntity with a source
     *
     * @param sourceTestEntity the source object with updates
     * @return the updated TestEntity
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    @Nullable
    public TestEntity patch(@Nonnull final TestEntity sourceTestEntity) throws ClientException {
        return send(HttpMethod.PATCH, sourceTestEntity);
    }

    /**
     * Creates a TestEntity with a new object
     *
     * @param newTestEntity the new object to create
     * @return a future with the result
     */
    @Nonnull
    public java.util.concurrent.CompletableFuture<TestEntity> postAsync(@Nonnull final TestEntity newTestEntity) {
        return sendAsync(HttpMethod.POST, newTestEntity);
    }

    /**
     * Creates a TestEntity with a new object
     *
     * @param newTestEntity the new object to create
     * @return the created TestEntity
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    @Nullable
    public TestEntity post(@Nonnull final TestEntity newTestEntity) throws ClientException {
        return send(HttpMethod.POST, newTestEntity);
    }

    /**
     * Creates a TestEntity with a new object
     *
     * @param newTestEntity the object to create/update
     * @return a future with the result
     */
    @Nonnull
    public java.util.concurrent.CompletableFuture<TestEntity> putAsync(@Nonnull final TestEntity newTestEntity) {
        return sendAsync(HttpMethod.PUT, newTestEntity);
    }

    /**
     * Creates a TestEntity with a new object
     *
     * @param newTestEntity the object to create/update
     * @return the created TestEntity
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    @Nullable
    public TestEntity put(@Nonnull final TestEntity newTestEntity) throws ClientException {
        return send(HttpMethod.PUT, newTestEntity);
    }

    /**
     * Sets the select clause for the request
     *
     * @param value the select clause
     * @return the updated request
     */
     @Nonnull
     public TestEntityRequest select(@Nonnull final String value) {
         addSelectOption(value);
         return this;
     }

    /**
     * Sets the expand clause for the request
     *
     * @param value the expand clause
     * @return the updated request
     */
     @Nonnull
     public TestEntityRequest expand(@Nonnull final String value) {
         addExpandOption(value);
         return this;
     }

}

