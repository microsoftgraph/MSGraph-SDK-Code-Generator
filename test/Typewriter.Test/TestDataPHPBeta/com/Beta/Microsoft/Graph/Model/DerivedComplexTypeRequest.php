<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* DerivedComplexTypeRequest File
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
* DerivedComplexTypeRequest class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class DerivedComplexTypeRequest extends EmptyBaseComplexTypeRequest
{
    /**
    * Gets the property1
    *
    * @return string The property1
    */
    public function getProperty1()
    {
        if (array_key_exists("property1", $this->_propDict)) {
            return $this->_propDict["property1"];
        } else {
            return null;
        }
    }

    /**
    * Sets the property1
    *
    * @param string $val The value of the property1
    *
    * @return DerivedComplexTypeRequest
    */
    public function setProperty1($val)
    {
        $this->_propDict["property1"] = $val;
        return $this;
    }
    /**
    * Gets the property2
    *
    * @return string The property2
    */
    public function getProperty2()
    {
        if (array_key_exists("property2", $this->_propDict)) {
            return $this->_propDict["property2"];
        } else {
            return null;
        }
    }

    /**
    * Sets the property2
    *
    * @param string $val The value of the property2
    *
    * @return DerivedComplexTypeRequest
    */
    public function setProperty2($val)
    {
        $this->_propDict["property2"] = $val;
        return $this;
    }

    /**
    * Gets the enumProperty
    *
    * @return Enum1 The enumProperty
    */
    public function getEnumProperty()
    {
        if (array_key_exists("enumProperty", $this->_propDict)) {
            if (is_a($this->_propDict["enumProperty"], "\Beta\Microsoft\Graph\Model\Enum1")) {
                return $this->_propDict["enumProperty"];
            } else {
                $this->_propDict["enumProperty"] = new Enum1($this->_propDict["enumProperty"]);
                return $this->_propDict["enumProperty"];
            }
        }
        return null;
    }

    /**
    * Sets the enumProperty
    *
    * @param Enum1 $val The value to assign to the enumProperty
    *
    * @return DerivedComplexTypeRequest The DerivedComplexTypeRequest
    */
    public function setEnumProperty($val)
    {
        $this->_propDict["enumProperty"] = $val;
         return $this;
    }
}
