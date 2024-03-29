<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* Segment File
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
* Segment class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class Segment extends \Microsoft\Graph\Model\Entity
{
    /**
    * Gets the startDateTime
    *
    * @return \DateTime|null The startDateTime
    */
    public function getStartDateTime()
    {
        if (array_key_exists("startDateTime", $this->_propDict)) {
            if (is_a($this->_propDict["startDateTime"], "\DateTime") || is_null($this->_propDict["startDateTime"])) {
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
    * @param \DateTime $val The startDateTime
    *
    * @return Segment
    */
    public function setStartDateTime($val)
    {
        $this->_propDict["startDateTime"] = $val;
        return $this;
    }

    /**
    * Gets the endDateTime
    *
    * @return \DateTime|null The endDateTime
    */
    public function getEndDateTime()
    {
        if (array_key_exists("endDateTime", $this->_propDict)) {
            if (is_a($this->_propDict["endDateTime"], "\DateTime") || is_null($this->_propDict["endDateTime"])) {
                return $this->_propDict["endDateTime"];
            } else {
                $this->_propDict["endDateTime"] = new \DateTime($this->_propDict["endDateTime"]);
                return $this->_propDict["endDateTime"];
            }
        }
        return null;
    }

    /**
    * Sets the endDateTime
    *
    * @param \DateTime $val The endDateTime
    *
    * @return Segment
    */
    public function setEndDateTime($val)
    {
        $this->_propDict["endDateTime"] = $val;
        return $this;
    }

    /**
    * Gets the caller
    *
    * @return Endpoint|null The caller
    */
    public function getCaller()
    {
        if (array_key_exists("caller", $this->_propDict)) {
            if (is_a($this->_propDict["caller"], "\Microsoft\Graph\CallRecords\Model\Endpoint") || is_null($this->_propDict["caller"])) {
                return $this->_propDict["caller"];
            } else {
                $this->_propDict["caller"] = new Endpoint($this->_propDict["caller"]);
                return $this->_propDict["caller"];
            }
        }
        return null;
    }

    /**
    * Sets the caller
    *
    * @param Endpoint $val The caller
    *
    * @return Segment
    */
    public function setCaller($val)
    {
        $this->_propDict["caller"] = $val;
        return $this;
    }

    /**
    * Gets the callee
    *
    * @return Endpoint|null The callee
    */
    public function getCallee()
    {
        if (array_key_exists("callee", $this->_propDict)) {
            if (is_a($this->_propDict["callee"], "\Microsoft\Graph\CallRecords\Model\Endpoint") || is_null($this->_propDict["callee"])) {
                return $this->_propDict["callee"];
            } else {
                $this->_propDict["callee"] = new Endpoint($this->_propDict["callee"]);
                return $this->_propDict["callee"];
            }
        }
        return null;
    }

    /**
    * Sets the callee
    *
    * @param Endpoint $val The callee
    *
    * @return Segment
    */
    public function setCallee($val)
    {
        $this->_propDict["callee"] = $val;
        return $this;
    }

    /**
    * Gets the failureInfo
    *
    * @return FailureInfo|null The failureInfo
    */
    public function getFailureInfo()
    {
        if (array_key_exists("failureInfo", $this->_propDict)) {
            if (is_a($this->_propDict["failureInfo"], "\Microsoft\Graph\CallRecords\Model\FailureInfo") || is_null($this->_propDict["failureInfo"])) {
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
    * @return Segment
    */
    public function setFailureInfo($val)
    {
        $this->_propDict["failureInfo"] = $val;
        return $this;
    }


     /**
     * Gets the media
     *
     * @return array|null The media
     */
    public function getMedia()
    {
        if (array_key_exists("media", $this->_propDict)) {
           return $this->_propDict["media"];
        } else {
            return null;
        }
    }

    /**
    * Sets the media
    *
    * @param Media[] $val The media
    *
    * @return Segment
    */
    public function setMedia($val)
    {
        $this->_propDict["media"] = $val;
        return $this;
    }


     /**
     * Gets the refTypes
     *
     * @return array|null The refTypes
     */
    public function getRefTypes()
    {
        if (array_key_exists("refTypes", $this->_propDict)) {
           return $this->_propDict["refTypes"];
        } else {
            return null;
        }
    }

    /**
    * Sets the refTypes
    *
    * @param \Microsoft\Graph\Model\EntityType3[] $val The refTypes
    *
    * @return Segment
    */
    public function setRefTypes($val)
    {
        $this->_propDict["refTypes"] = $val;
        return $this;
    }

    /**
    * Gets the refType
    *
    * @return \Microsoft\Graph\Model\Call|null The refType
    */
    public function getRefType()
    {
        if (array_key_exists("refType", $this->_propDict)) {
            if (is_a($this->_propDict["refType"], "\Microsoft\Graph\Model\Call") || is_null($this->_propDict["refType"])) {
                return $this->_propDict["refType"];
            } else {
                $this->_propDict["refType"] = new \Microsoft\Graph\Model\Call($this->_propDict["refType"]);
                return $this->_propDict["refType"];
            }
        }
        return null;
    }

    /**
    * Sets the refType
    *
    * @param \Microsoft\Graph\Model\Call $val The refType
    *
    * @return Segment
    */
    public function setRefType($val)
    {
        $this->_propDict["refType"] = $val;
        return $this;
    }

    /**
    * Gets the sessionRef
    *
    * @return Session|null The sessionRef
    */
    public function getSessionRef()
    {
        if (array_key_exists("sessionRef", $this->_propDict)) {
            if (is_a($this->_propDict["sessionRef"], "\Microsoft\Graph\CallRecords\Model\Session") || is_null($this->_propDict["sessionRef"])) {
                return $this->_propDict["sessionRef"];
            } else {
                $this->_propDict["sessionRef"] = new Session($this->_propDict["sessionRef"]);
                return $this->_propDict["sessionRef"];
            }
        }
        return null;
    }

    /**
    * Sets the sessionRef
    *
    * @param Session $val The sessionRef
    *
    * @return Segment
    */
    public function setSessionRef($val)
    {
        $this->_propDict["sessionRef"] = $val;
        return $this;
    }

    /**
    * Gets the photo
    *
    * @return Photo|null The photo
    */
    public function getPhoto()
    {
        if (array_key_exists("photo", $this->_propDict)) {
            if (is_a($this->_propDict["photo"], "\Microsoft\Graph\CallRecords\Model\Photo") || is_null($this->_propDict["photo"])) {
                return $this->_propDict["photo"];
            } else {
                $this->_propDict["photo"] = new Photo($this->_propDict["photo"]);
                return $this->_propDict["photo"];
            }
        }
        return null;
    }

    /**
    * Sets the photo
    *
    * @param Photo $val The photo
    *
    * @return Segment
    */
    public function setPhoto($val)
    {
        $this->_propDict["photo"] = $val;
        return $this;
    }

}
