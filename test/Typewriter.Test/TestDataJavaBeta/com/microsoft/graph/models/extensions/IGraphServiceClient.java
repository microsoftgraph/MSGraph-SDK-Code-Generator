// Template Source: IClient.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.models.extensions;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.concurrency.ICallback;
import com.microsoft.graph.requests.extensions.IEntityType3CollectionRequestBuilder;
import com.microsoft.graph.requests.extensions.IEntityType3RequestBuilder;
import com.microsoft.graph.requests.extensions.ISingletonEntity1RequestBuilder;
import com.microsoft.graph.requests.extensions.ISingletonEntity2RequestBuilder;
import com.microsoft.graph2.callrecords.requests.extensions.ISingletonEntity1RequestBuilder;
import java.util.Arrays;
import java.util.EnumSet;
import com.google.gson.JsonObject;
import com.microsoft.graph.models.extensions.IBaseGraphServiceClient;
import com.microsoft.graph.requests.extensions.CustomRequestBuilder;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The interface for the Graph Service Client.
 */
public interface IGraphServiceClient extends IBaseGraphServiceClient {
    
    <T> CustomRequestBuilder<T> customRequest(final String url, final Class<T> responseType);
	
    CustomRequestBuilder<JsonObject> customRequest(final String url);
}