<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* FailureInfo File
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
* FailureInfo class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class FailureInfo extends \Microsoft\Graph\Model\Entity
{

    /**
    * Gets the stage
    *
    * @return FailureStage|null The stage
    */
    public function getStage()
    {
        if (array_key_exists("stage", $this->_propDict) && !is_null($this->_propDict["stage"])) {
     
            if (is_a($this->_propDict["stage"], "\Microsoft\Graph\CallRecords\Model\FailureStage")) {
                return $this->_propDict["stage"];
            } else {
                $this->_propDict["stage"] = new FailureStage($this->_propDict["stage"]);
                return $this->_propDict["stage"];
            }
        }
        return null;
    }

    /**
    * Sets the stage
    *
    * @param FailureStage $val The value to assign to the stage
    *
    * @return FailureInfo The FailureInfo
    */
    public function setStage($val)
    {
        $this->_propDict["stage"] = $val;
         return $this;
    }
    /**
    * Gets the reason
    *
    * @return string|null The reason
    */
    public function getReason()
    {
        if (array_key_exists("reason", $this->_propDict)) {
            return $this->_propDict["reason"];
        } else {
            return null;
        }
    }

    /**
    * Sets the reason
    *
    * @param string $val The value of the reason
    *
    * @return FailureInfo
    */
    public function setReason($val)
    {
        $this->_propDict["reason"] = $val;
        return $this;
    }
}
