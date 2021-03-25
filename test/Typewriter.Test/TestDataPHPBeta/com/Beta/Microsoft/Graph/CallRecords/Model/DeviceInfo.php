<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* DeviceInfo File
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
* DeviceInfo class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class DeviceInfo extends \Beta\Microsoft\Graph\Model\Entity
{
    /**
    * Gets the captureDeviceName
    *
    * @return string The captureDeviceName
    */
    public function getCaptureDeviceName()
    {
        if (array_key_exists("captureDeviceName", $this->_propDict)) {
            return $this->_propDict["captureDeviceName"];
        } else {
            return null;
        }
    }

    /**
    * Sets the captureDeviceName
    *
    * @param string $val The value of the captureDeviceName
    *
    * @return DeviceInfo
    */
    public function setCaptureDeviceName($val)
    {
        $this->_propDict["captureDeviceName"] = $val;
        return $this;
    }
    /**
    * Gets the sentSignalLevel
    *
    * @return int The sentSignalLevel
    */
    public function getSentSignalLevel()
    {
        if (array_key_exists("sentSignalLevel", $this->_propDict)) {
            return $this->_propDict["sentSignalLevel"];
        } else {
            return null;
        }
    }

    /**
    * Sets the sentSignalLevel
    *
    * @param int $val The value of the sentSignalLevel
    *
    * @return DeviceInfo
    */
    public function setSentSignalLevel($val)
    {
        $this->_propDict["sentSignalLevel"] = $val;
        return $this;
    }

    /**
    * Gets the speakerGlitchRate
    *
    * @return \Beta\Microsoft\Graph\Model\Single The speakerGlitchRate
    */
    public function getSpeakerGlitchRate()
    {
        if (array_key_exists("speakerGlitchRate", $this->_propDict)) {
            if (is_a($this->_propDict["speakerGlitchRate"], "\Beta\Microsoft\Graph\Model\Single")) {
                return $this->_propDict["speakerGlitchRate"];
            } else {
                $this->_propDict["speakerGlitchRate"] = new \Beta\Microsoft\Graph\Model\Single($this->_propDict["speakerGlitchRate"]);
                return $this->_propDict["speakerGlitchRate"];
            }
        }
        return null;
    }

    /**
    * Sets the speakerGlitchRate
    *
    * @param \Beta\Microsoft\Graph\Model\Single $val The value to assign to the speakerGlitchRate
    *
    * @return DeviceInfo The DeviceInfo
    */
    public function setSpeakerGlitchRate($val)
    {
        $this->_propDict["speakerGlitchRate"] = $val;
         return $this;
    }
}
