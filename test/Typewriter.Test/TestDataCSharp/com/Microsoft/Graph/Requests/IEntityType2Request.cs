// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: IEntityRequest.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Linq.Expressions;

    /// <summary>
    /// The interface IEntityType2Request.
    /// </summary>
    public partial interface IEntityType2Request : IBaseRequest
    {
        /// <summary>
        /// Creates the specified EntityType2 using POST.
        /// </summary>
        /// <param name="entityType2ToCreate">The EntityType2 to create.</param>
        /// <returns>The created EntityType2.</returns>
        System.Threading.Tasks.Task<EntityType2> CreateAsync(EntityType2 entityType2ToCreate);        /// <summary>
        /// Creates the specified EntityType2 using POST.
        /// </summary>
        /// <param name="entityType2ToCreate">The EntityType2 to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created EntityType2.</returns>
        System.Threading.Tasks.Task<EntityType2> CreateAsync(EntityType2 entityType2ToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified EntityType2.
        /// </summary>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync();

        /// <summary>
        /// Deletes the specified EntityType2.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specified EntityType2.
        /// </summary>
        /// <returns>The EntityType2.</returns>
        System.Threading.Tasks.Task<EntityType2> GetAsync();

        /// <summary>
        /// Gets the specified EntityType2.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The EntityType2.</returns>
        System.Threading.Tasks.Task<EntityType2> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified EntityType2 using PATCH.
        /// </summary>
        /// <param name="entityType2ToUpdate">The EntityType2 to update.</param>
        /// <returns>The updated EntityType2.</returns>
        System.Threading.Tasks.Task<EntityType2> UpdateAsync(EntityType2 entityType2ToUpdate);

        /// <summary>
        /// Updates the specified EntityType2 using PATCH.
        /// </summary>
        /// <param name="entityType2ToUpdate">The EntityType2 to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The updated EntityType2.</returns>
        System.Threading.Tasks.Task<EntityType2> UpdateAsync(EntityType2 entityType2ToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        IEntityType2Request Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        IEntityType2Request Expand(Expression<Func<EntityType2, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        IEntityType2Request Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        IEntityType2Request Select(Expression<Func<EntityType2, object>> selectExpression);

    }
}
