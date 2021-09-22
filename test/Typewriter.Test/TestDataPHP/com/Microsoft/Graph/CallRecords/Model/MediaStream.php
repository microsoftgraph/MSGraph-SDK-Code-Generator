<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* MediaStream File
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
* MediaStream class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class MediaStream extends \Microsoft\Graph\Model\Entity
{
    /**
    * Gets the streamId
    *
    * @return string|null The streamId
    */
    public function getStreamId()
    {
        if (array_key_exists("streamId", $this->_propDict)) {
            return $this->_propDict["streamId"];
        } else {
            return null;
        }
    }

    /**
    * Sets the streamId
    *
    * @param string $val The value of the streamId
    *
    * @return MediaStream
    */
    public function setStreamId($val)
    {
        $this->_propDict["streamId"] = $val;
        return $this;
    }

    /**
    * Gets the startDateTime
    *
    * @return \DateTime|null The startDateTime
    */
    public function getStartDateTime()
    {
        if (array_key_exists("startDateTime", $this->_propDict) && !is_null($this->_propDict["startDateTime"])) {
     
            if (is_a($this->_propDict["startDateTime"], "\DateTime")) {
                return $this->_propDict["startDateTime"];
            } else {
                $this->_propDict["startDateTime"] = new \DateTime($this->_propDict["startDateTime"]);
                return $this->_propDict["startDateTime"];
            }
        }
        return null;
    }

    /**
    * Sets the startDateTime
    *
    * @param \DateTime $val The value to assign to the startDateTime
    *
    * @return MediaStream The MediaStream
    */
    public function setStartDateTime($val)
    {
        $this->_propDict["startDateTime"] = $val;
         return $this;
    }

    /**
    * Gets the streamDirection
    *
    * @return MediaStreamDirection|null The streamDirection
    */
    public function getStreamDirection()
    {
        if (array_key_exists("streamDirection", $this->_propDict) && !is_null($this->_propDict["streamDirection"])) {
     
            if (is_a($this->_propDict["streamDirection"], "\Microsoft\Graph\CallRecords\Model\MediaStreamDirection")) {
                return $this->_propDict["streamDirection"];
            } else {
                $this->_propDict["streamDirection"] = new MediaStreamDirection($this->_propDict["streamDirection"]);
                return $this->_propDict["streamDirection"];
            }
        }
        return null;
    }

    /**
    * Sets the streamDirection
    *
    * @param MediaStreamDirection $val The value to assign to the streamDirection
    *
    * @return MediaStream The MediaStream
    */
    public function setStreamDirection($val)
    {
        $this->_propDict["streamDirection"] = $val;
         return $this;
    }
    /**
    * Gets the packetUtilization
    *
    * @return int|null The packetUtilization
    */
    public function getPacketUtilization()
    {
        if (array_key_exists("packetUtilization", $this->_propDict)) {
            return $this->_propDict["packetUtilization"];
        } else {
            return null;
        }
    }

    /**
    * Sets the packetUtilization
    *
    * @param int $val The value of the packetUtilization
    *
    * @return MediaStream
    */
    public function setPacketUtilization($val)
    {
        $this->_propDict["packetUtilization"] = $val;
        return $this;
    }
    /**
    * Gets the wasMediaBypassed
    *
    * @return bool|null The wasMediaBypassed
    */
    public function getWasMediaBypassed()
    {
        if (array_key_exists("wasMediaBypassed", $this->_propDict)) {
            return $this->_propDict["wasMediaBypassed"];
        } else {
            return null;
        }
    }

    /**
    * Sets the wasMediaBypassed
    *
    * @param bool $val The value of the wasMediaBypassed
    *
    * @return MediaStream
    */
    public function setWasMediaBypassed($val)
    {
        $this->_propDict["wasMediaBypassed"] = $val;
        return $this;
    }
    /**
    * Gets the lowVideoProcessingCapabilityRatio
    *
    * @return float|null The lowVideoProcessingCapabilityRatio
    */
    public function getLowVideoProcessingCapabilityRatio()
    {
        if (array_key_exists("lowVideoProcessingCapabilityRatio", $this->_propDict)) {
            return $this->_propDict["lowVideoProcessingCapabilityRatio"];
        } else {
            return null;
        }
    }

    /**
    * Sets the lowVideoProcessingCapabilityRatio
    *
    * @param float $val The value of the lowVideoProcessingCapabilityRatio
    *
    * @return MediaStream
    */
    public function setLowVideoProcessingCapabilityRatio($val)
    {
        $this->_propDict["lowVideoProcessingCapabilityRatio"] = $val;
        return $this;
    }

    /**
    * Gets the averageAudioNetworkJitter
    *
    * @return \DateInterval|null The averageAudioNetworkJitter
    */
    public function getAverageAudioNetworkJitter()
    {
        if (array_key_exists("averageAudioNetworkJitter", $this->_propDict) && !is_null($this->_propDict["averageAudioNetworkJitter"])) {
     
            if (is_a($this->_propDict["averageAudioNetworkJitter"], "\Microsoft\Graph\Model\Duration")) {
                return $this->_propDict["averageAudioNetworkJitter"];
            } else {
                $this->_propDict["averageAudioNetworkJitter"] = new \DateInterval($this->_propDict["averageAudioNetworkJitter"]);
                return $this->_propDict["averageAudioNetworkJitter"];
            }
        }
        return null;
    }

    /**
    * Sets the averageAudioNetworkJitter
    *
    * @param \DateInterval $val The value to assign to the averageAudioNetworkJitter
    *
    * @return MediaStream The MediaStream
    */
    public function setAverageAudioNetworkJitter($val)
    {
        $this->_propDict["averageAudioNetworkJitter"] = $val;
         return $this;
    }
}
