// Template Source: BaseEntityRequestBuilder.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.requests;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.models.SingletonEntity1;
import java.util.Arrays;
import java.util.EnumSet;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;
import com.microsoft.graph.core.IBaseClient;
import com.microsoft.graph.http.BaseRequestBuilder;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Singleton Entity1Request Builder.
 */
public class SingletonEntity1RequestBuilder extends BaseRequestBuilder<SingletonEntity1> {

    /**
     * The request builder for the SingletonEntity1
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public SingletonEntity1RequestBuilder(@Nonnull final String requestUrl, @Nonnull final IBaseClient<?> client, @Nullable final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions);
    }

    /**
     * Creates the request
     *
     * @param requestOptions the options for this request
     * @return the SingletonEntity1Request instance
     */
    @Nonnull
    public SingletonEntity1Request buildRequest(@Nullable final com.microsoft.graph.options.Option... requestOptions) {
        return buildRequest(getOptions(requestOptions));
    }

    /**
     * Creates the request with specific requestOptions instead of the existing requestOptions
     *
     * @param requestOptions the options for this request
     * @return the SingletonEntity1Request instance
     */
    @Nonnull
    public SingletonEntity1Request buildRequest(@Nonnull final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        return new com.microsoft.graph.requests.SingletonEntity1Request(getRequestUrl(), getClient(), requestOptions);
    }



    /**
     * Gets the request builder for TestType
     *
     * @return the TestTypeRequestBuilder instance
     */
    @Nonnull
    public com.microsoft.graph.requests.TestTypeRequestBuilder testSingleNav() {
        return new com.microsoft.graph.requests.TestTypeRequestBuilder(getRequestUrlWithAdditionalSegment("testSingleNav"), getClient(), null);
    }
}
