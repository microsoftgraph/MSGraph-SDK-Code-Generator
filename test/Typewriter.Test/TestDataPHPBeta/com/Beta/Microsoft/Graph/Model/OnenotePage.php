<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* OnenotePage File
* PHP version 7
*
* @category  Library
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
namespace Beta\Microsoft\Graph\Model;

/**
* OnenotePage class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class OnenotePage extends Entity
{
    /**
    * Gets the content
    * The OneNotePage content.
///
/// Test token string
    *
    * @return \GuzzleHttp\Psr7\Stream|null The content
    */
    public function getContent()
    {
        if (array_key_exists("content", $this->_propDict) && !is_null($this->_propDict["content"])) {
            if (is_a($this->_propDict["content"], "\GuzzleHttp\Psr7\Stream")) {
                return $this->_propDict["content"];
            } else {
                $this->_propDict["content"] = \GuzzleHttp\Psr7\stream_for($this->_propDict["content"]);
                return $this->_propDict["content"];
            }
        }
        return null;
    }
    
    /**
    * Sets the content
    * The OneNotePage content.
///
/// Test token string
    *
    * @param \GuzzleHttp\Psr7\Stream $val The content
    *
    * @return OnenotePage
    */
    public function setContent($val)
    {
        $this->_propDict["content"] = $val;
        return $this;
    }
    
}
