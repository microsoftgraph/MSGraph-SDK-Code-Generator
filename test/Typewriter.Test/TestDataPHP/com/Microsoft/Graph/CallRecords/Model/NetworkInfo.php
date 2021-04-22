<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* NetworkInfo File
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
* NetworkInfo class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class NetworkInfo extends \Microsoft\Graph\Model\Entity
{

    /**
    * Gets the connectionType
    *
    * @return NetworkConnectionType|null The connectionType
    */
    public function getConnectionType()
    {
        if (array_key_exists("connectionType", $this->_propDict)) {
            if (is_a($this->_propDict["connectionType"], "\Microsoft\Graph\CallRecords\Model\NetworkConnectionType") || is_null($this->_propDict["connectionType"])) {
                return $this->_propDict["connectionType"];
            } else {
                $this->_propDict["connectionType"] = new NetworkConnectionType($this->_propDict["connectionType"]);
                return $this->_propDict["connectionType"];
            }
        }
        return null;
    }

    /**
    * Sets the connectionType
    *
    * @param NetworkConnectionType $val The value to assign to the connectionType
    *
    * @return NetworkInfo The NetworkInfo
    */
    public function setConnectionType($val)
    {
        $this->_propDict["connectionType"] = $val;
         return $this;
    }

    /**
    * Gets the wifiBand
    *
    * @return WifiBand|null The wifiBand
    */
    public function getWifiBand()
    {
        if (array_key_exists("wifiBand", $this->_propDict)) {
            if (is_a($this->_propDict["wifiBand"], "\Microsoft\Graph\CallRecords\Model\WifiBand") || is_null($this->_propDict["wifiBand"])) {
                return $this->_propDict["wifiBand"];
            } else {
                $this->_propDict["wifiBand"] = new WifiBand($this->_propDict["wifiBand"]);
                return $this->_propDict["wifiBand"];
            }
        }
        return null;
    }

    /**
    * Sets the wifiBand
    *
    * @param WifiBand $val The value to assign to the wifiBand
    *
    * @return NetworkInfo The NetworkInfo
    */
    public function setWifiBand($val)
    {
        $this->_propDict["wifiBand"] = $val;
         return $this;
    }
    /**
    * Gets the basicServiceSetIdentifier
    *
    * @return string|null The basicServiceSetIdentifier
    */
    public function getBasicServiceSetIdentifier()
    {
        if (array_key_exists("basicServiceSetIdentifier", $this->_propDict)) {
            return $this->_propDict["basicServiceSetIdentifier"];
        } else {
            return null;
        }
    }

    /**
    * Sets the basicServiceSetIdentifier
    *
    * @param string $val The value of the basicServiceSetIdentifier
    *
    * @return NetworkInfo
    */
    public function setBasicServiceSetIdentifier($val)
    {
        $this->_propDict["basicServiceSetIdentifier"] = $val;
        return $this;
    }

    /**
    * Gets the wifiRadioType
    *
    * @return WifiRadioType|null The wifiRadioType
    */
    public function getWifiRadioType()
    {
        if (array_key_exists("wifiRadioType", $this->_propDict)) {
            if (is_a($this->_propDict["wifiRadioType"], "\Microsoft\Graph\CallRecords\Model\WifiRadioType") || is_null($this->_propDict["wifiRadioType"])) {
                return $this->_propDict["wifiRadioType"];
            } else {
                $this->_propDict["wifiRadioType"] = new WifiRadioType($this->_propDict["wifiRadioType"]);
                return $this->_propDict["wifiRadioType"];
            }
        }
        return null;
    }

    /**
    * Sets the wifiRadioType
    *
    * @param WifiRadioType $val The value to assign to the wifiRadioType
    *
    * @return NetworkInfo The NetworkInfo
    */
    public function setWifiRadioType($val)
    {
        $this->_propDict["wifiRadioType"] = $val;
         return $this;
    }
    /**
    * Gets the wifiSignalStrength
    *
    * @return int|null The wifiSignalStrength
    */
    public function getWifiSignalStrength()
    {
        if (array_key_exists("wifiSignalStrength", $this->_propDict)) {
            return $this->_propDict["wifiSignalStrength"];
        } else {
            return null;
        }
    }

    /**
    * Sets the wifiSignalStrength
    *
    * @param int $val The value of the wifiSignalStrength
    *
    * @return NetworkInfo
    */
    public function setWifiSignalStrength($val)
    {
        $this->_propDict["wifiSignalStrength"] = $val;
        return $this;
    }
    /**
    * Gets the bandwidthLowEventRatio
    *
    * @return float|null The bandwidthLowEventRatio
    */
    public function getBandwidthLowEventRatio()
    {
        if (array_key_exists("bandwidthLowEventRatio", $this->_propDict)) {
            return $this->_propDict["bandwidthLowEventRatio"];
        } else {
            return null;
        }
    }

    /**
    * Sets the bandwidthLowEventRatio
    *
    * @param float $val The value of the bandwidthLowEventRatio
    *
    * @return NetworkInfo
    */
    public function setBandwidthLowEventRatio($val)
    {
        $this->_propDict["bandwidthLowEventRatio"] = $val;
        return $this;
    }
}
