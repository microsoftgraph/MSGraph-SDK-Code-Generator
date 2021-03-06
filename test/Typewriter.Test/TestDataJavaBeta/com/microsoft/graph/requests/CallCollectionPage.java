// Template Source: BaseEntityCollectionPage.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.requests;
import com.microsoft.graph.models.Call;
import com.microsoft.graph.requests.CallCollectionRequestBuilder;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;
import com.microsoft.graph.requests.CallCollectionResponse;
import com.microsoft.graph.http.BaseCollectionPage;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Call Collection Page.
 */
public class CallCollectionPage extends BaseCollectionPage<Call, CallCollectionRequestBuilder> {

    /**
     * A collection page for Call
     *
     * @param response the serialized CallCollectionResponse from the service
     * @param builder  the request builder for the next collection page
     */
    public CallCollectionPage(@Nonnull final CallCollectionResponse response, @Nonnull final CallCollectionRequestBuilder builder) {
        super(response, builder);
    }

    /**
     * Creates the collection page for Call
     *
     * @param pageContents       the contents of this page
     * @param nextRequestBuilder the request builder for the next page
     */
    public CallCollectionPage(@Nonnull final java.util.List<Call> pageContents, @Nullable final CallCollectionRequestBuilder nextRequestBuilder) {
        super(pageContents, nextRequestBuilder);
    }
}
