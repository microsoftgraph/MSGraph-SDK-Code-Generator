<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* UserAgent File
* PHP version 7
*
* @category  Library
* @package   Microsoft.Graph
* @copyright © Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
namespace Microsoft\Graph\CallRecords\Model;
/**
* UserAgent class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright © Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class UserAgent extends Microsoft\Graph\Model\Entity
{
    /**
    * Gets the headerValue
    *
    * @return string The headerValue
    */
    public function getHeaderValue()
    {
        if (array_key_exists("headerValue", $this->_propDict)) {
            return $this->_propDict["headerValue"];
        } else {
            return null;
        }
    }

    /**
    * Sets the headerValue
    *
    * @param string $val The value of the headerValue
    *
    * @return UserAgent
    */
    public function setHeaderValue($val)
    {
        $this->_propDict["headerValue"] = $val;
        return $this;
    }
    /**
    * Gets the applicationVersion
    *
    * @return string The applicationVersion
    */
    public function getApplicationVersion()
    {
        if (array_key_exists("applicationVersion", $this->_propDict)) {
            return $this->_propDict["applicationVersion"];
        } else {
            return null;
        }
    }

    /**
    * Sets the applicationVersion
    *
    * @param string $val The value of the applicationVersion
    *
    * @return UserAgent
    */
    public function setApplicationVersion($val)
    {
        $this->_propDict["applicationVersion"] = $val;
        return $this;
    }
}
