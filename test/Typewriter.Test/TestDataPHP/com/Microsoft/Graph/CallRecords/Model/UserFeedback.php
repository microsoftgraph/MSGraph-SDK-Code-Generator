<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* UserFeedback File
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
* UserFeedback class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright © Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class UserFeedback extends \Microsoft\Graph\Model\Entity
{
    /**
    * Gets the text
    *
    * @return string The text
    */
    public function getText()
    {
        if (array_key_exists("text", $this->_propDict)) {
            return $this->_propDict["text"];
        } else {
            return null;
        }
    }

    /**
    * Sets the text
    *
    * @param string $val The value of the text
    *
    * @return UserFeedback
    */
    public function setText($val)
    {
        $this->_propDict["text"] = $val;
        return $this;
    }

    /**
    * Gets the rating
    *
    * @return UserFeedbackRating The rating
    */
    public function getRating()
    {
        if (array_key_exists("rating", $this->_propDict)) {
            if (is_a($this->_propDict["rating"], "Microsoft\Graph\CallRecords\Model\UserFeedbackRating")) {
                return $this->_propDict["rating"];
            } else {
                $this->_propDict["rating"] = new UserFeedbackRating($this->_propDict["rating"]);
                return $this->_propDict["rating"];
            }
        }
        return null;
    }

    /**
    * Sets the rating
    *
    * @param UserFeedbackRating $val The value to assign to the rating
    *
    * @return UserFeedback The UserFeedback
    */
    public function setRating($val)
    {
        $this->_propDict["rating"] = $val;
         return $this;
    }

    /**
    * Gets the tokens
    *
    * @return FeedbackTokenSet The tokens
    */
    public function getTokens()
    {
        if (array_key_exists("tokens", $this->_propDict)) {
            if (is_a($this->_propDict["tokens"], "Microsoft\Graph\CallRecords\Model\FeedbackTokenSet")) {
                return $this->_propDict["tokens"];
            } else {
                $this->_propDict["tokens"] = new FeedbackTokenSet($this->_propDict["tokens"]);
                return $this->_propDict["tokens"];
            }
        }
        return null;
    }

    /**
    * Sets the tokens
    *
    * @param FeedbackTokenSet $val The value to assign to the tokens
    *
    * @return UserFeedback The UserFeedback
    */
    public function setTokens($val)
    {
        $this->_propDict["tokens"] = $val;
         return $this;
    }
}
