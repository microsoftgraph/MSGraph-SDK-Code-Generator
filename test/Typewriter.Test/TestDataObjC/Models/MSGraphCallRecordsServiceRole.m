// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsServiceRole.h"

@interface MSGraphCallRecordsServiceRole () {
    MSGraphCallRecordsServiceRoleValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphCallRecordsServiceRoleValue enumValue;
@end

@implementation MSGraphCallRecordsServiceRole

+ (MSGraphCallRecordsServiceRole*) unknown {
    static MSGraphCallRecordsServiceRole *_unknown;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknown = [[MSGraphCallRecordsServiceRole alloc] init];
        _unknown.enumValue = MSGraphCallRecordsServiceRoleUnknown;
    });
    return _unknown;
}
+ (MSGraphCallRecordsServiceRole*) customBot {
    static MSGraphCallRecordsServiceRole *_customBot;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _customBot = [[MSGraphCallRecordsServiceRole alloc] init];
        _customBot.enumValue = MSGraphCallRecordsServiceRoleCustomBot;
    });
    return _customBot;
}

+ (MSGraphCallRecordsServiceRole*) UnknownEnumValue {
    static MSGraphCallRecordsServiceRole *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphCallRecordsServiceRole alloc] init];
        _unknownValue.enumValue = MSGraphCallRecordsServiceRoleEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphCallRecordsServiceRole*) serviceRoleWithEnumValue:(MSGraphCallRecordsServiceRoleValue)val {

    switch(val)
    {
        case MSGraphCallRecordsServiceRoleUnknown:
            return [MSGraphCallRecordsServiceRole unknown];
        case MSGraphCallRecordsServiceRoleCustomBot:
            return [MSGraphCallRecordsServiceRole customBot];
        case MSGraphCallRecordsServiceRoleEndOfEnum:
        default:
            return [MSGraphCallRecordsServiceRole UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphCallRecordsServiceRoleUnknown:
            return @"unknown";
        case MSGraphCallRecordsServiceRoleCustomBot:
            return @"customBot";
        case MSGraphCallRecordsServiceRoleEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphCallRecordsServiceRoleValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphCallRecordsServiceRole)

- (MSGraphCallRecordsServiceRole*) toMSGraphCallRecordsServiceRole{

    if([self isEqualToString:@"unknown"])
    {
          return [MSGraphCallRecordsServiceRole unknown];
    }
    else if([self isEqualToString:@"customBot"])
    {
          return [MSGraphCallRecordsServiceRole customBot];
    }
    else {
        return [MSGraphCallRecordsServiceRole UnknownEnumValue];
    }
}

@end
