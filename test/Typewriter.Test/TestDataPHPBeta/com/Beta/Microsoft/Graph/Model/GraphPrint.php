<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* GraphPrint File
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
* GraphPrint class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class Print implements \JsonSerializable
{
    /**
    * Gets the settings
    *
    * @return string The settings
    */
    public function getSettings()
    {
        if (array_key_exists("settings", $this->_propDict)) {
            return $this->_propDict["settings"];
        } else {
            return null;
        }
    }

    /**
    * Sets the settings
    *
    * @param string $val The settings
    *
    * @return Print
    */
    public function setSettings($val)
    {
        $this->_propDict["settings"] = $val;
        return $this;
    }

}
