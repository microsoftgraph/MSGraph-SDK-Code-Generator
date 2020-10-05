// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph2.callrecords.requests.extensions;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.concurrency.ICallback;
import com.microsoft.graph.models.extensions.IdentitySet;
import com.microsoft.graph2.callrecords.models.extensions.Session;
import java.util.Arrays;
import java.util.EnumSet;

import com.microsoft.graph2.callrecords.requests.extensions.SegmentTestActionCollectionRequestBuilder;
import com.microsoft.graph2.callrecords.requests.extensions.SegmentTestActionCollectionRequest;
import com.microsoft.graph.options.FunctionOption;
import com.microsoft.graph.core.IBaseClient;
import com.microsoft.graph.core.BaseActionRequestBuilder;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Segment Test Action Collection Request Builder.
 */
public class SegmentTestActionCollectionRequestBuilder extends BaseActionRequestBuilder {

    /**
     * The request builder for this collection of Segment
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     * @param value the value
     */
    public SegmentTestActionCollectionRequestBuilder(final String requestUrl, final IBaseClient client, final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions, final IdentitySet value) {
        super(requestUrl, client, requestOptions);
  	 if(value!=null){
			bodyParams.put("value", value);
		}
      }
    
    /**
     * Creates the request
     *
     * @param requestOptions the options for this request
     * @return the IUserRequest instance
     */
    public SegmentTestActionCollectionRequest buildRequest(final com.microsoft.graph.options.Option... requestOptions) {
        return buildRequest(getOptions(requestOptions));
    }

    /**
     * Creates the request
     *
     * @param requestOptions the options for this request
     * @return the IUserRequest instance
     */
    public SegmentTestActionCollectionRequest buildRequest(final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        SegmentTestActionCollectionRequest request = new SegmentTestActionCollectionRequest(
                getRequestUrl(),
                getClient(),
                requestOptions
        );

        if (hasParameter("value")) {
            request.body.value = getParameter("value");
        }
  
        return request;
    }
}
