// Template Source: BaseEntityRequest.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.requests.extensions;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.concurrency.ICallback;
import com.microsoft.graph.models.extensions.Schedule;
import com.microsoft.graph.requests.extensions.ITimeOffCollectionRequestBuilder;
import com.microsoft.graph.requests.extensions.ITimeOffRequestBuilder;
import com.microsoft.graph.requests.extensions.TimeOffCollectionRequestBuilder;
import com.microsoft.graph.requests.extensions.TimeOffRequestBuilder;
import com.microsoft.graph.requests.extensions.ITimeOffRequestCollectionRequestBuilder;
import com.microsoft.graph.requests.extensions.ITimeOffRequestRequestBuilder;
import com.microsoft.graph.requests.extensions.TimeOffRequestCollectionRequestBuilder;
import com.microsoft.graph.requests.extensions.TimeOffRequestRequestBuilder;
import java.util.Arrays;
import java.util.EnumSet;
import com.microsoft.graph.core.IBaseClient;
import com.microsoft.graph.http.BaseRequest;
import com.microsoft.graph.http.HttpMethod;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Schedule Request.
 */
public class ScheduleRequest extends BaseRequest implements IScheduleRequest {
	
    /**
     * The request for the Schedule
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public ScheduleRequest(final String requestUrl, final IBaseClient client, final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions, Schedule.class);
    }

    /**
     * Gets the Schedule from the service
     *
     * @param callback the callback to be called after success or failure
     */
    public void get(final ICallback<? super Schedule> callback) {
        send(HttpMethod.GET, callback, null);
    }

    /**
     * Gets the Schedule from the service
     *
     * @return the Schedule from the request
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    public Schedule get() throws ClientException {
       return send(HttpMethod.GET, null);
    }

    /**
     * Delete this item from the service
     *
     * @param callback the callback when the deletion action has completed
     */
    public void delete(final ICallback<? super Schedule> callback) {
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
     * Patches this Schedule with a source
     *
     * @param sourceSchedule the source object with updates
     * @param callback the callback to be called after success or failure
     */
    public void patch(final Schedule sourceSchedule, final ICallback<? super Schedule> callback) {
        send(HttpMethod.PATCH, callback, sourceSchedule);
    }

    /**
     * Patches this Schedule with a source
     *
     * @param sourceSchedule the source object with updates
     * @return the updated Schedule
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    public Schedule patch(final Schedule sourceSchedule) throws ClientException {
        return send(HttpMethod.PATCH, sourceSchedule);
    }

    /**
     * Creates a Schedule with a new object
     *
     * @param newSchedule the new object to create
     * @param callback the callback to be called after success or failure
     */
    public void post(final Schedule newSchedule, final ICallback<? super Schedule> callback) {
        send(HttpMethod.POST, callback, newSchedule);
    }

    /**
     * Creates a Schedule with a new object
     *
     * @param newSchedule the new object to create
     * @return the created Schedule
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    public Schedule post(final Schedule newSchedule) throws ClientException {
        return send(HttpMethod.POST, newSchedule);
    }

    /**
     * Creates a Schedule with a new object
     *
     * @param newSchedule the object to create/update
     * @param callback the callback to be called after success or failure
     */
    public void put(final Schedule newSchedule, final ICallback<? super Schedule> callback) {
        send(HttpMethod.PUT, callback, newSchedule);
    }

    /**
     * Creates a Schedule with a new object
     *
     * @param newSchedule the object to create/update
     * @return the created Schedule
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    public Schedule put(final Schedule newSchedule) throws ClientException {
        return send(HttpMethod.PUT, newSchedule);
    }

    /**
     * Sets the select clause for the request
     *
     * @param value the select clause
     * @return the updated request
     */
     public IScheduleRequest select(final String value) {
         getQueryOptions().add(new com.microsoft.graph.options.QueryOption("$select", value));
         return (ScheduleRequest)this;
     }

    /**
     * Sets the expand clause for the request
     *
     * @param value the expand clause
     * @return the updated request
     */
     public IScheduleRequest expand(final String value) {
         getQueryOptions().add(new com.microsoft.graph.options.QueryOption("$expand", value));
         return (ScheduleRequest)this;
     }

}

