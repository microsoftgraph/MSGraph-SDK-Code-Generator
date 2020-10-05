// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.requests.extensions;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.concurrency.ICallback;
import com.microsoft.graph.models.extensions.Group;
import com.microsoft.graph.models.extensions.User;
import java.util.Arrays;
import java.util.EnumSet;

import com.microsoft.graph.requests.extensions.UserCollectionWithReferencesRequestBuilder;
import com.microsoft.graph.requests.extensions.UserCollectionWithReferencesPage;
import com.microsoft.graph.requests.extensions.UserCollectionResponse;
import com.microsoft.graph.models.extensions.User;
import com.google.gson.JsonObject;
import com.google.gson.annotations.SerializedName;
import com.google.gson.annotations.Expose;
import com.microsoft.graph.http.BaseCollectionPage;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the User Collection With References Page.
 */
public class UserCollectionWithReferencesPage extends BaseCollectionPage<User, UserCollectionWithReferencesRequestBuilder> {

    /**
     * A collection page for User
     *
     * @param response the serialized UserCollectionResponse from the service
     * @param builder  the request builder for the next collection page
     */
    public UserCollectionWithReferencesPage(final UserCollectionResponse response, final UserCollectionWithReferencesRequestBuilder builder) {
        super(response.value, builder, response.additionalDataManager());
    }
}
