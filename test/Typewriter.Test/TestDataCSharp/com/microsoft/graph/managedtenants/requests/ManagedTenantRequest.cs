// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityRequest.cs.tt

namespace Microsoft.Graph.ManagedTenants
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Linq.Expressions;

    /// <summary>
    /// The type ManagedTenantRequest.
    /// </summary>
    public partial class ManagedTenantRequest : Microsoft.Graph.BaseRequest, IManagedTenantRequest
    {
        /// <summary>
        /// Constructs a new ManagedTenantRequest.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="Microsoft.Graph.IBaseClient"/> for handling requests.</param>
        /// <param name="options">Query and header option name value pairs for the request.</param>
        public ManagedTenantRequest(
            string requestUrl,
            Microsoft.Graph.IBaseClient client,
            IEnumerable<Microsoft.Graph.Option> options)
            : base(requestUrl, client, options)
        {
        }

        /// <summary>
        /// Creates the specified ManagedTenant using POST.
        /// </summary>
        /// <param name="managedTenantToCreate">The ManagedTenant to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created ManagedTenant.</returns>
        public async System.Threading.Tasks.Task<ManagedTenant> CreateAsync(ManagedTenant managedTenantToCreate, CancellationToken cancellationToken = default)
        {
            this.ContentType = CoreConstants.MimeTypeNames.Application.Json;
            this.Method = HttpMethods.POST;
            var newEntity = await this.SendAsync<ManagedTenant>(managedTenantToCreate, cancellationToken).ConfigureAwait(false);
            this.InitializeCollectionProperties(newEntity);
            return newEntity;
        }

        /// <summary>
        /// Creates the specified ManagedTenant using POST and returns a <see cref="GraphResponse{ManagedTenant}"/> object.
        /// </summary>
        /// <param name="managedTenantToCreate">The ManagedTenant to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="GraphResponse{ManagedTenant}"/> object of the request.</returns>
        public System.Threading.Tasks.Task<GraphResponse<ManagedTenant>> CreateResponseAsync(ManagedTenant managedTenantToCreate, CancellationToken cancellationToken = default)
        {
            this.ContentType = CoreConstants.MimeTypeNames.Application.Json;
            this.Method = HttpMethods.POST;
            return this.SendAsyncWithGraphResponse<ManagedTenant>(managedTenantToCreate, cancellationToken);
        }

        /// <summary>
        /// Deletes the specified ManagedTenant.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        public async System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken = default)
        {
            this.Method = HttpMethods.DELETE;
            await this.SendAsync<ManagedTenant>(null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the specified ManagedTenant and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        public System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync(CancellationToken cancellationToken = default)
        {
            this.Method = HttpMethods.DELETE;
            return this.SendAsyncWithGraphResponse(null, cancellationToken);
        }

        /// <summary>
        /// Gets the specified ManagedTenant.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The ManagedTenant.</returns>
        public async System.Threading.Tasks.Task<ManagedTenant> GetAsync(CancellationToken cancellationToken = default)
        {
            this.Method = HttpMethods.GET;
            var retrievedEntity = await this.SendAsync<ManagedTenant>(null, cancellationToken).ConfigureAwait(false);
            this.InitializeCollectionProperties(retrievedEntity);
            return retrievedEntity;
        }

        /// <summary>
        /// Gets the specified ManagedTenant and returns a <see cref="GraphResponse{ManagedTenant}"/> object.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="GraphResponse{ManagedTenant}"/> object of the request.</returns>
        public System.Threading.Tasks.Task<GraphResponse<ManagedTenant>> GetResponseAsync(CancellationToken cancellationToken = default)
        {
            this.Method = HttpMethods.GET;
            return this.SendAsyncWithGraphResponse<ManagedTenant>(null, cancellationToken);
        }

        /// <summary>
        /// Updates the specified ManagedTenant using PATCH.
        /// </summary>
        /// <param name="managedTenantToUpdate">The ManagedTenant to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="Microsoft.Graph.ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The updated ManagedTenant.</returns>
        public async System.Threading.Tasks.Task<ManagedTenant> UpdateAsync(ManagedTenant managedTenantToUpdate, CancellationToken cancellationToken = default)
        {
            this.ContentType = CoreConstants.MimeTypeNames.Application.Json;
            this.Method = HttpMethods.PATCH;
            var updatedEntity = await this.SendAsync<ManagedTenant>(managedTenantToUpdate, cancellationToken).ConfigureAwait(false);
            this.InitializeCollectionProperties(updatedEntity);
            return updatedEntity;
        }

        /// <summary>
        /// Updates the specified ManagedTenant using PATCH and returns a <see cref="GraphResponse{ManagedTenant}"/> object.
        /// </summary>
        /// <param name="managedTenantToUpdate">The ManagedTenant to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="Microsoft.Graph.ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The <see cref="GraphResponse{ManagedTenant}"/> object of the request.</returns>
        public System.Threading.Tasks.Task<GraphResponse<ManagedTenant>> UpdateResponseAsync(ManagedTenant managedTenantToUpdate, CancellationToken cancellationToken = default)
        {
            this.ContentType = CoreConstants.MimeTypeNames.Application.Json;
            this.Method = HttpMethods.PATCH;
            return this.SendAsyncWithGraphResponse<ManagedTenant>(managedTenantToUpdate, cancellationToken);
        }

        /// <summary>
        /// Updates the specified ManagedTenant using PUT.
        /// </summary>
        /// <param name="managedTenantToUpdate">The ManagedTenant object to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        public async System.Threading.Tasks.Task<ManagedTenant> PutAsync(ManagedTenant managedTenantToUpdate, CancellationToken cancellationToken = default)
        {
            this.ContentType = CoreConstants.MimeTypeNames.Application.Json;
            this.Method = HttpMethods.PUT;
            var updatedEntity = await this.SendAsync<ManagedTenant>(managedTenantToUpdate, cancellationToken).ConfigureAwait(false);
            this.InitializeCollectionProperties(updatedEntity);
            return updatedEntity;
        }

        /// <summary>
        /// Updates the specified ManagedTenant using PUT and returns a <see cref="GraphResponse{ManagedTenant}"/> object.
        /// </summary>
        /// <param name="managedTenantToUpdate">The ManagedTenant object to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await of <see cref="GraphResponse{ManagedTenant}"/>.</returns>
        public System.Threading.Tasks.Task<GraphResponse<ManagedTenant>> PutResponseAsync(ManagedTenant managedTenantToUpdate, CancellationToken cancellationToken = default)
        {
            this.ContentType = CoreConstants.MimeTypeNames.Application.Json;
            this.Method = HttpMethods.PUT;
            return this.SendAsyncWithGraphResponse<ManagedTenant>(managedTenantToUpdate, cancellationToken);
        }

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        public IManagedTenantRequest Expand(string value)
        {
            this.QueryOptions.Add(new Microsoft.Graph.QueryOption("$expand", value));
            return this;
        }

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        public IManagedTenantRequest Expand(Expression<Func<ManagedTenant, object>> expandExpression)
        {
		    if (expandExpression == null)
            {
                throw new ArgumentNullException(nameof(expandExpression));
            }
            string error;
            string value = Microsoft.Graph.ExpressionExtractHelper.ExtractMembers(expandExpression, out error);
            if (value == null)
            {
                throw new ArgumentException(error, nameof(expandExpression));
            }
            else
            {
                this.QueryOptions.Add(new Microsoft.Graph.QueryOption("$expand", value));
            }
            return this;
        }

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        public IManagedTenantRequest Select(string value)
        {
            this.QueryOptions.Add(new Microsoft.Graph.QueryOption("$select", value));
            return this;
        }

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        public IManagedTenantRequest Select(Expression<Func<ManagedTenant, object>> selectExpression)
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }
            string error;
            string value = Microsoft.Graph.ExpressionExtractHelper.ExtractMembers(selectExpression, out error);
            if (value == null)
            {
                throw new ArgumentException(error, nameof(selectExpression));
            }
            else
            {
                this.QueryOptions.Add(new Microsoft.Graph.QueryOption("$select", value));
            }
            return this;
        }

        /// <summary>
        /// Initializes any collection properties after deserialization, like next requests for paging.
        /// </summary>
        /// <param name="managedTenantToInitialize">The <see cref="ManagedTenant"/> with the collection properties to initialize.</param>
        private void InitializeCollectionProperties(ManagedTenant managedTenantToInitialize)
        {

            if (managedTenantToInitialize != null)
            {
                if (managedTenantToInitialize.ManagementTemplateCollections != null && managedTenantToInitialize.ManagementTemplateCollections.CurrentPage != null)
                {
                    managedTenantToInitialize.ManagementTemplateCollections.InitializeNextPageRequest(this.Client, managedTenantToInitialize.ManagementTemplateCollectionsNextLink);
                    // Copy the additional data collection to the page itself so that information is not lost
                    managedTenantToInitialize.ManagementTemplateCollections.AdditionalData = managedTenantToInitialize.AdditionalData;
                }
                if (managedTenantToInitialize.ManagementTemplates != null && managedTenantToInitialize.ManagementTemplates.CurrentPage != null)
                {
                    managedTenantToInitialize.ManagementTemplates.InitializeNextPageRequest(this.Client, managedTenantToInitialize.ManagementTemplatesNextLink);
                    // Copy the additional data collection to the page itself so that information is not lost
                    managedTenantToInitialize.ManagementTemplates.AdditionalData = managedTenantToInitialize.AdditionalData;
                }

            }


        }
    }
}
