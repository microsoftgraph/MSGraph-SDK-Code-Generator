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
    /// The interface ITestEntityRequest.
    /// </summary>
    public partial interface ITestEntityRequest : IBaseRequest
    {
        /// <summary>
        /// Creates the specified TestEntity using POST.
        /// </summary>
        /// <param name="testEntityToCreate">The TestEntity to create.</param>
        /// <returns>The created TestEntity.</returns>
        System.Threading.Tasks.Task<TestEntity> CreateAsync(TestEntity testEntityToCreate);

        /// <summary>
        /// Creates the specified TestEntity using POST.
        /// </summary>
        /// <param name="testEntityToCreate">The TestEntity to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created TestEntity.</returns>
        System.Threading.Tasks.Task<TestEntity> CreateAsync(TestEntity testEntityToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Creates the specified TestEntity using POST and returns a <see cref="GraphResponse{TestEntity}"/> object.
        /// </summary>
        /// <param name="testEntityToCreate">The TestEntity to create.</param>
        /// <returns>The <see cref="GraphResponse{TestEntity}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<TestEntity>> CreateResponseAsync(TestEntity testEntityToCreate);

        /// <summary>
        /// Creates the specified TestEntity using POST and returns a <see cref="GraphResponse{TestEntity}"/> object.
        /// </summary>
        /// <param name="testEntityToCreate">The TestEntity to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="GraphResponse{TestEntity}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<TestEntity>> CreateResponseAsync(TestEntity testEntityToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified TestEntity.
        /// </summary>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync();

        /// <summary>
        /// Deletes the specified TestEntity.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified TestEntity and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync();

        /// <summary>
        /// Deletes the specified TestEntity and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specified TestEntity.
        /// </summary>
        /// <returns>The TestEntity.</returns>
        System.Threading.Tasks.Task<TestEntity> GetAsync();

        /// <summary>
        /// Gets the specified TestEntity.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The TestEntity.</returns>
        System.Threading.Tasks.Task<TestEntity> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified TestEntity using PATCH.
        /// </summary>
        /// <param name="testEntityToUpdate">The TestEntity to update.</param>
        /// <returns>The updated TestEntity.</returns>
        System.Threading.Tasks.Task<TestEntity> UpdateAsync(TestEntity testEntityToUpdate);

        /// <summary>
        /// Updates the specified TestEntity using PATCH.
        /// </summary>
        /// <param name="testEntityToUpdate">The TestEntity to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The updated TestEntity.</returns>
        System.Threading.Tasks.Task<TestEntity> UpdateAsync(TestEntity testEntityToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified TestEntity using PATCH and returns a <see cref="GraphResponse{TestEntity}"/> object.
        /// </summary>
        /// <param name="testEntityToUpdate">The TestEntity to update.</param>
        /// <returns>The <see cref="GraphResponse{TestEntity}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<TestEntity>> UpdateResponseAsync(TestEntity testEntityToUpdate);

        /// <summary>
        /// Updates the specified TestEntity using PATCH and returns a <see cref="GraphResponse{TestEntity}"/> object.
        /// </summary>
        /// <param name="testEntityToUpdate">The TestEntity to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The <see cref="GraphResponse{TestEntity}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<TestEntity>> UpdateResponseAsync(TestEntity testEntityToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        ITestEntityRequest Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        ITestEntityRequest Expand(Expression<Func<TestEntity, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        ITestEntityRequest Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        ITestEntityRequest Select(Expression<Func<TestEntity, object>> selectExpression);

    }
}
