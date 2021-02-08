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
    /// The interface IOptionRequest.
    /// </summary>
    public partial interface IOptionRequest : Microsoft.Graph.IBaseRequest
    {
        /// <summary>
        /// Creates the specified Option using POST.
        /// </summary>
        /// <param name="optionToCreate">The Option to create.</param>
        /// <returns>The created Option.</returns>
        System.Threading.Tasks.Task<Option> CreateAsync(Option optionToCreate);

        /// <summary>
        /// Creates the specified Option using POST.
        /// </summary>
        /// <param name="optionToCreate">The Option to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created Option.</returns>
        System.Threading.Tasks.Task<Option> CreateAsync(Option optionToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Creates the specified Option using POST and returns a <see cref="GraphResponse{Option}"/> object.
        /// </summary>
        /// <param name="optionToCreate">The Option to create.</param>
        /// <returns>The <see cref="GraphResponse{Option}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<Option>> CreateResponseAsync(Option optionToCreate);

        /// <summary>
        /// Creates the specified Option using POST and returns a <see cref="GraphResponse{Option}"/> object.
        /// </summary>
        /// <param name="optionToCreate">The Option to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="GraphResponse{Option}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<Option>> CreateResponseAsync(Option optionToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified Option.
        /// </summary>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync();

        /// <summary>
        /// Deletes the specified Option.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified Option and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync();

        /// <summary>
        /// Deletes the specified Option and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specified Option.
        /// </summary>
        /// <returns>The Option.</returns>
        System.Threading.Tasks.Task<Option> GetAsync();

        /// <summary>
        /// Gets the specified Option.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The Option.</returns>
        System.Threading.Tasks.Task<Option> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified Option using PATCH.
        /// </summary>
        /// <param name="optionToUpdate">The Option to update.</param>
        /// <returns>The updated Option.</returns>
        System.Threading.Tasks.Task<Option> UpdateAsync(Option optionToUpdate);

        /// <summary>
        /// Updates the specified Option using PATCH.
        /// </summary>
        /// <param name="optionToUpdate">The Option to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="Microsoft.Graph.ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The updated Option.</returns>
        System.Threading.Tasks.Task<Option> UpdateAsync(Option optionToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified Option using PATCH and returns a <see cref="GraphResponse{Option}"/> object.
        /// </summary>
        /// <param name="optionToUpdate">The Option to update.</param>
        /// <returns>The <see cref="GraphResponse{Option}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<Option>> UpdateResponseAsync(Option optionToUpdate);

        /// <summary>
        /// Updates the specified Option using PATCH and returns a <see cref="GraphResponse{Option}"/> object.
        /// </summary>
        /// <param name="optionToUpdate">The Option to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="Microsoft.Graph.ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The <see cref="GraphResponse{Option}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<Option>> UpdateResponseAsync(Option optionToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        IOptionRequest Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        IOptionRequest Expand(Expression<Func<Option, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        IOptionRequest Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        IOptionRequest Select(Expression<Func<Option, object>> selectExpression);

    }
}
