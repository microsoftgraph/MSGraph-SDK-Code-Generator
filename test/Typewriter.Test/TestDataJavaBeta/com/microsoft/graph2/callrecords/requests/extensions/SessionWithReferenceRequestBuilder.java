// Template Source: BaseEntityWithReferenceRequestBuilder.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph2.callrecords.requests.extensions;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.concurrency.ICallback;
import com.microsoft.graph2.callrecords.models.extensions.Session;
import com.microsoft.graph2.callrecords.requests.extensions.ISegmentCollectionRequestBuilder;
import com.microsoft.graph2.callrecords.requests.extensions.ISegmentRequestBuilder;
import com.microsoft.graph2.callrecords.requests.extensions.SegmentCollectionRequestBuilder;
import com.microsoft.graph2.callrecords.requests.extensions.SegmentRequestBuilder;
import java.util.Arrays;
import java.util.EnumSet;

import com.microsoft.graph.http.BaseRequestBuilder;
import com.microsoft.graph.core.IBaseClient;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Session With Reference Request Builder.
 */
public class SessionWithReferenceRequestBuilder extends BaseRequestBuilder implements ISessionWithReferenceRequestBuilder {

    /**
     * The request builder for the Session
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public SessionWithReferenceRequestBuilder(final String requestUrl, final IBaseClient client, final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions);
    }

    /**
     * Creates the request
     *
     * @param requestOptions the options for this request
     * @return the ISessionWithReferenceRequest instance
     */
    public ISessionWithReferenceRequest buildRequest(final com.microsoft.graph.options.Option... requestOptions) {
        return buildRequest(getOptions(requestOptions));
    }

    /**
     * Creates the request with specific options instead of the existing options
	 *
     * @param requestOptions the options for this request
     * @return the ISessionWithReferenceRequest instance
     */
    public ISessionWithReferenceRequest buildRequest(final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        return new SessionWithReferenceRequest(getRequestUrl(), getClient(), requestOptions);
    }

    public ISessionReferenceRequestBuilder reference(){
        return new SessionReferenceRequestBuilder(getRequestUrl() + "/$ref", getClient(), getOptions());
    }

}