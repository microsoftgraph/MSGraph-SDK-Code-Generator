<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* Endpoint File
* PHP version 7
*
* @category  Library
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
namespace Beta\Microsoft\Graph\Model;

/**
* Endpoint class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class Endpoint extends Entity
{
    /**
    * Gets the property1
    *
    * @return int|null The property1
    */
    public function getProperty1()
    {
        if (array_key_exists("property1", $this->_propDict)) {
            return $this->_propDict["property1"];
        } else {
            return null;
        }
    }
    
    /**
    * Sets the property1
    *
    * @param int $val The property1
    *
    * @return Endpoint
    */
    public function setProperty1($val)
    {
        $this->_propDict["property1"] = intval($val);
        return $this;
    }
    
}
