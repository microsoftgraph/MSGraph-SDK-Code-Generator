// Template Source: Templates\Java\requests_extensions\BaseEntityRequest.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph2.callrecords.requests.extensions;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.concurrency.ICallback;
import com.microsoft.graph2.callrecords.models.extensions.Segment;
import com.microsoft.graph.models.extensions.Recipient;
import com.microsoft.graph2.callrecords.models.extensions.Session;
import com.microsoft.graph.models.extensions.IdentitySet;
import com.microsoft.graph.requests.extensions.EntityType3CollectionRequestBuilder;
import com.microsoft.graph.requests.extensions.EntityType3RequestBuilder;
import com.microsoft.graph.requests.extensions.CallRequestBuilder;
import com.microsoft.graph2.callrecords.requests.extensions.SessionRequestBuilder;
import com.microsoft.graph2.callrecords.requests.extensions.PhotoRequestBuilder;
import java.util.Arrays;
import java.util.EnumSet;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;
import com.microsoft.graph.core.IBaseClient;
import com.microsoft.graph.http.BaseRequest;
import com.microsoft.graph.http.HttpMethod;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Segment Request.
 */
public class SegmentRequest extends BaseRequest<Segment> {
	
    /**
     * The request for the Segment
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public SegmentRequest(@Nonnull final String requestUrl, @Nonnull final IBaseClient client, @Nullable final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions, Segment.class);
    }

    /**
     * Gets the Segment from the service
     *
     * @param callback the callback to be called after success or failure
     */
    public void get(@Nonnull final ICallback<? super Segment> callback) {
        send(HttpMethod.GET, callback, null);
    }

    /**
     * Gets the Segment from the service
     *
     * @return the Segment from the request
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    @Nullable
    public Segment get() throws ClientException {
       return send(HttpMethod.GET, null);
    }

    /**
     * Delete this item from the service
     *
     * @param callback the callback when the deletion action has completed
     */
    public void delete(@Nonnull final ICallback<? super Segment> callback) {
        send(HttpMethod.DELETE, callback, null);
    }

    /**
     * Delete this item from the service
     *
     * @throws ClientException if there was an exception during the delete operation
     */
    public void delete() throws ClientException {
        send(HttpMethod.DELETE, null);
    }

    /**
     * Patches this Segment with a source
     *
     * @param sourceSegment the source object with updates
     * @param callback the callback to be called after success or failure
     */
    public void patch(@Nonnull final Segment sourceSegment, @Nonnull final ICallback<? super Segment> callback) {
        send(HttpMethod.PATCH, callback, sourceSegment);
    }

    /**
     * Patches this Segment with a source
     *
     * @param sourceSegment the source object with updates
     * @return the updated Segment
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    @Nullable
    public Segment patch(@Nonnull final Segment sourceSegment) throws ClientException {
        return send(HttpMethod.PATCH, sourceSegment);
    }

    /**
     * Creates a Segment with a new object
     *
     * @param newSegment the new object to create
     * @param callback the callback to be called after success or failure
     */
    public void post(@Nonnull final Segment newSegment, @Nonnull final ICallback<? super Segment> callback) {
        send(HttpMethod.POST, callback, newSegment);
    }

    /**
     * Creates a Segment with a new object
     *
     * @param newSegment the new object to create
     * @return the created Segment
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    @Nullable
    public Segment post(@Nonnull final Segment newSegment) throws ClientException {
        return send(HttpMethod.POST, newSegment);
    }

    /**
     * Creates a Segment with a new object
     *
     * @param newSegment the object to create/update
     * @param callback the callback to be called after success or failure
     */
    public void put(@Nonnull final Segment newSegment, @Nonnull final ICallback<? super Segment> callback) {
        send(HttpMethod.PUT, callback, newSegment);
    }

    /**
     * Creates a Segment with a new object
     *
     * @param newSegment the object to create/update
     * @return the created Segment
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    @Nullable
    public Segment put(@Nonnull final Segment newSegment) throws ClientException {
        return send(HttpMethod.PUT, newSegment);
    }

    /**
     * Sets the select clause for the request
     *
     * @param value the select clause
     * @return the updated request
     */
     @Nonnull
     public SegmentRequest select(@Nonnull final String value) {
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
     public SegmentRequest expand(@Nonnull final String value) {
         addExpandOption(value);
         return this;
     }

}

