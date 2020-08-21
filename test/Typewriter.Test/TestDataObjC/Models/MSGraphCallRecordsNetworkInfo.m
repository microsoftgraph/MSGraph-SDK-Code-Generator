// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsNetworkInfo()
{
    MSGraphCallRecordsNetworkConnectionType* _connectionType;
    MSGraphCallRecordsWifiBand* _wifiBand;
    NSString* _basicServiceSetIdentifier;
    MSGraphCallRecordsWifiRadioType* _wifiRadioType;
    int32_t _wifiSignalStrength;
    double _bandwidthLowEventRatio;
}
@end

@implementation MSGraphCallRecordsNetworkInfo

- (MSGraphCallRecordsNetworkConnectionType*) connectionType
{
    if(!_connectionType){
        _connectionType = [self.dictionary[@"connectionType"] toMSGraphCallRecordsNetworkConnectionType];
    }
    return _connectionType;
}

- (void) setConnectionType: (MSGraphCallRecordsNetworkConnectionType*) val
{
    _connectionType = val;
    self.dictionary[@"connectionType"] = val;
}

- (MSGraphCallRecordsWifiBand*) wifiBand
{
    if(!_wifiBand){
        _wifiBand = [self.dictionary[@"wifiBand"] toMSGraphCallRecordsWifiBand];
    }
    return _wifiBand;
}

- (void) setWifiBand: (MSGraphCallRecordsWifiBand*) val
{
    _wifiBand = val;
    self.dictionary[@"wifiBand"] = val;
}

- (NSString*) basicServiceSetIdentifier
{
    if([[NSNull null] isEqual:self.dictionary[@"basicServiceSetIdentifier"]])
    {
        return nil;
    }   
    return self.dictionary[@"basicServiceSetIdentifier"];
}

- (void) setBasicServiceSetIdentifier: (NSString*) val
{
    self.dictionary[@"basicServiceSetIdentifier"] = val;
}

- (MSGraphCallRecordsWifiRadioType*) wifiRadioType
{
    if(!_wifiRadioType){
        _wifiRadioType = [self.dictionary[@"wifiRadioType"] toMSGraphCallRecordsWifiRadioType];
    }
    return _wifiRadioType;
}

- (void) setWifiRadioType: (MSGraphCallRecordsWifiRadioType*) val
{
    _wifiRadioType = val;
    self.dictionary[@"wifiRadioType"] = val;
}

- (int32_t) wifiSignalStrength
{
    _wifiSignalStrength = [self.dictionary[@"wifiSignalStrength"] intValue];
    return _wifiSignalStrength;
}

- (void) setWifiSignalStrength: (int32_t) val
{
    _wifiSignalStrength = val;
    self.dictionary[@"wifiSignalStrength"] = @(val);
}

- (double) bandwidthLowEventRatio
{
    _bandwidthLowEventRatio = [self.dictionary[@"bandwidthLowEventRatio"] floatValue];
    return _bandwidthLowEventRatio;
}

- (void) setBandwidthLowEventRatio: (double) val
{
    _bandwidthLowEventRatio = val;
    self.dictionary[@"bandwidthLowEventRatio"] = @(val);
}

@end
