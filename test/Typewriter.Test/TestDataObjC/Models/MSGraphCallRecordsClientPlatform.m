// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsClientPlatform.h"

@interface MSGraphCallRecordsClientPlatform () {
    MSGraphCallRecordsClientPlatformValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphCallRecordsClientPlatformValue enumValue;
@end

@implementation MSGraphCallRecordsClientPlatform

+ (MSGraphCallRecordsClientPlatform*) unknown {
    static MSGraphCallRecordsClientPlatform *_unknown;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknown = [[MSGraphCallRecordsClientPlatform alloc] init];
        _unknown.enumValue = MSGraphCallRecordsClientPlatformUnknown;
    });
    return _unknown;
}
+ (MSGraphCallRecordsClientPlatform*) windows {
    static MSGraphCallRecordsClientPlatform *_windows;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _windows = [[MSGraphCallRecordsClientPlatform alloc] init];
        _windows.enumValue = MSGraphCallRecordsClientPlatformWindows;
    });
    return _windows;
}

+ (MSGraphCallRecordsClientPlatform*) UnknownEnumValue {
    static MSGraphCallRecordsClientPlatform *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphCallRecordsClientPlatform alloc] init];
        _unknownValue.enumValue = MSGraphCallRecordsClientPlatformEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphCallRecordsClientPlatform*) clientPlatformWithEnumValue:(MSGraphCallRecordsClientPlatformValue)val {

    switch(val)
    {
        case MSGraphCallRecordsClientPlatformUnknown:
            return [MSGraphCallRecordsClientPlatform unknown];
        case MSGraphCallRecordsClientPlatformWindows:
            return [MSGraphCallRecordsClientPlatform windows];
        case MSGraphCallRecordsClientPlatformEndOfEnum:
        default:
            return [MSGraphCallRecordsClientPlatform UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphCallRecordsClientPlatformUnknown:
            return @"unknown";
        case MSGraphCallRecordsClientPlatformWindows:
            return @"windows";
        case MSGraphCallRecordsClientPlatformEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphCallRecordsClientPlatformValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphCallRecordsClientPlatform)

- (MSGraphCallRecordsClientPlatform*) toMSGraphCallRecordsClientPlatform{

    if([self isEqualToString:@"unknown"])
    {
          return [MSGraphCallRecordsClientPlatform unknown];
    }
    else if([self isEqualToString:@"windows"])
    {
          return [MSGraphCallRecordsClientPlatform windows];
    }
    else {
        return [MSGraphCallRecordsClientPlatform UnknownEnumValue];
    }
}

@end
