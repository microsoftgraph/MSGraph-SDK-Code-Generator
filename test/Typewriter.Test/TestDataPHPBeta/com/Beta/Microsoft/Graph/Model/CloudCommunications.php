<?php
/**
* Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
* 
* CloudCommunications File
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
* CloudCommunications class
*
* @category  Model
* @package   Microsoft.Graph
* @copyright (c) Microsoft Corporation. All rights reserved.
* @license   https://opensource.org/licenses/MIT MIT License
* @link      https://graph.microsoft.com
*/
class CloudCommunications extends Entity
{

     /**
     * Gets the calls
     *
     * @return Call[]|null The calls
     */
    public function getCalls()
    {
        if (array_key_exists('calls', $this->_propDict) && !is_null($this->_propDict['calls'])) {
            $calls = [];
            if (count($this->_propDict['calls']) > 0 && is_a($this->_propDict['calls'][0], 'Call')) {
                return $this->_propDict['calls'];
            }
            foreach ($this->_propDict['calls'] as $singleValue) {
                $calls []= new Call($singleValue);
            }
            $this->_propDict['calls'] = $calls;
            return $this->_propDict['calls'];
        }
        return null;
    }

    /**
    * Sets the calls
    *
    * @param Call[] $val The calls
    *
    * @return CloudCommunications
    */
    public function setCalls($val)
    {
        $this->_propDict["calls"] = $val;
        return $this;
    }


     /**
     * Gets the callRecords
     *
     * @return \Beta\Microsoft\Graph\CallRecords\Model\CallRecord[]|null The callRecords
     */
    public function getCallRecords()
    {
        if (array_key_exists('callRecords', $this->_propDict) && !is_null($this->_propDict['callRecords'])) {
            $callRecords = [];
            if (count($this->_propDict['callRecords']) > 0 && is_a($this->_propDict['callRecords'][0], '\Beta\Microsoft\Graph\CallRecords\Model\CallRecord')) {
                return $this->_propDict['callRecords'];
            }
            foreach ($this->_propDict['callRecords'] as $singleValue) {
                $callRecords []= new \Beta\Microsoft\Graph\CallRecords\Model\CallRecord($singleValue);
            }
            $this->_propDict['callRecords'] = $callRecords;
            return $this->_propDict['callRecords'];
        }
        return null;
    }

    /**
    * Sets the callRecords
    *
    * @param \Beta\Microsoft\Graph\CallRecords\Model\CallRecord[] $val The callRecords
    *
    * @return CloudCommunications
    */
    public function setCallRecords($val)
    {
        $this->_propDict["callRecords"] = $val;
        return $this;
    }

}
