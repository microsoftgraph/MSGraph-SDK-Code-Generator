// Template Source: Templates\Java\requests_extensions\BaseEntityReferenceRequest.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.requests.extensions;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.concurrency.ICallback;
import com.microsoft.graph.models.extensions.EntityType3;
import com.microsoft.graph.models.extensions.Recipient;
import com.microsoft.graph2.callrecords.models.extensions.Session;
import java.util.Arrays;
import java.util.EnumSet;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;

import com.microsoft.graph.options.QueryOption;
import com.microsoft.graph.http.BaseReferenceRequest;
import com.microsoft.graph.http.HttpMethod;
import com.microsoft.graph.core.IBaseClient;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Entity Type3Reference Request.
 */
public class EntityType3ReferenceRequest extends BaseReferenceRequest<EntityType3> {

    /**
     * The request for the EntityType3
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public EntityType3ReferenceRequest(@Nonnull final String requestUrl, @Nonnull final IBaseClient client, @Nullable final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions, EntityType3.class);
    }

    /**
     * Sets the select clause for the request
     *
     * @param value the select clause
     * @return the updated request
     */
    @Nonnull
    public EntityType3ReferenceRequest select(@Nonnull final String value) {
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
    public EntityType3ReferenceRequest expand(@Nonnull final String value) {
        addExpandOption(value);
        return this;
    }
    /**
     * Puts the EntityType3
     *
     * @param srcEntityType3 the EntityType3 reference to PUT
     * @param callback the callback to be called after success or failure
     */
    public void put(@Nonnull final EntityType3 srcEntityType3, @Nonnull final ICallback<? super EntityType3> callback) {
        send(HttpMethod.PUT, callback, srcEntityType3);
    }

    /**
     * Puts the EntityType3
     *
     * @param srcEntityType3 the EntityType3 reference to PUT
     * @return the EntityType3
     * @throws ClientException an exception occurs if there was an error while the request was sent
     */
    @Nullable
    public EntityType3 put(@Nonnull final EntityType3 srcEntityType3) throws ClientException {
        return send(HttpMethod.PUT, srcEntityType3);
    }
}
