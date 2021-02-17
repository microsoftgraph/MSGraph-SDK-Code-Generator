// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphCallRecordsWifiRadioTypeValue){

	MSGraphCallRecordsWifiRadioTypeUnknown = 0,
	MSGraphCallRecordsWifiRadioTypeWifi80211a = 1,
    MSGraphCallRecordsWifiRadioTypeEndOfEnum
};

@interface MSGraphCallRecordsWifiRadioType : NSObject

+(MSGraphCallRecordsWifiRadioType*) unknown;
+(MSGraphCallRecordsWifiRadioType*) wifi80211a;

+(MSGraphCallRecordsWifiRadioType*) UnknownEnumValue;

+(MSGraphCallRecordsWifiRadioType*) wifiRadioTypeWithEnumValue:(MSGraphCallRecordsWifiRadioTypeValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphCallRecordsWifiRadioTypeValue enumValue;

@end


@interface NSString (MSGraphCallRecordsWifiRadioType)

- (MSGraphCallRecordsWifiRadioType*) toMSGraphCallRecordsWifiRadioType;

@end
