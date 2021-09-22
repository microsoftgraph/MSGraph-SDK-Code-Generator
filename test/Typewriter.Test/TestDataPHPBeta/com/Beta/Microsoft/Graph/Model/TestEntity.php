<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* TestEntity File
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
* TestEntity class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class TestEntity extends Entity
{
    /**
    * Gets the testNav
    *
    * @return TestType|null The testNav
    */
    public function getTestNav()
    {
        if (array_key_exists("testNav", $this->_propDict)) {
            if (is_a($this->_propDict["testNav"], "\Beta\Microsoft\Graph\Model\TestType") || is_null($this->_propDict["testNav"])) {
                return $this->_propDict["testNav"];
            } else {
                $this->_propDict["testNav"] = new TestType($this->_propDict["testNav"]);
                return $this->_propDict["testNav"];
            }
        }
        return null;
    }
    
    /**
    * Sets the testNav
    *
    * @param TestType $val The testNav
    *
    * @return TestEntity
    */
    public function setTestNav($val)
    {
        $this->_propDict["testNav"] = $val;
        return $this;
    }
    
    /**
    * Gets the testInvalidNav
    *
    * @return EntityType2|null The testInvalidNav
    */
    public function getTestInvalidNav()
    {
        if (array_key_exists("testInvalidNav", $this->_propDict)) {
            if (is_a($this->_propDict["testInvalidNav"], "\Beta\Microsoft\Graph\Model\EntityType2") || is_null($this->_propDict["testInvalidNav"])) {
                return $this->_propDict["testInvalidNav"];
            } else {
                $this->_propDict["testInvalidNav"] = new EntityType2($this->_propDict["testInvalidNav"]);
                return $this->_propDict["testInvalidNav"];
            }
        }
        return null;
    }
    
    /**
    * Sets the testInvalidNav
    *
    * @param EntityType2 $val The testInvalidNav
    *
    * @return TestEntity
    */
    public function setTestInvalidNav($val)
    {
        $this->_propDict["testInvalidNav"] = $val;
        return $this;
    }
    
    /**
    * Gets the testExplicitNav
    *
    * @return EntityType3|null The testExplicitNav
    */
    public function getTestExplicitNav()
    {
        if (array_key_exists("testExplicitNav", $this->_propDict)) {
            if (is_a($this->_propDict["testExplicitNav"], "\Beta\Microsoft\Graph\Model\EntityType3") || is_null($this->_propDict["testExplicitNav"])) {
                return $this->_propDict["testExplicitNav"];
            } else {
                $this->_propDict["testExplicitNav"] = new EntityType3($this->_propDict["testExplicitNav"]);
                return $this->_propDict["testExplicitNav"];
            }
        }
        return null;
    }
    
    /**
    * Sets the testExplicitNav
    *
    * @param EntityType3 $val The testExplicitNav
    *
    * @return TestEntity
    */
    public function setTestExplicitNav($val)
    {
        $this->_propDict["testExplicitNav"] = $val;
        return $this;
    }
    
}
