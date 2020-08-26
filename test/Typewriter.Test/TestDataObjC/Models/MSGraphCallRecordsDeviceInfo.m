// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsDeviceInfo()
{
    NSString* _captureDeviceName;
    int32_t _sentSignalLevel;
    double _speakerGlitchRate;
}
@end

@implementation MSGraphCallRecordsDeviceInfo

- (NSString*) captureDeviceName
{
    if([[NSNull null] isEqual:self.dictionary[@"captureDeviceName"]])
    {
        return nil;
    }   
    return self.dictionary[@"captureDeviceName"];
}

- (void) setCaptureDeviceName: (NSString*) val
{
    self.dictionary[@"captureDeviceName"] = val;
}

- (int32_t) sentSignalLevel
{
    _sentSignalLevel = [self.dictionary[@"sentSignalLevel"] intValue];
    return _sentSignalLevel;
}

- (void) setSentSignalLevel: (int32_t) val
{
    _sentSignalLevel = val;
    self.dictionary[@"sentSignalLevel"] = @(val);
}

- (double) speakerGlitchRate
{
    _speakerGlitchRate = [self.dictionary[@"speakerGlitchRate"] floatValue];
    return _speakerGlitchRate;
}

- (void) setSpeakerGlitchRate: (double) val
{
    _speakerGlitchRate = val;
    self.dictionary[@"speakerGlitchRate"] = @(val);
}

@end
