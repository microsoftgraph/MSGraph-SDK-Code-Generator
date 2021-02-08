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
    /// The interface ICallRecordRequest.
    /// </summary>
    public partial interface ICallRecordRequest : Microsoft.Graph.IBaseRequest
    {
        /// <summary>
        /// Creates the specified CallRecord using POST.
        /// </summary>
        /// <param name="callRecordToCreate">The CallRecord to create.</param>
        /// <returns>The created CallRecord.</returns>
        System.Threading.Tasks.Task<CallRecord> CreateAsync(CallRecord callRecordToCreate);

        /// <summary>
        /// Creates the specified CallRecord using POST.
        /// </summary>
        /// <param name="callRecordToCreate">The CallRecord to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The created CallRecord.</returns>
        System.Threading.Tasks.Task<CallRecord> CreateAsync(CallRecord callRecordToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Creates the specified CallRecord using POST and returns a <see cref="GraphResponse{CallRecord}"/> object.
        /// </summary>
        /// <param name="callRecordToCreate">The CallRecord to create.</param>
        /// <returns>The <see cref="GraphResponse{CallRecord}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<CallRecord>> CreateResponseAsync(CallRecord callRecordToCreate);

        /// <summary>
        /// Creates the specified CallRecord using POST and returns a <see cref="GraphResponse{CallRecord}"/> object.
        /// </summary>
        /// <param name="callRecordToCreate">The CallRecord to create.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The <see cref="GraphResponse{CallRecord}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<CallRecord>> CreateResponseAsync(CallRecord callRecordToCreate, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified CallRecord.
        /// </summary>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync();

        /// <summary>
        /// Deletes the specified CallRecord.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        System.Threading.Tasks.Task DeleteAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the specified CallRecord and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync();

        /// <summary>
        /// Deletes the specified CallRecord and returns a <see cref="GraphResponse"/> object.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task of <see cref="GraphResponse"/> to await.</returns>
        System.Threading.Tasks.Task<GraphResponse> DeleteResponseAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specified CallRecord.
        /// </summary>
        /// <returns>The CallRecord.</returns>
        System.Threading.Tasks.Task<CallRecord> GetAsync();

        /// <summary>
        /// Gets the specified CallRecord.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The CallRecord.</returns>
        System.Threading.Tasks.Task<CallRecord> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified CallRecord using PATCH.
        /// </summary>
        /// <param name="callRecordToUpdate">The CallRecord to update.</param>
        /// <returns>The updated CallRecord.</returns>
        System.Threading.Tasks.Task<CallRecord> UpdateAsync(CallRecord callRecordToUpdate);

        /// <summary>
        /// Updates the specified CallRecord using PATCH.
        /// </summary>
        /// <param name="callRecordToUpdate">The CallRecord to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="Microsoft.Graph.ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The updated CallRecord.</returns>
        System.Threading.Tasks.Task<CallRecord> UpdateAsync(CallRecord callRecordToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the specified CallRecord using PATCH and returns a <see cref="GraphResponse{CallRecord}"/> object.
        /// </summary>
        /// <param name="callRecordToUpdate">The CallRecord to update.</param>
        /// <returns>The <see cref="GraphResponse{CallRecord}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<CallRecord>> UpdateResponseAsync(CallRecord callRecordToUpdate);

        /// <summary>
        /// Updates the specified CallRecord using PATCH and returns a <see cref="GraphResponse{CallRecord}"/> object.
        /// </summary>
        /// <param name="callRecordToUpdate">The CallRecord to update.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <exception cref="Microsoft.Graph.ClientException">Thrown when an object returned in a response is used for updating an object in Microsoft Graph.</exception>
        /// <returns>The <see cref="GraphResponse{CallRecord}"/> object of the request.</returns>
        System.Threading.Tasks.Task<GraphResponse<CallRecord>> UpdateResponseAsync(CallRecord callRecordToUpdate, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="value">The expand value.</param>
        /// <returns>The request object to send.</returns>
        ICallRecordRequest Expand(string value);

        /// <summary>
        /// Adds the specified expand value to the request.
        /// </summary>
        /// <param name="expandExpression">The expression from which to calculate the expand value.</param>
        /// <returns>The request object to send.</returns>
        ICallRecordRequest Expand(Expression<Func<CallRecord, object>> expandExpression);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="value">The select value.</param>
        /// <returns>The request object to send.</returns>
        ICallRecordRequest Select(string value);

        /// <summary>
        /// Adds the specified select value to the request.
        /// </summary>
        /// <param name="selectExpression">The expression from which to calculate the select value.</param>
        /// <returns>The request object to send.</returns>
        ICallRecordRequest Select(Expression<Func<CallRecord, object>> selectExpression);

    }
}
