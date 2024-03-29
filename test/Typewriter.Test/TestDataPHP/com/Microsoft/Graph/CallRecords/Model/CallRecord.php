<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* CallRecord File
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
* CallRecord class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class CallRecord extends \Microsoft\Graph\Model\Entity
{
    /**
    * Gets the version
    *
    * @return int|null The version
    */
    public function getVersion()
    {
        if (array_key_exists("version", $this->_propDict)) {
            return $this->_propDict["version"];
        } else {
            return null;
        }
    }

    /**
    * Sets the version
    *
    * @param int $val The version
    *
    * @return CallRecord
    */
    public function setVersion($val)
    {
        $this->_propDict["version"] = intval($val);
        return $this;
    }

    /**
    * Gets the type
    *
    * @return CallType|null The type
    */
    public function getType()
    {
        if (array_key_exists("type", $this->_propDict)) {
            if (is_a($this->_propDict["type"], "\Microsoft\Graph\CallRecords\Model\CallType") || is_null($this->_propDict["type"])) {
                return $this->_propDict["type"];
            } else {
                $this->_propDict["type"] = new CallType($this->_propDict["type"]);
                return $this->_propDict["type"];
            }
        }
        return null;
    }

    /**
    * Sets the type
    *
    * @param CallType $val The type
    *
    * @return CallRecord
    */
    public function setType($val)
    {
        $this->_propDict["type"] = $val;
        return $this;
    }


     /**
     * Gets the modalities
     *
     * @return array|null The modalities
     */
    public function getModalities()
    {
        if (array_key_exists("modalities", $this->_propDict)) {
           return $this->_propDict["modalities"];
        } else {
            return null;
        }
    }

    /**
    * Sets the modalities
    *
    * @param Modality[] $val The modalities
    *
    * @return CallRecord
    */
    public function setModalities($val)
    {
        $this->_propDict["modalities"] = $val;
        return $this;
    }

    /**
    * Gets the lastModifiedDateTime
    *
    * @return \DateTime|null The lastModifiedDateTime
    */
    public function getLastModifiedDateTime()
    {
        if (array_key_exists("lastModifiedDateTime", $this->_propDict)) {
            if (is_a($this->_propDict["lastModifiedDateTime"], "\DateTime") || is_null($this->_propDict["lastModifiedDateTime"])) {
                return $this->_propDict["lastModifiedDateTime"];
            } else {
                $this->_propDict["lastModifiedDateTime"] = new \DateTime($this->_propDict["lastModifiedDateTime"]);
                return $this->_propDict["lastModifiedDateTime"];
            }
        }
        return null;
    }

    /**
    * Sets the lastModifiedDateTime
    *
    * @param \DateTime $val The lastModifiedDateTime
    *
    * @return CallRecord
    */
    public function setLastModifiedDateTime($val)
    {
        $this->_propDict["lastModifiedDateTime"] = $val;
        return $this;
    }

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
    * @return CallRecord
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
    * @return CallRecord
    */
    public function setEndDateTime($val)
    {
        $this->_propDict["endDateTime"] = $val;
        return $this;
    }

    /**
    * Gets the organizer
    *
    * @return \Microsoft\Graph\Model\IdentitySet|null The organizer
    */
    public function getOrganizer()
    {
        if (array_key_exists("organizer", $this->_propDict)) {
            if (is_a($this->_propDict["organizer"], "\Microsoft\Graph\Model\IdentitySet") || is_null($this->_propDict["organizer"])) {
                return $this->_propDict["organizer"];
            } else {
                $this->_propDict["organizer"] = new \Microsoft\Graph\Model\IdentitySet($this->_propDict["organizer"]);
                return $this->_propDict["organizer"];
            }
        }
        return null;
    }

    /**
    * Sets the organizer
    *
    * @param \Microsoft\Graph\Model\IdentitySet $val The organizer
    *
    * @return CallRecord
    */
    public function setOrganizer($val)
    {
        $this->_propDict["organizer"] = $val;
        return $this;
    }


     /**
     * Gets the participants
     *
     * @return array|null The participants
     */
    public function getParticipants()
    {
        if (array_key_exists("participants", $this->_propDict)) {
           return $this->_propDict["participants"];
        } else {
            return null;
        }
    }

    /**
    * Sets the participants
    *
    * @param \Microsoft\Graph\Model\IdentitySet[] $val The participants
    *
    * @return CallRecord
    */
    public function setParticipants($val)
    {
        $this->_propDict["participants"] = $val;
        return $this;
    }

    /**
    * Gets the joinWebUrl
    *
    * @return string|null The joinWebUrl
    */
    public function getJoinWebUrl()
    {
        if (array_key_exists("joinWebUrl", $this->_propDict)) {
            return $this->_propDict["joinWebUrl"];
        } else {
            return null;
        }
    }

    /**
    * Sets the joinWebUrl
    *
    * @param string $val The joinWebUrl
    *
    * @return CallRecord
    */
    public function setJoinWebUrl($val)
    {
        $this->_propDict["joinWebUrl"] = $val;
        return $this;
    }


     /**
     * Gets the sessions
     *
     * @return array|null The sessions
     */
    public function getSessions()
    {
        if (array_key_exists("sessions", $this->_propDict)) {
           return $this->_propDict["sessions"];
        } else {
            return null;
        }
    }

    /**
    * Sets the sessions
    *
    * @param Session[] $val The sessions
    *
    * @return CallRecord
    */
    public function setSessions($val)
    {
        $this->_propDict["sessions"] = $val;
        return $this;
    }


     /**
     * Gets the recipients
     *
     * @return array|null The recipients
     */
    public function getRecipients()
    {
        if (array_key_exists("recipients", $this->_propDict)) {
           return $this->_propDict["recipients"];
        } else {
            return null;
        }
    }

    /**
    * Sets the recipients
    *
    * @param \Microsoft\Graph\Model\EntityType2[] $val The recipients
    *
    * @return CallRecord
    */
    public function setRecipients($val)
    {
        $this->_propDict["recipients"] = $val;
        return $this;
    }

}
