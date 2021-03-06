<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* ServiceUserAgent File
* PHP version 7
*
* @category  Library
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
namespace Beta\Microsoft\Graph\CallRecords\Model;
/**
* ServiceUserAgent class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class ServiceUserAgent extends UserAgent
{

    /**
    * Gets the role
    *
    * @return ServiceRole|null The role
    */
    public function getRole()
    {
        if (array_key_exists("role", $this->_propDict)) {
            if (is_a($this->_propDict["role"], "\Beta\Microsoft\Graph\CallRecords\Model\ServiceRole") || is_null($this->_propDict["role"])) {
                return $this->_propDict["role"];
            } else {
                $this->_propDict["role"] = new ServiceRole($this->_propDict["role"]);
                return $this->_propDict["role"];
            }
        }
        return null;
    }

    /**
    * Sets the role
    *
    * @param ServiceRole $val The value to assign to the role
    *
    * @return ServiceUserAgent The ServiceUserAgent
    */
    public function setRole($val)
    {
        $this->_propDict["role"] = $val;
         return $this;
    }
}
