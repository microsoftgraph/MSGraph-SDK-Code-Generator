// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsWifiRadioType.h"

@interface MSGraphCallRecordsWifiRadioType () {
    MSGraphCallRecordsWifiRadioTypeValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphCallRecordsWifiRadioTypeValue enumValue;
@end

@implementation MSGraphCallRecordsWifiRadioType

+ (MSGraphCallRecordsWifiRadioType*) unknown {
    static MSGraphCallRecordsWifiRadioType *_unknown;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknown = [[MSGraphCallRecordsWifiRadioType alloc] init];
        _unknown.enumValue = MSGraphCallRecordsWifiRadioTypeUnknown;
    });
    return _unknown;
}
+ (MSGraphCallRecordsWifiRadioType*) wifi80211a {
    static MSGraphCallRecordsWifiRadioType *_wifi80211a;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _wifi80211a = [[MSGraphCallRecordsWifiRadioType alloc] init];
        _wifi80211a.enumValue = MSGraphCallRecordsWifiRadioTypeWifi80211a;
    });
    return _wifi80211a;
}

+ (MSGraphCallRecordsWifiRadioType*) UnknownEnumValue {
    static MSGraphCallRecordsWifiRadioType *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphCallRecordsWifiRadioType alloc] init];
        _unknownValue.enumValue = MSGraphCallRecordsWifiRadioTypeEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphCallRecordsWifiRadioType*) wifiRadioTypeWithEnumValue:(MSGraphCallRecordsWifiRadioTypeValue)val {

    switch(val)
    {
        case MSGraphCallRecordsWifiRadioTypeUnknown:
            return [MSGraphCallRecordsWifiRadioType unknown];
        case MSGraphCallRecordsWifiRadioTypeWifi80211a:
            return [MSGraphCallRecordsWifiRadioType wifi80211a];
        case MSGraphCallRecordsWifiRadioTypeEndOfEnum:
        default:
            return [MSGraphCallRecordsWifiRadioType UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphCallRecordsWifiRadioTypeUnknown:
            return @"unknown";
        case MSGraphCallRecordsWifiRadioTypeWifi80211a:
            return @"wifi80211a";
        case MSGraphCallRecordsWifiRadioTypeEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphCallRecordsWifiRadioTypeValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphCallRecordsWifiRadioType)

- (MSGraphCallRecordsWifiRadioType*) toMSGraphCallRecordsWifiRadioType{

    if([self isEqualToString:@"unknown"])
    {
          return [MSGraphCallRecordsWifiRadioType unknown];
    }
    else if([self isEqualToString:@"wifi80211a"])
    {
          return [MSGraphCallRecordsWifiRadioType wifi80211a];
    }
    else {
        return [MSGraphCallRecordsWifiRadioType UnknownEnumValue];
    }
}

@end
