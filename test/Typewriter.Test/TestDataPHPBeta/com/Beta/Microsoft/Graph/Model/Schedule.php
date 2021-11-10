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
    * @return bool|null The enabled
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
     * @return TimeOff[]|null The timesOff
     */
    public function getTimesOff()
    {
        if (array_key_exists('timesOff', $this->_propDict) && !is_null($this->_propDict['timesOff'])) {
            $timesOff = [];
            if (count($this->_propDict['timesOff']) > 0 && is_a($this->_propDict['timesOff'][0], 'TimeOff')) {
                return $this->_propDict['timesOff'];
            }
            foreach ($this->_propDict['timesOff'] as $singleValue) {
                $timesOff []= new TimeOff($singleValue);
            }
            $this->_propDict['timesOff'] = $timesOff;
            return $this->_propDict['timesOff'];
        }
        return null;
    }

    /**
    * Sets the timesOff
    *
    * @param TimeOff[] $val The timesOff
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
     * @return TimeOffRequest[]|null The timeOffRequests
     */
    public function getTimeOffRequests()
    {
        if (array_key_exists('timeOffRequests', $this->_propDict) && !is_null($this->_propDict['timeOffRequests'])) {
            $timeOffRequests = [];
            if (count($this->_propDict['timeOffRequests']) > 0 && is_a($this->_propDict['timeOffRequests'][0], 'TimeOffRequest')) {
                return $this->_propDict['timeOffRequests'];
            }
            foreach ($this->_propDict['timeOffRequests'] as $singleValue) {
                $timeOffRequests []= new TimeOffRequest($singleValue);
            }
            $this->_propDict['timeOffRequests'] = $timeOffRequests;
            return $this->_propDict['timeOffRequests'];
        }
        return null;
    }

    /**
    * Sets the timeOffRequests
    *
    * @param TimeOffRequest[] $val The timeOffRequests
    *
    * @return Schedule
    */
    public function setTimeOffRequests($val)
    {
        $this->_propDict["timeOffRequests"] = $val;
        return $this;
    }

}
