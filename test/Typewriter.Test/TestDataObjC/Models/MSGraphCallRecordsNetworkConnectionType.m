// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsNetworkConnectionType.h"

@interface MSGraphCallRecordsNetworkConnectionType () {
    MSGraphCallRecordsNetworkConnectionTypeValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphCallRecordsNetworkConnectionTypeValue enumValue;
@end

@implementation MSGraphCallRecordsNetworkConnectionType

+ (MSGraphCallRecordsNetworkConnectionType*) unknown {
    static MSGraphCallRecordsNetworkConnectionType *_unknown;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknown = [[MSGraphCallRecordsNetworkConnectionType alloc] init];
        _unknown.enumValue = MSGraphCallRecordsNetworkConnectionTypeUnknown;
    });
    return _unknown;
}
+ (MSGraphCallRecordsNetworkConnectionType*) wired {
    static MSGraphCallRecordsNetworkConnectionType *_wired;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _wired = [[MSGraphCallRecordsNetworkConnectionType alloc] init];
        _wired.enumValue = MSGraphCallRecordsNetworkConnectionTypeWired;
    });
    return _wired;
}

+ (MSGraphCallRecordsNetworkConnectionType*) UnknownEnumValue {
    static MSGraphCallRecordsNetworkConnectionType *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphCallRecordsNetworkConnectionType alloc] init];
        _unknownValue.enumValue = MSGraphCallRecordsNetworkConnectionTypeEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphCallRecordsNetworkConnectionType*) networkConnectionTypeWithEnumValue:(MSGraphCallRecordsNetworkConnectionTypeValue)val {

    switch(val)
    {
        case MSGraphCallRecordsNetworkConnectionTypeUnknown:
            return [MSGraphCallRecordsNetworkConnectionType unknown];
        case MSGraphCallRecordsNetworkConnectionTypeWired:
            return [MSGraphCallRecordsNetworkConnectionType wired];
        case MSGraphCallRecordsNetworkConnectionTypeEndOfEnum:
        default:
            return [MSGraphCallRecordsNetworkConnectionType UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphCallRecordsNetworkConnectionTypeUnknown:
            return @"unknown";
        case MSGraphCallRecordsNetworkConnectionTypeWired:
            return @"wired";
        case MSGraphCallRecordsNetworkConnectionTypeEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphCallRecordsNetworkConnectionTypeValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphCallRecordsNetworkConnectionType)

- (MSGraphCallRecordsNetworkConnectionType*) toMSGraphCallRecordsNetworkConnectionType{

    if([self isEqualToString:@"unknown"])
    {
          return [MSGraphCallRecordsNetworkConnectionType unknown];
    }
    else if([self isEqualToString:@"wired"])
    {
          return [MSGraphCallRecordsNetworkConnectionType wired];
    }
    else {
        return [MSGraphCallRecordsNetworkConnectionType UnknownEnumValue];
    }
}

@end
