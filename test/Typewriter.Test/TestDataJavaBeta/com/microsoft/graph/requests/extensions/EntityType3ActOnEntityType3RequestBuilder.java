// Template Source: BaseMethodRequestBuilder.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.requests.extensions;
import com.microsoft.graph.requests.extensions.IEntityType3ActOnEntityType3Request;
import com.microsoft.graph.requests.extensions.EntityType3ActOnEntityType3Request;
import com.microsoft.graph.models.extensions.Endpoint;
import com.microsoft.graph.core.BaseActionRequestBuilder;
import com.microsoft.graph.core.BaseFunctionRequestBuilder;
import com.microsoft.graph.core.IBaseClient;
import com.google.gson.JsonElement;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Entity Type3Act On Entity Type3Request Builder.
 */
public class EntityType3ActOnEntityType3RequestBuilder extends BaseFunctionRequestBuilder implements IEntityType3ActOnEntityType3RequestBuilder {

    /**
     * The request builder for this EntityType3ActOnEntityType3
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     * @param name the name
     */
    public EntityType3ActOnEntityType3RequestBuilder(final String requestUrl, final IBaseClient client, final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions, final java.util.List<String> name) {
        super(requestUrl, client, requestOptions);
        functionOptions.add(new com.microsoft.graph.options.FunctionOption("name", name));
    }

    /**
     * Creates the IEntityType3ActOnEntityType3Request
     *
     * @param requestOptions the options for the request
     * @return the IEntityType3ActOnEntityType3Request instance
     */
    public IEntityType3ActOnEntityType3Request buildRequest(final com.microsoft.graph.options.Option... requestOptions) {
        return buildRequest(getOptions(requestOptions));
    }

    /**
     * Creates the IEntityType3ActOnEntityType3Request with specific requestOptions instead of the existing requestOptions
     *
     * @param requestOptions the options for the request
     * @return the IEntityType3ActOnEntityType3Request instance
     */
    public IEntityType3ActOnEntityType3Request buildRequest(final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        EntityType3ActOnEntityType3Request request = new EntityType3ActOnEntityType3Request(
                getRequestUrl(),
                getClient(),
                requestOptions
        );

      for (com.microsoft.graph.options.FunctionOption option : functionOptions) {
            request.addFunctionOption(option);
      }

        return request;
    }
}
