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
    /// The interface ISingletonEntity2Request.
    /// </summary>
    public partial interface ISingletonEntity2Request : IBaseRequest
    {
        /// <summary>
        /// Creates the specified SingletonEntity2 using POST.
        /// </summary>
        /// <param name="singletonEntity2ToCreate">The SingletonEntity2 to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created SingletonEntity2.</returns>
        System.Threading.Tasks.Task<SingletonEntity2> CreateAsync(SingletonEntity2 singletonEntity2ToCreate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates the specified SingletonEntity2 using POST and returns a <see cref="GraphResponse{SingletonEntity2}"/> object.
        /// </summary>
        /// <param name="singletonEntity2ToCreate">The SingletonEntity2 to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="GraphResponse{SingletonEntity2}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<SingletonEntity2>> CreateResponseAsync(SingletonEntity2 singletonEntity2ToCreate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the specified SingletonEntity2.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the specified SingletonEntity2 and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the specified SingletonEntity2.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The SingletonEntity2.</returns>
        System.Threading.Tasks.Task<SingletonEntity2> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the specified SingletonEntity2 and returns a <see cref="GraphResponse{SingletonEntity2}"/> object.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="GraphResponse{SingletonEntity2}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<SingletonEntity2>> GetResponseAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the specified SingletonEntity2 using PATCH.
        /// </summary>
        /// <param name="singletonEntity2ToUpdate">The SingletonEntity2 to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The updated SingletonEntity2.</returns>
        System.Threading.Tasks.Task<SingletonEntity2> UpdateAsync(SingletonEntity2 singletonEntity2ToUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the specified SingletonEntity2 using PATCH and returns a <see cref="GraphResponse{SingletonEntity2}"/> object.
        /// </summary>
        /// <param name="singletonEntity2ToUpdate">The SingletonEntity2 to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The <see cref="GraphResponse{SingletonEntity2}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<SingletonEntity2>> UpdateResponseAsync(SingletonEntity2 singletonEntity2ToUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the specified SingletonEntity2 using PUT.
        /// </summary>
        /// <param name="singletonEntity2ToUpdate">The SingletonEntity2 object to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task<SingletonEntity2> PutAsync(SingletonEntity2 singletonEntity2ToUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the specified SingletonEntity2 using PUT and returns a <see cref="GraphResponse{SingletonEntity2}"/> object.
        /// </summary>
        /// <param name="singletonEntity2ToUpdate">The SingletonEntity2 object to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task of <see cref="GraphResponse{SingletonEntity2}"/> to await.</returns>
        System.Threading.Tasks.Task<GraphResponse<SingletonEntity2>> PutResponseAsync(SingletonEntity2 singletonEntity2ToUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        ISingletonEntity2Request Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        ISingletonEntity2Request Expand(Expression<Func<SingletonEntity2, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        ISingletonEntity2Request Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        ISingletonEntity2Request Select(Expression<Func<SingletonEntity2, object>> selectExpression);

    }
}
