// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsNetworkConnectionType.h"
#import "MSGraphCallRecordsWifiBand.h"
#import "MSGraphCallRecordsWifiRadioType.h"


#import "MSObject.h"

@interface MSGraphCallRecordsNetworkInfo : MSObject

@property (nonnull, nonatomic, setter=setConnectionType:, getter=connectionType) MSGraphCallRecordsNetworkConnectionType* connectionType;
@property (nonnull, nonatomic, setter=setWifiBand:, getter=wifiBand) MSGraphCallRecordsWifiBand* wifiBand;
@property (nullable, nonatomic, setter=setBasicServiceSetIdentifier:, getter=basicServiceSetIdentifier) NSString* basicServiceSetIdentifier;
@property (nonnull, nonatomic, setter=setWifiRadioType:, getter=wifiRadioType) MSGraphCallRecordsWifiRadioType* wifiRadioType;
@property (nonatomic, setter=setWifiSignalStrength:, getter=wifiSignalStrength) int32_t wifiSignalStrength;
@property (nonatomic, setter=setBandwidthLowEventRatio:, getter=bandwidthLowEventRatio) double bandwidthLowEventRatio;

@end
