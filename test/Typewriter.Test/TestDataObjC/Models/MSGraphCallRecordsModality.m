// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsModality.h"

@interface MSGraphCallRecordsModality () {
    MSGraphCallRecordsModalityValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphCallRecordsModalityValue enumValue;
@end

@implementation MSGraphCallRecordsModality

+ (MSGraphCallRecordsModality*) audio {
    static MSGraphCallRecordsModality *_audio;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _audio = [[MSGraphCallRecordsModality alloc] init];
        _audio.enumValue = MSGraphCallRecordsModalityAudio;
    });
    return _audio;
}
+ (MSGraphCallRecordsModality*) video {
    static MSGraphCallRecordsModality *_video;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _video = [[MSGraphCallRecordsModality alloc] init];
        _video.enumValue = MSGraphCallRecordsModalityVideo;
    });
    return _video;
}

+ (MSGraphCallRecordsModality*) UnknownEnumValue {
    static MSGraphCallRecordsModality *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphCallRecordsModality alloc] init];
        _unknownValue.enumValue = MSGraphCallRecordsModalityEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphCallRecordsModality*) modalityWithEnumValue:(MSGraphCallRecordsModalityValue)val {

    switch(val)
    {
        case MSGraphCallRecordsModalityAudio:
            return [MSGraphCallRecordsModality audio];
        case MSGraphCallRecordsModalityVideo:
            return [MSGraphCallRecordsModality video];
        case MSGraphCallRecordsModalityEndOfEnum:
        default:
            return [MSGraphCallRecordsModality UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphCallRecordsModalityAudio:
            return @"audio";
        case MSGraphCallRecordsModalityVideo:
            return @"video";
        case MSGraphCallRecordsModalityEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphCallRecordsModalityValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphCallRecordsModality)

- (MSGraphCallRecordsModality*) toMSGraphCallRecordsModality{

    if([self isEqualToString:@"audio"])
    {
          return [MSGraphCallRecordsModality audio];
    }
    else if([self isEqualToString:@"video"])
    {
          return [MSGraphCallRecordsModality video];
    }
    else {
        return [MSGraphCallRecordsModality UnknownEnumValue];
    }
}

@end
