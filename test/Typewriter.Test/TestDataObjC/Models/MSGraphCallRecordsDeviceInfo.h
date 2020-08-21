// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.





#import "MSObject.h"

@interface MSGraphCallRecordsDeviceInfo : MSObject

@property (nullable, nonatomic, setter=setCaptureDeviceName:, getter=captureDeviceName) NSString* captureDeviceName;
@property (nonatomic, setter=setSentSignalLevel:, getter=sentSignalLevel) int32_t sentSignalLevel;
@property (nonatomic, setter=setSpeakerGlitchRate:, getter=speakerGlitchRate) double speakerGlitchRate;

@end
