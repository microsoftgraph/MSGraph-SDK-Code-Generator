﻿// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: EntityRequest.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Linq.Expressions;

    /// <summary>
    /// The type OnenotePageRequest.
    /// </summary>
    public partial class OnenotePageRequest : BaseRequest, IOnenotePageRequest
    {
        /// <summary>
        /// Constructs a new OnenotePageRequest.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        /// <param name="options">Query and header option name value pairs for the request.</param>
        public OnenotePageRequest(
            string requestUrl,
            IBaseClient client,
            IEnumerable<Option> options)
            : base(requestUrl, client, options)
        {
        }

        /// <summary>
        /// Creates the specified OnenotePage using POST.
        /// </summary>
        /// <param name="onenotePageToCreate">The OnenotePage to create.</param>
        /// <returns>The created OnenotePage.</returns>
        public System.Threading.Tasks.Task<OnenotePage> CreateAsync(OnenotePage onenotePageToCreate)
        {
            return this.CreateAsync(onenotePageToCreate, CancellationToken.None);
        }

        /// <summary>
        /// Creates the specified OnenotePage using POST.
        /// </summary>
        /// <param name="onenotePageToCreate">The OnenotePage to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created OnenotePage.</returns>
        public async System.Threading.Tasks.Task<OnenotePage> CreateAsync(OnenotePage onenotePageToCreate, CancellationToken cancellationToken)
        {
            this.ContentType = "application/json";
            this.Method = "POST";
            var newEntity = await this.SendAsync<OnenotePage>(onenotePageToCreate, cancellationToken).ConfigureAwait(false);
            this.InitializeCollectionProperties(newEntity);
            return newEntity;
        }

        /// <summary>
        /// Creates the specified OnenotePage using POST and returns a <see cref="GraphResponse{OnenotePage}"/> object.
        /// </summary>
        /// <param name="onenotePageToCreate">The OnenotePage to create.</param>
        /// <returns>The <see cref="GraphResponse{OnenotePage}"/> object of the request.</returns>
        public System.Threading.Tasks.Task<GraphResponse<OnenotePage>> CreateResponseAsync(OnenotePage onenotePageToCreate)
        {
            return this.CreateResponseAsync(onenotePageToCreate, CancellationToken.None);
        }

        /// <summary>
        /// Creates the specified OnenotePage using POST and returns a <see cref="GraphResponse{OnenotePage}"/> object.
        /// </summary>
        /// <param name="onenotePageToCreate">The OnenotePage to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="GraphResponse{OnenotePage}"/> object of the request.</returns>
        public async System.Threading.Tasks.Task<GraphResponse<OnenotePage>> CreateResponseAsync(OnenotePage onenotePageToCreate, CancellationToken cancellationToken)
        {
            this.ContentType = "application/json";
            this.Method = "POST";
            return await this.SendAsyncWithGraphResponse<OnenotePage>(onenotePageToCreate, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the specified OnenotePage.
        /// </summary>
        /// <returns>The task to await.</returns>
        public System.Threading.Tasks.Task DeleteAsync()
        {
            return this.DeleteAsync(CancellationToken.None);
        }

        /// <summary>
        /// Deletes the specified OnenotePage.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        public async System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken)
        {
            this.Method = "DELETE";
            await this.SendAsync<OnenotePage>(null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the specified OnenotePage and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        public System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync()
        {
            return this.DeleteResponseAsync(CancellationToken.None);
        }

        /// <summary>
        /// Deletes the specified OnenotePage and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        public async System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync(CancellationToken cancellationToken)
        {
            this.Method = "DELETE";
            return await this.SendAsyncWithGraphResponse(null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the specified OnenotePage.
        /// </summary>
        /// <returns>The OnenotePage.</returns>
        public System.Threading.Tasks.Task<OnenotePage> GetAsync()
        {
            return this.GetAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets the specified OnenotePage.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The OnenotePage.</returns>
        public async System.Threading.Tasks.Task<OnenotePage> GetAsync(CancellationToken cancellationToken)
        {
            this.Method = "GET";
            var retrievedEntity = await this.SendAsync<OnenotePage>(null, cancellationToken).ConfigureAwait(false);
            this.InitializeCollectionProperties(retrievedEntity);
            return retrievedEntity;
        }

        /// <summary>
        /// Updates the specified OnenotePage using PATCH.
        /// </summary>
        /// <param name="onenotePageToUpdate">The OnenotePage to update.</param>
        /// <returns>The updated OnenotePage.</returns>
        public System.Threading.Tasks.Task<OnenotePage> UpdateAsync(OnenotePage onenotePageToUpdate)
        {
            return this.UpdateAsync(onenotePageToUpdate, CancellationToken.None);
        }

        /// <summary>
        /// Updates the specified OnenotePage using PATCH.
        /// </summary>
        /// <param name="onenotePageToUpdate">The OnenotePage to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The updated OnenotePage.</returns>
        public async System.Threading.Tasks.Task<OnenotePage> UpdateAsync(OnenotePage onenotePageToUpdate, CancellationToken cancellationToken)
        {
			if (onenotePageToUpdate.AdditionalData != null)
			{
				if (onenotePageToUpdate.AdditionalData.ContainsKey(Constants.HttpPropertyNames.ResponseHeaders) ||
					onenotePageToUpdate.AdditionalData.ContainsKey(Constants.HttpPropertyNames.StatusCode))
				{
					throw new ClientException(
						new Error
						{
							Code = GeneratedErrorConstants.Codes.NotAllowed,
							Message = String.Format(GeneratedErrorConstants.Messages.ResponseObjectUsedForUpdate, onenotePageToUpdate.GetType().Name)
						});
				}
			}
            if (onenotePageToUpdate.AdditionalData != null)
            {
                if (onenotePageToUpdate.AdditionalData.ContainsKey(Constants.HttpPropertyNames.ResponseHeaders) ||
                    onenotePageToUpdate.AdditionalData.ContainsKey(Constants.HttpPropertyNames.StatusCode))
                {
                    throw new ClientException(
                        new Error
                        {
                            Code = GeneratedErrorConstants.Codes.NotAllowed,
                            Message = String.Format(GeneratedErrorConstants.Messages.ResponseObjectUsedForUpdate, onenotePageToUpdate.GetType().Name)
                        });
                }
            }
            this.ContentType = "application/json";
            this.Method = "PATCH";
            var updatedEntity = await this.SendAsync<OnenotePage>(onenotePageToUpdate, cancellationToken).ConfigureAwait(false);
            this.InitializeCollectionProperties(updatedEntity);
            return updatedEntity;
        }

        /// <summary>
        /// Updates the specified OnenotePage using PATCH and returns a <see cref="GraphResponse{OnenotePage}"/> object.
        /// </summary>
        /// <param name="onenotePageToUpdate">The OnenotePage to update.</param>
        /// <returns>The <see cref="GraphResponse{OnenotePage}"/> object of the request.</returns>
        public System.Threading.Tasks.Task<GraphResponse<OnenotePage>> UpdateResponseAsync(OnenotePage onenotePageToUpdate)
        {
            return this.UpdateResponseAsync(onenotePageToUpdate, CancellationToken.None);
        }

        /// <summary>
        /// Updates the specified OnenotePage using PATCH and returns a <see cref="GraphResponse{OnenotePage}"/> object.
        /// </summary>
        /// <param name="onenotePageToUpdate">The OnenotePage to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The <see cref="GraphResponse{OnenotePage}"/> object of the request.</returns>
        public async System.Threading.Tasks.Task<GraphResponse<OnenotePage>> UpdateResponseAsync(OnenotePage onenotePageToUpdate, CancellationToken cancellationToken)
        {
			if (onenotePageToUpdate.AdditionalData != null)
			{
				if (onenotePageToUpdate.AdditionalData.ContainsKey(Constants.HttpPropertyNames.ResponseHeaders) ||
					onenotePageToUpdate.AdditionalData.ContainsKey(Constants.HttpPropertyNames.StatusCode))
				{
					throw new ClientException(
						new Error
						{
							Code = GeneratedErrorConstants.Codes.NotAllowed,
							Message = String.Format(GeneratedErrorConstants.Messages.ResponseObjectUsedForUpdate, onenotePageToUpdate.GetType().Name)
						});
				}
			}
            if (onenotePageToUpdate.AdditionalData != null)
            {
                if (onenotePageToUpdate.AdditionalData.ContainsKey(Constants.HttpPropertyNames.ResponseHeaders) ||
                    onenotePageToUpdate.AdditionalData.ContainsKey(Constants.HttpPropertyNames.StatusCode))
                {
                    throw new ClientException(
                        new Error
                        {
                            Code = GeneratedErrorConstants.Codes.NotAllowed,
                            Message = String.Format(GeneratedErrorConstants.Messages.ResponseObjectUsedForUpdate, onenotePageToUpdate.GetType().Name)
                        });
                }
            }
            this.ContentType = "application/json";
            this.Method = "PATCH";
            return await this.SendAsyncWithGraphResponse<OnenotePage>(onenotePageToUpdate, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        public IOnenotePageRequest Expand(string value)
        {
            this.QueryOptions.Add(new QueryOption("$expand", value));
            return this;
        }

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        public IOnenotePageRequest Expand(Expression<Func<OnenotePage, object>> expandExpression)
        {
		    if (expandExpression == null)
            {
                throw new ArgumentNullException(nameof(expandExpression));
            }
            string error;
            string value = ExpressionExtractHelper.ExtractMembers(expandExpression, out error);
            if (value == null)
            {
                throw new ArgumentException(error, nameof(expandExpression));
            }
            else
            {
                this.QueryOptions.Add(new QueryOption("$expand", value));
            }
            return this;
        }

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        public IOnenotePageRequest Select(string value)
        {
            this.QueryOptions.Add(new QueryOption("$select", value));
            return this;
        }

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        public IOnenotePageRequest Select(Expression<Func<OnenotePage, object>> selectExpression)
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }
            string error;
            string value = ExpressionExtractHelper.ExtractMembers(selectExpression, out error);
            if (value == null)
            {
                throw new ArgumentException(error, nameof(selectExpression));
            }
            else
            {
                this.QueryOptions.Add(new QueryOption("$select", value));
            }
            return this;
        }

        /// <summary>
        /// Initializes any collection properties after deserialization, like next requests for paging.
        /// </summary>
        /// <param name="onenotePageToInitialize">The <see cref="OnenotePage"/> with the collection properties to initialize.</param>
        private void InitializeCollectionProperties(OnenotePage onenotePageToInitialize)
        {

        }
    }
}
