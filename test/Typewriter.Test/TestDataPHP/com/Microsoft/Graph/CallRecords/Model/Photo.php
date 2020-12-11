<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* Photo File
* PHP version 7
*
* @category  Library
* @package   Microsoft.Graph
* @copyright © Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
namespace Microsoft\Graph\CallRecords\Model;

/**
* Photo class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class Photo extends \Microsoft\Graph\Model\Entity
{
    /**
    * Gets the failureInfo
    *
    * @return FailureInfo The failureInfo
    */
    public function getFailureInfo()
    {
        if (array_key_exists("failureInfo", $this->_propDict)) {
            if (is_a($this->_propDict["failureInfo"], "Microsoft\Graph\CallRecords\Model\FailureInfo")) {
                return $this->_propDict["failureInfo"];
            } else {
                $this->_propDict["failureInfo"] = new FailureInfo($this->_propDict["failureInfo"]);
                return $this->_propDict["failureInfo"];
            }
        }
        return null;
    }
    
    /**
    * Sets the failureInfo
    *
    * @param FailureInfo $val The failureInfo
    *
    * @return Photo
    */
    public function setFailureInfo($val)
    {
        $this->_propDict["failureInfo"] = $val;
        return $this;
    }
    
    /**
    * Gets the option
    *
    * @return Option The option
    */
    public function getOption()
    {
        if (array_key_exists("option", $this->_propDict)) {
            if (is_a($this->_propDict["option"], "Microsoft\Graph\CallRecords\Model\Option")) {
                return $this->_propDict["option"];
            } else {
                $this->_propDict["option"] = new Option($this->_propDict["option"]);
                return $this->_propDict["option"];
            }
        }
        return null;
    }
    
    /**
    * Sets the option
    *
    * @param Option $val The option
    *
    * @return Photo
    */
    public function setOption($val)
    {
        $this->_propDict["option"] = $val;
        return $this;
    }
    
}