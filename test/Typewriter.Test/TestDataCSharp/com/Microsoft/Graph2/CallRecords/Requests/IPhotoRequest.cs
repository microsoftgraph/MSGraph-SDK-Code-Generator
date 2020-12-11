// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: IEntityRequest.cs.tt

namespace Microsoft.Graph2.CallRecords
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Linq.Expressions;

    /// <summary>
    /// The interface IPhotoRequest.
    /// </summary>
    public partial interface IPhotoRequest : Microsoft.Graph.IBaseRequest
    {
        /// <summary>
        /// Creates the specified Photo using POST.
        /// </summary>
        /// <param name="photoToCreate">The Photo to create.</param>
        /// <returns>The created Photo.</returns>
        System.Threading.Tasks.Task<Photo> CreateAsync(Photo photoToCreate);        /// <summary>
        /// Creates the specified Photo using POST.
        /// </summary>
        /// <param name="photoToCreate">The Photo to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created Photo.</returns>
        System.Threading.Tasks.Task<Photo> CreateAsync(Photo photoToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified Photo.
        /// </summary>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync();

        /// <summary>
        /// Deletes the specified Photo.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specified Photo.
        /// </summary>
        /// <returns>The Photo.</returns>
        System.Threading.Tasks.Task<Photo> GetAsync();

        /// <summary>
        /// Gets the specified Photo.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The Photo.</returns>
        System.Threading.Tasks.Task<Photo> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified Photo using PATCH.
        /// </summary>
        /// <param name="photoToUpdate">The Photo to update.</param>
        /// <returns>The updated Photo.</returns>
        System.Threading.Tasks.Task<Photo> UpdateAsync(Photo photoToUpdate);

        /// <summary>
        /// Updates the specified Photo using PATCH.
        /// </summary>
        /// <param name="photoToUpdate">The Photo to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="Microsoft.Graph.ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The updated Photo.</returns>
        System.Threading.Tasks.Task<Photo> UpdateAsync(Photo photoToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        IPhotoRequest Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        IPhotoRequest Expand(Expression<Func<Photo, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        IPhotoRequest Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        IPhotoRequest Select(Expression<Func<Photo, object>> selectExpression);

    }
}
