// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: IEntityCollectionWithReferencesRequest.cs.tt

namespace Microsoft.Graph.ManagedTenants
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Linq.Expressions;

    /// <summary>
    /// The interface IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest.
    /// </summary>
    public partial interface IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest : Microsoft.Graph.IBaseRequest
    {

        /// <summary>
        /// Gets the collection page.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The collection page.</returns>
        System.Threading.Tasks.Task<IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesPage> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the collection page and returns a <see cref="GraphResponse{ManagementTemplateManagementTemplateCollectionsCollectionWithReferencesResponse}"/> object.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="GraphResponse{ManagementTemplateManagementTemplateCollectionsCollectionWithReferencesResponse}"/> object.</returns>
        System.Threading.Tasks.Task<GraphResponse<ManagementTemplateManagementTemplateCollectionsCollectionWithReferencesResponse>> GetResponseAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest Expand(Expression<Func<ManagementTemplateCollection, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest Select(Expression<Func<ManagementTemplateCollection, object>> selectExpression);

        /// <summary>
        /// Adds the specified top value to the request.
        /// </summary>
        /// <param name="value">The top value.</param>
        /// <returns>The request object to send.</returns>
        IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest Top(int value);

        /// <summary>
        /// Adds the specified filter value to the request.
        /// </summary>
        /// <param name="value">The filter value.</param>
        /// <returns>The request object to send.</returns>
        IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest Filter(string value);

        /// <summary>
        /// Adds the specified skip value to the request.
        /// </summary>
        /// <param name="value">The skip value.</param>
        /// <returns>The request object to send.</returns>
        IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest Skip(int value);

        /// <summary>
        /// Adds the specified orderby value to the request.
        /// </summary>
        /// <param name="value">The orderby value.</param>
        /// <returns>The request object to send.</returns>
        IManagementTemplateManagementTemplateCollectionsCollectionWithReferencesRequest OrderBy(string value);
    }
}
