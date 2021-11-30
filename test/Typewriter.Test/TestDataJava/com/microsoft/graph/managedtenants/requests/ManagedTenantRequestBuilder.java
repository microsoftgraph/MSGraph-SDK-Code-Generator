// Template Source: BaseEntityRequestBuilder.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.managedtenants.requests;

import com.microsoft.graph.http.IRequestBuilder;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.managedtenants.models.ManagedTenant;
import com.microsoft.graph.managedtenants.requests.ManagementTemplateCollectionObjectCollectionRequestBuilder;
import com.microsoft.graph.managedtenants.requests.ManagementTemplateCollectionObjectRequestBuilder;
import com.microsoft.graph.managedtenants.requests.ManagementTemplateCollectionRequestBuilder;
import com.microsoft.graph.managedtenants.requests.ManagementTemplateRequestBuilder;
import java.util.Arrays;
import java.util.EnumSet;
import javax.annotation.Nullable;
import javax.annotation.Nonnull;
import com.microsoft.graph.core.IBaseClient;
import com.microsoft.graph.http.BaseRequestBuilder;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The class for the Managed Tenant Request Builder.
 */
public class ManagedTenantRequestBuilder extends BaseRequestBuilder<ManagedTenant> {

    /**
     * The request builder for the ManagedTenant
     *
     * @param requestUrl     the request URL
     * @param client         the service client
     * @param requestOptions the options for this request
     */
    public ManagedTenantRequestBuilder(@Nonnull final String requestUrl, @Nonnull final IBaseClient<?> client, @Nullable final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        super(requestUrl, client, requestOptions);
    }

    /**
     * Creates the request
     *
     * @param requestOptions the options for this request
     * @return the ManagedTenantRequest instance
     */
    @Nonnull
    public ManagedTenantRequest buildRequest(@Nullable final com.microsoft.graph.options.Option... requestOptions) {
        return buildRequest(getOptions(requestOptions));
    }

    /**
     * Creates the request with specific requestOptions instead of the existing requestOptions
     *
     * @param requestOptions the options for this request
     * @return the ManagedTenantRequest instance
     */
    @Nonnull
    public ManagedTenantRequest buildRequest(@Nonnull final java.util.List<? extends com.microsoft.graph.options.Option> requestOptions) {
        return new com.microsoft.graph.managedtenants.requests.ManagedTenantRequest(getRequestUrl(), getClient(), requestOptions);
    }


    /**
     *  Gets a request builder for the ManagementTemplateCollectionObject collection
     *
     * @return the collection request builder
     */
    @Nonnull
    public ManagementTemplateCollectionObjectCollectionRequestBuilder managementTemplateCollections() {
        return new ManagementTemplateCollectionObjectCollectionRequestBuilder(getRequestUrlWithAdditionalSegment("managementTemplateCollections"), getClient(), null);
    }

    /**
     * Gets a request builder for the ManagementTemplateCollectionObject item
     *
     * @return the request builder
     * @param id the item identifier
     */
    @Nonnull
    public ManagementTemplateCollectionObjectRequestBuilder managementTemplateCollections(@Nonnull final String id) {
        return new ManagementTemplateCollectionObjectRequestBuilder(getRequestUrlWithAdditionalSegment("managementTemplateCollections") + "/" + id, getClient(), null);
    }
    /**
     *  Gets a request builder for the ManagementTemplate collection
     *
     * @return the collection request builder
     */
    @Nonnull
    public ManagementTemplateCollectionRequestBuilder managementTemplates() {
        return new ManagementTemplateCollectionRequestBuilder(getRequestUrlWithAdditionalSegment("managementTemplates"), getClient(), null);
    }

    /**
     * Gets a request builder for the ManagementTemplate item
     *
     * @return the request builder
     * @param id the item identifier
     */
    @Nonnull
    public ManagementTemplateRequestBuilder managementTemplates(@Nonnull final String id) {
        return new ManagementTemplateRequestBuilder(getRequestUrlWithAdditionalSegment("managementTemplates") + "/" + id, getClient(), null);
    }
}