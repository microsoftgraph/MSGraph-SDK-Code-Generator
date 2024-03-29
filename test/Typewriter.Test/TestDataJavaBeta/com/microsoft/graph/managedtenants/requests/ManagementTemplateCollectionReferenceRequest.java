// Template Source: BaseEntityCollectionReferenceRequest.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.managedtenants.requests;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.managedtenants.models.ManagementTemplateCollectionObject;
import com.microsoft.graph.managedtenants.models.ManagementTemplate;
import java.util.Arrays;
import java.util.EnumSet;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;

import com.microsoft.graph.managedtenants.requests.ManagementTemplateWithReferenceRequest;
import com.microsoft.graph.managedtenants.requests.ManagementTemplateReferenceRequestBuilder;
import com.microsoft.graph.managedtenants.requests.ManagementTemplateWithReferenceRequestBuilder;
import com.microsoft.graph.managedtenants.requests.ManagementTemplateCollectionWithReferencesRequest;
import com.microsoft.graph.managedtenants.requests.ManagementTemplateCollectionWithReferencesRequestBuilder;
import com.microsoft.graph.managedtenants.models.ManagementTemplate;
import com.microsoft.graph.options.QueryOption;
import com.microsoft.graph.core.IBaseClient;
import com.microsoft.graph.http.BaseCollectionWithReferencesRequest;
import com.microsoft.graph.http.BaseCollectionWithReferencesRequestBuilder;
import com.microsoft.graph.http.ReferenceRequestBody;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Management Template Collection Reference Request.
 */
public class ManagementTemplateCollectionReferenceRequest extends BaseCollectionWithReferencesRequest<ManagementTemplate, ManagementTemplateWithReferenceRequest, ManagementTemplateReferenceRequestBuilder, ManagementTemplateWithReferenceRequestBuilder, ManagementTemplateCollectionResponse, ManagementTemplateCollectionWithReferencesPage, ManagementTemplateCollectionWithReferencesRequest> {

    /**
     * The request builder for this collection of ManagementTemplate
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public ManagementTemplateCollectionReferenceRequest(@Nonnull final String requestUrl, @Nonnull final IBaseClient<?> client, @Nullable final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions, ManagementTemplateCollectionResponse.class, ManagementTemplateCollectionWithReferencesPage.class, ManagementTemplateCollectionWithReferencesRequestBuilder.class);
    }

    /**
     * Sets the expand clause for the request
     *
     * @param value the expand clause
     * @return the updated request
     */
    @Nonnull
    public ManagementTemplateCollectionReferenceRequest expand(@Nonnull final String value) {
        addExpandOption(value);
        return this;
    }

    /**
     * Sets the filter clause for the request
     *
     * @param value the filter clause
     * @return the updated request
     */
    @Nonnull
    public ManagementTemplateCollectionReferenceRequest filter(@Nonnull final String value) {
        addFilterOption(value);
        return this;
    }

    /**
     * Sets the order by clause for the request
     *
     * @param value the sort clause
     * @return the updated request
     */
    @Nonnull
    public ManagementTemplateCollectionReferenceRequest orderBy(@Nonnull final String value) {
        addOrderByOption(value);
        return this;
    }

    /**
     * Sets the select clause for the request
     *
     * @param value the select clause
     * @return the updated request
     */
    @Nonnull
    public ManagementTemplateCollectionReferenceRequest select(@Nonnull final String value) {
        addSelectOption(value);
        return this;
    }

    /**
     * Sets the top value for the request
     *
     * @param value the max number of items to return
     * @return the updated request
     */
    @Nonnull
    public ManagementTemplateCollectionReferenceRequest top(final int value) {
        addTopOption(value);
        return this;
    }
    /**
     * Sets the count value for the request
     *
     * @param value whether or not to return the count of objects with the request
     * @return the updated request
     */
    @Nonnull
    public ManagementTemplateCollectionReferenceRequest count(final boolean value) {
        addCountOption(value);
        return this;
    }
    /**
     * Sets the count value to true for the request
     *
     * @return the updated request
     */
    @Nonnull
    public ManagementTemplateCollectionReferenceRequest count() {
        addCountOption(true);
        return this;
    }
}
