// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSDuration; 
#import "MSGraphCallRecordsMediaStreamDirection.h"


#import "MSObject.h"

@interface MSGraphCallRecordsMediaStream : MSObject

@property (nullable, nonatomic, setter=setStreamId:, getter=streamId) NSString* streamId;
@property (nullable, nonatomic, setter=setStartDateTime:, getter=startDateTime) NSDate* startDateTime;
@property (nonnull, nonatomic, setter=setStreamDirection:, getter=streamDirection) MSGraphCallRecordsMediaStreamDirection* streamDirection;
@property (nonatomic, setter=setPacketUtilization:, getter=packetUtilization) int64_t packetUtilization;
@property (nonatomic, setter=setWasMediaBypassed:, getter=wasMediaBypassed) BOOL wasMediaBypassed;
@property (nonatomic, setter=setLowVideoProcessingCapabilityRatio:, getter=lowVideoProcessingCapabilityRatio) double lowVideoProcessingCapabilityRatio;
@property (nullable, nonatomic, setter=setAverageAudioNetworkJitter:, getter=averageAudioNetworkJitter) MSDuration* averageAudioNetworkJitter;

@end
