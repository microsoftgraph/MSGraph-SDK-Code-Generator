<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* Media File
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
* Media class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class Media extends \Beta\Microsoft\Graph\Model\Entity
{
    /**
    * Gets the label
    *
    * @return string|null The label
    */
    public function getLabel()
    {
        if (array_key_exists("label", $this->_propDict)) {
            return $this->_propDict["label"];
        } else {
            return null;
        }
    }

    /**
    * Sets the label
    *
    * @param string $val The value of the label
    *
    * @return Media
    */
    public function setLabel($val)
    {
        $this->_propDict["label"] = $val;
        return $this;
    }

    /**
    * Gets the callerNetwork
    *
    * @return NetworkInfo|null The callerNetwork
    */
    public function getCallerNetwork()
    {
        if (array_key_exists("callerNetwork", $this->_propDict) && !is_null($this->_propDict["callerNetwork"])) {
     
            if (is_a($this->_propDict["callerNetwork"], "\Beta\Microsoft\Graph\CallRecords\Model\NetworkInfo")) {
                return $this->_propDict["callerNetwork"];
            } else {
                $this->_propDict["callerNetwork"] = new NetworkInfo($this->_propDict["callerNetwork"]);
                return $this->_propDict["callerNetwork"];
            }
        }
        return null;
    }

    /**
    * Sets the callerNetwork
    *
    * @param NetworkInfo $val The value to assign to the callerNetwork
    *
    * @return Media The Media
    */
    public function setCallerNetwork($val)
    {
        $this->_propDict["callerNetwork"] = $val;
         return $this;
    }

    /**
    * Gets the callerDevice
    *
    * @return DeviceInfo|null The callerDevice
    */
    public function getCallerDevice()
    {
        if (array_key_exists("callerDevice", $this->_propDict) && !is_null($this->_propDict["callerDevice"])) {
     
            if (is_a($this->_propDict["callerDevice"], "\Beta\Microsoft\Graph\CallRecords\Model\DeviceInfo")) {
                return $this->_propDict["callerDevice"];
            } else {
                $this->_propDict["callerDevice"] = new DeviceInfo($this->_propDict["callerDevice"]);
                return $this->_propDict["callerDevice"];
            }
        }
        return null;
    }

    /**
    * Sets the callerDevice
    *
    * @param DeviceInfo $val The value to assign to the callerDevice
    *
    * @return Media The Media
    */
    public function setCallerDevice($val)
    {
        $this->_propDict["callerDevice"] = $val;
         return $this;
    }

    /**
    * Gets the streams
    *
    * @return MediaStream[]|null The streams
    */
    public function getStreams()
    {
        if (array_key_exists("streams", $this->_propDict) && !is_null($this->_propDict["streams"])) {
       
            if (count($this->_propDict['streams']) === 0) {
              return $this->_propDict['streams'];
            }
            if (is_a($this->_propDict['streams'][0], ' MediaStream')) {
               return $this->_propDict['streams'];
            }
            $streams = [];
            foreach ($this->_propDict['streams'] as $singleValue) {
               $streams []= new MediaStream($singleValue);
            }
            $this->_propDict['streams'] = $streams;
            return $this->_propDict['streams'];
            }
        return null;
    }

    /**
    * Sets the streams
    *
    * @param MediaStream[] $val The value to assign to the streams
    *
    * @return Media The Media
    */
    public function setStreams($val)
    {
        $this->_propDict["streams"] = $val;
         return $this;
    }
}
