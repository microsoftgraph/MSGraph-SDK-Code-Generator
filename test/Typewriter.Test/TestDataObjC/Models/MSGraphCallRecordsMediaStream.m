// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsMediaStream()
{
    NSString* _streamId;
    NSDate* _startDateTime;
    MSGraphCallRecordsMediaStreamDirection* _streamDirection;
    int64_t _packetUtilization;
    BOOL _wasMediaBypassed;
    double _lowVideoProcessingCapabilityRatio;
    MSDuration* _averageAudioNetworkJitter;
}
@end

@implementation MSGraphCallRecordsMediaStream

- (NSString*) streamId
{
    if([[NSNull null] isEqual:self.dictionary[@"streamId"]])
    {
        return nil;
    }   
    return self.dictionary[@"streamId"];
}

- (void) setStreamId: (NSString*) val
{
    self.dictionary[@"streamId"] = val;
}

- (NSDate*) startDateTime
{
    if(!_startDateTime){
        _startDateTime = [NSDate ms_dateFromString: self.dictionary[@"startDateTime"]];
    }
    return _startDateTime;
}

- (void) setStartDateTime: (NSDate*) val
{
    _startDateTime = val;
    self.dictionary[@"startDateTime"] = [val ms_toString];
}

- (MSGraphCallRecordsMediaStreamDirection*) streamDirection
{
    if(!_streamDirection){
        _streamDirection = [self.dictionary[@"streamDirection"] toMSGraphCallRecordsMediaStreamDirection];
    }
    return _streamDirection;
}

- (void) setStreamDirection: (MSGraphCallRecordsMediaStreamDirection*) val
{
    _streamDirection = val;
    self.dictionary[@"streamDirection"] = val;
}

- (int64_t) packetUtilization
{
    _packetUtilization = [self.dictionary[@"packetUtilization"] longLongValue];
    return _packetUtilization;
}

- (void) setPacketUtilization: (int64_t) val
{
    _packetUtilization = val;
    self.dictionary[@"packetUtilization"] = @(val);
}

- (BOOL) wasMediaBypassed
{
    _wasMediaBypassed = [self.dictionary[@"wasMediaBypassed"] boolValue];
    return _wasMediaBypassed;
}

- (void) setWasMediaBypassed: (BOOL) val
{
    _wasMediaBypassed = val;
    self.dictionary[@"wasMediaBypassed"] = @(val);
}

- (double) lowVideoProcessingCapabilityRatio
{
    _lowVideoProcessingCapabilityRatio = [self.dictionary[@"lowVideoProcessingCapabilityRatio"] floatValue];
    return _lowVideoProcessingCapabilityRatio;
}

- (void) setLowVideoProcessingCapabilityRatio: (double) val
{
    _lowVideoProcessingCapabilityRatio = val;
    self.dictionary[@"lowVideoProcessingCapabilityRatio"] = @(val);
}

- (MSDuration*) averageAudioNetworkJitter
{
    if(!_averageAudioNetworkJitter){
        _averageAudioNetworkJitter = [MSDuration ms_durationFromString: self.dictionary[@"averageAudioNetworkJitter"]];
    }
    return _averageAudioNetworkJitter;
}

- (void) setAverageAudioNetworkJitter: (MSDuration*) val
{
    _averageAudioNetworkJitter = val;
    self.dictionary[@"averageAudioNetworkJitter"] = val.durationString;
}

@end
