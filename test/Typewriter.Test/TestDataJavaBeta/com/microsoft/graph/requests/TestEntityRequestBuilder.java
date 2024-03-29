// Template Source: BaseEntityRequestBuilder.java.tt
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
import com.microsoft.graph.http.BaseRequestBuilder;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Test Entity Request Builder.
 */
public class TestEntityRequestBuilder extends BaseRequestBuilder<TestEntity> {

    /**
     * The request builder for the TestEntity
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public TestEntityRequestBuilder(@Nonnull final String requestUrl, @Nonnull final IBaseClient<?> client, @Nullable final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions);
    }

    /**
     * Creates the request
     *
     * @param requestOptions the options for this request
     * @return the TestEntityRequest instance
     */
    @Nonnull
    public TestEntityRequest buildRequest(@Nullable final com.microsoft.graph.options.Option... requestOptions) {
        return buildRequest(getOptions(requestOptions));
    }

    /**
     * Creates the request with specific requestOptions instead of the existing requestOptions
     *
     * @param requestOptions the options for this request
     * @return the TestEntityRequest instance
     */
    @Nonnull
    public TestEntityRequest buildRequest(@Nonnull final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        return new com.microsoft.graph.requests.TestEntityRequest(getRequestUrl(), getClient(), requestOptions);
    }



    /**
     * Gets the request builder for TestType
     *
     * @return the TestTypeWithReferenceRequestBuilder instance
     */
    @Nonnull
    public com.microsoft.graph.requests.TestTypeWithReferenceRequestBuilder testNav() {
        return new com.microsoft.graph.requests.TestTypeWithReferenceRequestBuilder(getRequestUrlWithAdditionalSegment("testNav"), getClient(), null);
    }

    /**
     * Gets the request builder for EntityType2
     *
     * @return the EntityType2WithReferenceRequestBuilder instance
     */
    @Nonnull
    public com.microsoft.graph.requests.EntityType2WithReferenceRequestBuilder testInvalidNav() {
        return new com.microsoft.graph.requests.EntityType2WithReferenceRequestBuilder(getRequestUrlWithAdditionalSegment("testInvalidNav"), getClient(), null);
    }

    /**
     * Gets the request builder for EntityType3
     *
     * @return the EntityType3WithReferenceRequestBuilder instance
     */
    @Nonnull
    public com.microsoft.graph.requests.EntityType3WithReferenceRequestBuilder testExplicitNav() {
        return new com.microsoft.graph.requests.EntityType3WithReferenceRequestBuilder(getRequestUrlWithAdditionalSegment("testExplicitNav"), getClient(), null);
    }
}
