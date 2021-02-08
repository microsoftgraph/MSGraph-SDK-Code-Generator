// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: IEntityWithReferenceRequest.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Linq.Expressions;

    /// <summary>
    /// The interface ITestTypeWithReferenceRequest.
    /// </summary>
    public partial interface ITestTypeWithReferenceRequest : IBaseRequest
    {
        /// <summary>
        /// Gets the specified TestType.
        /// </summary>
        /// <returns>The TestType.</returns>
        System.Threading.Tasks.Task<TestType> GetAsync();

        /// <summary>
        /// Gets the specified TestType.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The TestType.</returns>
        System.Threading.Tasks.Task<TestType> GetAsync(CancellationToken cancellationToken);

		/// <summary>
        /// Creates the specified TestType using POST.
        /// </summary>
        /// <param name="testTypeToCreate">The TestType to create.</param>
        /// <returns>The created TestType.</returns>
        System.Threading.Tasks.Task<TestType> CreateAsync(TestType testTypeToCreate);

        /// <summary>
        /// Creates the specified TestType using POST.
        /// </summary>
        /// <param name="testTypeToCreate">The TestType to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created TestType.</returns>
        System.Threading.Tasks.Task<TestType> CreateAsync(TestType testTypeToCreate, CancellationToken cancellationToken);

		/// <summary>
        /// Creates the specified TestType using POST and returns a <see cref="GraphResponse{TestType}"/> object.
        /// </summary>
        /// <param name="testTypeToCreate">The TestType to create.</param>
        /// <returns>The <see cref="GraphResponse{TestType}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<TestType>> CreateResponseAsync(TestType testTypeToCreate);

        /// <summary>
        /// Creates the specified TestType using POST and returns a <see cref="GraphResponse{TestType}"/> object.
        /// </summary>
        /// <param name="testTypeToCreate">The TestType to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="GraphResponse{TestType}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<TestType>> CreateResponseAsync(TestType testTypeToCreate, CancellationToken cancellationToken);

		/// <summary>
        /// Updates the specified TestType using PATCH.
        /// </summary>
        /// <param name="testTypeToUpdate">The TestType to update.</param>
        /// <returns>The updated TestType.</returns>
        System.Threading.Tasks.Task<TestType> UpdateAsync(TestType testTypeToUpdate);

        /// <summary>
        /// Updates the specified TestType using PATCH.
        /// </summary>
        /// <param name="testTypeToUpdate">The TestType to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The updated TestType.</returns>
        System.Threading.Tasks.Task<TestType> UpdateAsync(TestType testTypeToUpdate, CancellationToken cancellationToken);

		/// <summary>
        /// Updates the specified TestType using PATCH and returns a <see cref="GraphResponse{TestType}"/> object.
        /// </summary>
        /// <param name="testTypeToUpdate">The TestType to update.</param>
        /// <returns>The <see cref="GraphResponse{TestType}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<TestType>> UpdateResponseAsync(TestType testTypeToUpdate);

        /// <summary>
        /// Updates the specified TestType using PATCH and returns a <see cref="GraphResponse{TestType}"/> object.
        /// </summary>
        /// <param name="testTypeToUpdate">The TestType to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The <see cref="GraphResponse{TestType}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<TestType>> UpdateResponseAsync(TestType testTypeToUpdate, CancellationToken cancellationToken);

		/// <summary>
        /// Deletes the specified TestType.
        /// </summary>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync();

        /// <summary>
        /// Deletes the specified TestType.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken);

		/// <summary>
        /// Deletes the specified TestType and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync();

        /// <summary>
        /// Deletes the specified TestType and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        ITestTypeWithReferenceRequest Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        ITestTypeWithReferenceRequest Expand(Expression<Func<TestType, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        ITestTypeWithReferenceRequest Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        ITestTypeWithReferenceRequest Select(Expression<Func<TestType, object>> selectExpression);

    }
}
