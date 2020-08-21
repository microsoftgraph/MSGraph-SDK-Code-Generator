// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsProductFamily.h"

@interface MSGraphCallRecordsProductFamily () {
    MSGraphCallRecordsProductFamilyValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphCallRecordsProductFamilyValue enumValue;
@end

@implementation MSGraphCallRecordsProductFamily

+ (MSGraphCallRecordsProductFamily*) unknown {
    static MSGraphCallRecordsProductFamily *_unknown;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknown = [[MSGraphCallRecordsProductFamily alloc] init];
        _unknown.enumValue = MSGraphCallRecordsProductFamilyUnknown;
    });
    return _unknown;
}
+ (MSGraphCallRecordsProductFamily*) teams {
    static MSGraphCallRecordsProductFamily *_teams;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _teams = [[MSGraphCallRecordsProductFamily alloc] init];
        _teams.enumValue = MSGraphCallRecordsProductFamilyTeams;
    });
    return _teams;
}

+ (MSGraphCallRecordsProductFamily*) UnknownEnumValue {
    static MSGraphCallRecordsProductFamily *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphCallRecordsProductFamily alloc] init];
        _unknownValue.enumValue = MSGraphCallRecordsProductFamilyEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphCallRecordsProductFamily*) productFamilyWithEnumValue:(MSGraphCallRecordsProductFamilyValue)val {

    switch(val)
    {
        case MSGraphCallRecordsProductFamilyUnknown:
            return [MSGraphCallRecordsProductFamily unknown];
        case MSGraphCallRecordsProductFamilyTeams:
            return [MSGraphCallRecordsProductFamily teams];
        case MSGraphCallRecordsProductFamilyEndOfEnum:
        default:
            return [MSGraphCallRecordsProductFamily UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphCallRecordsProductFamilyUnknown:
            return @"unknown";
        case MSGraphCallRecordsProductFamilyTeams:
            return @"teams";
        case MSGraphCallRecordsProductFamilyEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphCallRecordsProductFamilyValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphCallRecordsProductFamily)

- (MSGraphCallRecordsProductFamily*) toMSGraphCallRecordsProductFamily{

    if([self isEqualToString:@"unknown"])
    {
          return [MSGraphCallRecordsProductFamily unknown];
    }
    else if([self isEqualToString:@"teams"])
    {
          return [MSGraphCallRecordsProductFamily teams];
    }
    else {
        return [MSGraphCallRecordsProductFamily UnknownEnumValue];
    }
}

@end
