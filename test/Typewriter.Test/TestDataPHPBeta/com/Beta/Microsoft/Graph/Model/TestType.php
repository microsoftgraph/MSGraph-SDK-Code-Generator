<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* TestType File
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
* TestType class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class TestType extends Entity
{
    /**
    * Gets the propertyAlpha
    *
    * @return DerivedComplexTypeRequest|null The propertyAlpha
    */
    public function getPropertyAlpha()
    {
        if (array_key_exists("propertyAlpha", $this->_propDict)) {
            if (is_a($this->_propDict["propertyAlpha"], "\Beta\Microsoft\Graph\Model\DerivedComplexTypeRequest") || is_null($this->_propDict["propertyAlpha"])) {
                return $this->_propDict["propertyAlpha"];
            } else {
                $this->_propDict["propertyAlpha"] = new DerivedComplexTypeRequest($this->_propDict["propertyAlpha"]);
                return $this->_propDict["propertyAlpha"];
            }
        }
        return null;
    }

    /**
    * Sets the propertyAlpha
    *
    * @param DerivedComplexTypeRequest $val The propertyAlpha
    *
    * @return TestType
    */
    public function setPropertyAlpha($val)
    {
        $this->_propDict["propertyAlpha"] = $val;
        return $this;
    }

    /**
    * Gets the primitiveCollection
    *
    * @return array|null The primitiveCollection
    */
    public function getPrimitiveCollection()
    {
        if (array_key_exists("primitiveCollection", $this->_propDict)) {
            return $this->_propDict["primitiveCollection"];
        } else {
            return null;
        }
    }

    /**
    * Sets the primitiveCollection
    *
    * @param string[] $val The primitiveCollection
    *
    * @return TestType
    */
    public function setPrimitiveCollection($val)
    {
        $this->_propDict["primitiveCollection"] = $val;
        return $this;
    }

}
