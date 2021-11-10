<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* SingletonEntity1 File
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
* SingletonEntity1 class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class SingletonEntity1 extends \Beta\Microsoft\Graph\Model\Entity
{
    /**
    * Gets the testSingleNav
    *
    * @return \Beta\Microsoft\Graph\Model\TestType|null The testSingleNav
    */
    public function getTestSingleNav()
    {
        if (array_key_exists("testSingleNav", $this->_propDict) && !is_null($this->_propDict["testSingleNav"])) {
            if (is_a($this->_propDict["testSingleNav"], "\Beta\Microsoft\Graph\Model\TestType")) {
                return $this->_propDict["testSingleNav"];
            } else {
                $this->_propDict["testSingleNav"] = new \Beta\Microsoft\Graph\Model\TestType($this->_propDict["testSingleNav"]);
                return $this->_propDict["testSingleNav"];
            }
        }
        return null;
    }

    /**
    * Sets the testSingleNav
    *
    * @param \Beta\Microsoft\Graph\Model\TestType $val The testSingleNav
    *
    * @return SingletonEntity1
    */
    public function setTestSingleNav($val)
    {
        $this->_propDict["testSingleNav"] = $val;
        return $this;
    }

}
