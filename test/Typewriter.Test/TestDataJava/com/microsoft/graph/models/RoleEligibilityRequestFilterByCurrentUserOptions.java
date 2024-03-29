// Template Source: Enum.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.models;


/**
 * The Enum Role Eligibility Request Filter By Current User Options.
*/
public enum RoleEligibilityRequestFilterByCurrentUserOptions
{
    /**
    * principal
    */
    PRINCIPAL,
    /**
    * created By
    * @deprecated The createdBy will be deprecated on April 30, 2023.
    */
    @Deprecated
    CREATED_BY,
    /**
    * approver
    */
    APPROVER,
    /**
    * unknown Future Value
    */
    UNKNOWN_FUTURE_VALUE,
    /**
    * For RoleEligibilityRequestFilterByCurrentUserOptions values that were not expected from the service
    */
    UNEXPECTED_VALUE
}
