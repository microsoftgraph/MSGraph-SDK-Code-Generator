<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* Schedule File
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
* Schedule class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class Schedule extends Entity
{
    /**
    * Gets the enabled
    *
    * @return bool The enabled
    */
    public function getEnabled()
    {
        if (array_key_exists("enabled", $this->_propDict)) {
            return $this->_propDict["enabled"];
        } else {
            return null;
        }
    }

    /**
    * Sets the enabled
    *
    * @param bool $val The enabled
    *
    * @return Schedule
    */
    public function setEnabled($val)
    {
        $this->_propDict["enabled"] = boolval($val);
        return $this;
    }


     /**
     * Gets the timesOff
     *
     * @return array The timesOff
     */
    public function getTimesOff()
    {
        if (array_key_exists("timesOff", $this->_propDict)) {
           return $this->_propDict["timesOff"];
        } else {
            return null;
        }
    }

    /**
    * Sets the timesOff
    *
    * @param TimeOff $val The timesOff
    *
    * @return Schedule
    */
    public function setTimesOff($val)
    {
        $this->_propDict["timesOff"] = $val;
        return $this;
    }


     /**
     * Gets the timeOffRequests
     *
     * @return array The timeOffRequests
     */
    public function getTimeOffRequests()
    {
        if (array_key_exists("timeOffRequests", $this->_propDict)) {
           return $this->_propDict["timeOffRequests"];
        } else {
            return null;
        }
    }

    /**
    * Sets the timeOffRequests
    *
    * @param TimeOffRequest $val The timeOffRequests
    *
    * @return Schedule
    */
    public function setTimeOffRequests($val)
    {
        $this->_propDict["timeOffRequests"] = $val;
        return $this;
    }

}
