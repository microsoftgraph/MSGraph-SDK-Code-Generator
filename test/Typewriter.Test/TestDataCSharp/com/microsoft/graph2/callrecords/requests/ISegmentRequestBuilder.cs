// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.
// <auto-generated/>

// Template Source: IEntityRequestBuilder.cs.tt

namespace Microsoft.Graph2.CallRecords
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The interface ISegmentRequestBuilder.
    /// </summary>
    public partial interface ISegmentRequestBuilder : Microsoft.Graph.IEntityRequestBuilder
    {
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        new ISegmentRequest Request();

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        new ISegmentRequest Request(IEnumerable<Microsoft.Graph.Option> options);
    
        /// <summary>
        /// Gets the request builder for RefTypes.
        /// </summary>
        /// <returns>The <see cref="ISegmentRefTypesCollectionWithReferencesRequestBuilder"/>.</returns>
        ISegmentRefTypesCollectionWithReferencesRequestBuilder RefTypes { get; }

        /// <summary>
        /// Gets the request builder for RefType.
        /// </summary>
        /// <returns>The <see cref="Microsoft.Graph.ICallWithReferenceRequestBuilder"/>.</returns>
        Microsoft.Graph.ICallWithReferenceRequestBuilder RefType { get; }

        /// <summary>
        /// Gets the request builder for SessionRef.
        /// </summary>
        /// <returns>The <see cref="ISessionWithReferenceRequestBuilder"/>.</returns>
        ISessionWithReferenceRequestBuilder SessionRef { get; }

        /// <summary>
        /// Gets the request builder for Photo.
        /// </summary>
        /// <returns>The <see cref="IPhotoRequestBuilder"/>.</returns>
        IPhotoRequestBuilder Photo { get; }
    
        /// <summary>
        /// Gets the request builder for SegmentForward.
        /// </summary>
        /// <returns>The <see cref="ISegmentForwardRequestBuilder"/>.</returns>
        ISegmentForwardRequestBuilder Forward(
            IEnumerable<Microsoft.Graph.Recipient> ToRecipients,
            Microsoft.Graph.Recipient SingleRecipient,
            IEnumerable<Session> MultipleSessions,
            Session SingleSession,
            string Comment = null);

        /// <summary>
        /// Gets the request builder for SegmentTestAction.
        /// </summary>
        /// <returns>The <see cref="ISegmentTestActionRequestBuilder"/>.</returns>
        ISegmentTestActionRequestBuilder TestAction(
            Microsoft.Graph.IdentitySet value = null);
    
    }
}
