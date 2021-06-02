<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* ClientUserAgent File
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
* ClientUserAgent class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class ClientUserAgent extends UserAgent
{

    /**
    * Gets the platform
    *
    * @return ClientPlatform|null The platform
    */
    public function getPlatform()
    {
        if (array_key_exists("platform", $this->_propDict) && !is_null($this->_propDict["platform"])) {
     
            if (is_a($this->_propDict["platform"], "\Microsoft\Graph\CallRecords\Model\ClientPlatform")) {
                return $this->_propDict["platform"];
            } else {
                $this->_propDict["platform"] = new ClientPlatform($this->_propDict["platform"]);
                return $this->_propDict["platform"];
            }
        }
        return null;
    }

    /**
    * Sets the platform
    *
    * @param ClientPlatform $val The value to assign to the platform
    *
    * @return ClientUserAgent The ClientUserAgent
    */
    public function setPlatform($val)
    {
        $this->_propDict["platform"] = $val;
         return $this;
    }

    /**
    * Gets the productFamily
    *
    * @return ProductFamily|null The productFamily
    */
    public function getProductFamily()
    {
        if (array_key_exists("productFamily", $this->_propDict) && !is_null($this->_propDict["productFamily"])) {
     
            if (is_a($this->_propDict["productFamily"], "\Microsoft\Graph\CallRecords\Model\ProductFamily")) {
                return $this->_propDict["productFamily"];
            } else {
                $this->_propDict["productFamily"] = new ProductFamily($this->_propDict["productFamily"]);
                return $this->_propDict["productFamily"];
            }
        }
        return null;
    }

    /**
    * Sets the productFamily
    *
    * @param ProductFamily $val The value to assign to the productFamily
    *
    * @return ClientUserAgent The ClientUserAgent
    */
    public function setProductFamily($val)
    {
        $this->_propDict["productFamily"] = $val;
         return $this;
    }
}
