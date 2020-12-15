// Template Source: IBaseEntityRequest.java.tt
// ------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

package com.microsoft.graph.requests.extensions;
import com.microsoft.graph.models.extensions.TestEntity;

import com.microsoft.graph.concurrency.ICallback;
import com.microsoft.graph.core.ClientException;
import com.microsoft.graph.http.IHttpRequest;

// **NOTE** This file was generated by a tool and any changes will be overwritten.

/**
 * The interface for the Test Entity Request.
 */
public interface ITestEntityRequest extends IHttpRequest {

    /**
     * Gets the TestEntity from the service
     *
     * @param callback the callback to be called after success or failure
     */
    void get(final ICallback<? super TestEntity> callback);

    /**
     * Gets the TestEntity from the service
     *
     * @return the TestEntity from the request
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    TestEntity get() throws ClientException;

    /**
     * Delete this item from the service
     *
     * @param callback the callback when the deletion action has completed
     */
    void delete(final ICallback<? super TestEntity> callback);

    /**
     * Delete this item from the service
     *
     * @throws ClientException if there was an exception during the delete operation
     */
    void delete() throws ClientException;

    /**
     * Patches this TestEntity with a source
     *
     * @param sourceTestEntity the source object with updates
     * @param callback the callback to be called after success or failure
     */
    void patch(final TestEntity sourceTestEntity, final ICallback<? super TestEntity> callback);

    /**
     * Patches this TestEntity with a source
     *
     * @param sourceTestEntity the source object with updates
     * @return the updated TestEntity
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    TestEntity patch(final TestEntity sourceTestEntity) throws ClientException;

    /**
     * Posts a TestEntity with a new object
     *
     * @param newTestEntity the new object to create
     * @param callback the callback to be called after success or failure
     */
    void post(final TestEntity newTestEntity, final ICallback<? super TestEntity> callback);

    /**
     * Posts a TestEntity with a new object
     *
     * @param newTestEntity the new object to create
     * @return the created TestEntity
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    TestEntity post(final TestEntity newTestEntity) throws ClientException;

    /**
     * Posts a TestEntity with a new object
     *
     * @param newTestEntity the object to create/update
     * @param callback the callback to be called after success or failure
     */
    void put(final TestEntity newTestEntity, final ICallback<? super TestEntity> callback);

    /**
     * Posts a TestEntity with a new object
     *
     * @param newTestEntity the object to create/update
     * @return the created TestEntity
     * @throws ClientException this exception occurs if the request was unable to complete for any reason
     */
    TestEntity put(final TestEntity newTestEntity) throws ClientException;

    /**
     * Sets the select clause for the request
     *
     * @param value the select clause
     * @return the updated request
     */
    ITestEntityRequest select(final String value);

    /**
     * Sets the expand clause for the request
     *
     * @param value the expand clause
     * @return the updated request
     */
    ITestEntityRequest expand(final String value);

}

