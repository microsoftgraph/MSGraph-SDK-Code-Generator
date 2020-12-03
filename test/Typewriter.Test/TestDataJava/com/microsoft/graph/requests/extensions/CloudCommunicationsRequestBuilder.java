// Template Source: Templates\Java\requests_extensions\BaseEntityRequestBuilder.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.requests.extensions;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.concurrency.ICallback;
import com.microsoft.graph.models.extensions.CloudCommunications;
import com.microsoft.graph.requests.extensions.CallCollectionRequestBuilder;
import com.microsoft.graph.requests.extensions.CallRequestBuilder;
import com.microsoft.graph2.callrecords.requests.extensions.CallRecordCollectionRequestBuilder;
import com.microsoft.graph2.callrecords.requests.extensions.CallRecordRequestBuilder;
import java.util.Arrays;
import java.util.EnumSet;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;
import com.microsoft.graph.core.IBaseClient;
import com.microsoft.graph.http.BaseRequestBuilder;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Cloud Communications Request Builder.
 */
public class CloudCommunicationsRequestBuilder extends BaseRequestBuilder<CloudCommunications> {

    /**
     * The request builder for the CloudCommunications
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public CloudCommunicationsRequestBuilder(@Nonnull final String requestUrl, @Nonnull final IBaseClient client, @Nullable final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions);
    }

    /**
     * Creates the request
     *
     * @param requestOptions the options for this request
     * @return the CloudCommunicationsRequest instance
     */
    @Nonnull
    public CloudCommunicationsRequest buildRequest(@Nullable final com.microsoft.graph.options.Option... requestOptions) {
        return buildRequest(getOptions(requestOptions));
    }

    /**
     * Creates the request with specific requestOptions instead of the existing requestOptions
     *
     * @param requestOptions the options for this request
     * @return the CloudCommunicationsRequest instance
     */
    @Nonnull
    public CloudCommunicationsRequest buildRequest(@Nonnull final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        return new com.microsoft.graph.requests.extensions.CloudCommunicationsRequest(getRequestUrl(), getClient(), requestOptions);
    }


    /**
     *  Gets a request builder for the Call collection
     *
     * @return the collection request builder
     */
    @Nonnull
    public CallCollectionRequestBuilder calls() {
        return new CallCollectionRequestBuilder(getRequestUrlWithAdditionalSegment("calls"), getClient(), null);
    }

    /**
     * Gets a request builder for the Call item
     *
     * @return the request builder
     * @param id the item identifier
     */
    @Nonnull
    public CallRequestBuilder calls(@Nonnull final String id) {
        return new CallRequestBuilder(getRequestUrlWithAdditionalSegment("calls") + "/" + id, getClient(), null);
    }
    /**
     *  Gets a request builder for the CallRecord collection
     *
     * @return the collection request builder
     */
    @Nonnull
    public CallRecordCollectionRequestBuilder callRecords() {
        return new CallRecordCollectionRequestBuilder(getRequestUrlWithAdditionalSegment("callRecords"), getClient(), null);
    }

    /**
     * Gets a request builder for the CallRecord item
     *
     * @return the request builder
     * @param id the item identifier
     */
    @Nonnull
    public CallRecordRequestBuilder callRecords(@Nonnull final String id) {
        return new CallRecordRequestBuilder(getRequestUrlWithAdditionalSegment("callRecords") + "/" + id, getClient(), null);
    }
}
