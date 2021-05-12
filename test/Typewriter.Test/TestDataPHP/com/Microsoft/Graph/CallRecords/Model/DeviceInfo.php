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
namespace Microsoft\Graph\CallRecords\Model;
/**
* DeviceInfo class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class DeviceInfo extends \Microsoft\Graph\Model\Entity
{
    /**
    * Gets the captureDeviceName
    *
    * @return string|null The captureDeviceName
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
    * @return int|null The sentSignalLevel
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
    * @return float|null The speakerGlitchRate
    */
    public function getSpeakerGlitchRate()
    {
        if (array_key_exists("speakerGlitchRate", $this->_propDict)) {
            return $this->_propDict["speakerGlitchRate"];
        } else {
            return null;
        }
    }

    /**
    * Sets the speakerGlitchRate
    *
    * @param float $val The value of the speakerGlitchRate
    *
    * @return DeviceInfo
    */
    public function setSpeakerGlitchRate($val)
    {
        $this->_propDict["speakerGlitchRate"] = $val;
        return $this;
    }
}
