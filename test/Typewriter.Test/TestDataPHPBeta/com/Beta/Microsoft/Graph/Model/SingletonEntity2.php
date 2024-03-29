<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* SingletonEntity2 File
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
* SingletonEntity2 class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class SingletonEntity2 extends Entity
{
    /**
    * Gets the testSingleNav2
    *
    * @return EntityType3|null The testSingleNav2
    */
    public function getTestSingleNav2()
    {
        if (array_key_exists("testSingleNav2", $this->_propDict)) {
            if (is_a($this->_propDict["testSingleNav2"], "\Beta\Microsoft\Graph\Model\EntityType3") || is_null($this->_propDict["testSingleNav2"])) {
                return $this->_propDict["testSingleNav2"];
            } else {
                $this->_propDict["testSingleNav2"] = new EntityType3($this->_propDict["testSingleNav2"]);
                return $this->_propDict["testSingleNav2"];
            }
        }
        return null;
    }

    /**
    * Sets the testSingleNav2
    *
    * @param EntityType3 $val The testSingleNav2
    *
    * @return SingletonEntity2
    */
    public function setTestSingleNav2($val)
    {
        $this->_propDict["testSingleNav2"] = $val;
        return $this;
    }

}
