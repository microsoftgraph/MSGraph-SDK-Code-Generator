// Template Source: BaseMethodRequest.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph2.callrecords.requests;
import com.microsoft.graph2.callrecords.models.CallRecord;
import com.microsoft.graph2.callrecords.requests.CallRecordItemRequest;

import javax.annotation.Nullable;
import javax.annotation.Nonnull;
import com.microsoft.graph.http.BaseRequest;
import com.microsoft.graph.http.HttpMethod;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.core.IBaseClient;
import com.microsoft.graph2.callrecords.models.CallRecordItemParameterSet;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Call Record Item Request.
 */
public class CallRecordItemRequest extends BaseRequest<CallRecord> {
    /**
     * The request for this CallRecordItem
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public CallRecordItemRequest(@Nonnull final String requestUrl, @Nonnull final IBaseClient client, @Nullable final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions, CallRecord.class);
    }

    /**
     * Gets the CallRecord
     *
     * @return a future with the result
     */
    @Nonnull
    public java.util.concurrent.CompletableFuture<CallRecord> getAsync() {
        return sendAsync(HttpMethod.GET, null);
    }

    /**
     * Gets the CallRecord
     *
     * @return the CallRecord
     * @throws ClientException an exception occurs if there was an error while the request was sent
     */
    @Nullable
    public CallRecord get() throws ClientException {
       return send(HttpMethod.GET, null);
    }

    /**
     * Sets the select clause for the request
     *
     * @param value the select clause
     * @return the updated request
     */
    @Nonnull
    public CallRecordItemRequest select(@Nonnull final String value) {
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
    public CallRecordItemRequest expand(@Nonnull final String value) {
        addExpandOption(value);
        return this;
    }

}