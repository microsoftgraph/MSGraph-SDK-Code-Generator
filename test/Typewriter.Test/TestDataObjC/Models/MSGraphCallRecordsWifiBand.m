// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsWifiBand.h"

@interface MSGraphCallRecordsWifiBand () {
    MSGraphCallRecordsWifiBandValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphCallRecordsWifiBandValue enumValue;
@end

@implementation MSGraphCallRecordsWifiBand

+ (MSGraphCallRecordsWifiBand*) unknown {
    static MSGraphCallRecordsWifiBand *_unknown;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknown = [[MSGraphCallRecordsWifiBand alloc] init];
        _unknown.enumValue = MSGraphCallRecordsWifiBandUnknown;
    });
    return _unknown;
}
+ (MSGraphCallRecordsWifiBand*) frequency24GHz {
    static MSGraphCallRecordsWifiBand *_frequency24GHz;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _frequency24GHz = [[MSGraphCallRecordsWifiBand alloc] init];
        _frequency24GHz.enumValue = MSGraphCallRecordsWifiBandFrequency24GHz;
    });
    return _frequency24GHz;
}

+ (MSGraphCallRecordsWifiBand*) UnknownEnumValue {
    static MSGraphCallRecordsWifiBand *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphCallRecordsWifiBand alloc] init];
        _unknownValue.enumValue = MSGraphCallRecordsWifiBandEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphCallRecordsWifiBand*) wifiBandWithEnumValue:(MSGraphCallRecordsWifiBandValue)val {

    switch(val)
    {
        case MSGraphCallRecordsWifiBandUnknown:
            return [MSGraphCallRecordsWifiBand unknown];
        case MSGraphCallRecordsWifiBandFrequency24GHz:
            return [MSGraphCallRecordsWifiBand frequency24GHz];
        case MSGraphCallRecordsWifiBandEndOfEnum:
        default:
            return [MSGraphCallRecordsWifiBand UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphCallRecordsWifiBandUnknown:
            return @"unknown";
        case MSGraphCallRecordsWifiBandFrequency24GHz:
            return @"frequency24GHz";
        case MSGraphCallRecordsWifiBandEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphCallRecordsWifiBandValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphCallRecordsWifiBand)

- (MSGraphCallRecordsWifiBand*) toMSGraphCallRecordsWifiBand{

    if([self isEqualToString:@"unknown"])
    {
          return [MSGraphCallRecordsWifiBand unknown];
    }
    else if([self isEqualToString:@"frequency24GHz"])
    {
          return [MSGraphCallRecordsWifiBand frequency24GHz];
    }
    else {
        return [MSGraphCallRecordsWifiBand UnknownEnumValue];
    }
}

@end
