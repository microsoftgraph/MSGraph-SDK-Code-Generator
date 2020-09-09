// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsMediaStreamDirection.h"

@interface MSGraphCallRecordsMediaStreamDirection () {
    MSGraphCallRecordsMediaStreamDirectionValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphCallRecordsMediaStreamDirectionValue enumValue;
@end

@implementation MSGraphCallRecordsMediaStreamDirection

+ (MSGraphCallRecordsMediaStreamDirection*) callerToCallee {
    static MSGraphCallRecordsMediaStreamDirection *_callerToCallee;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _callerToCallee = [[MSGraphCallRecordsMediaStreamDirection alloc] init];
        _callerToCallee.enumValue = MSGraphCallRecordsMediaStreamDirectionCallerToCallee;
    });
    return _callerToCallee;
}
+ (MSGraphCallRecordsMediaStreamDirection*) calleeToCaller {
    static MSGraphCallRecordsMediaStreamDirection *_calleeToCaller;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _calleeToCaller = [[MSGraphCallRecordsMediaStreamDirection alloc] init];
        _calleeToCaller.enumValue = MSGraphCallRecordsMediaStreamDirectionCalleeToCaller;
    });
    return _calleeToCaller;
}

+ (MSGraphCallRecordsMediaStreamDirection*) UnknownEnumValue {
    static MSGraphCallRecordsMediaStreamDirection *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphCallRecordsMediaStreamDirection alloc] init];
        _unknownValue.enumValue = MSGraphCallRecordsMediaStreamDirectionEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphCallRecordsMediaStreamDirection*) mediaStreamDirectionWithEnumValue:(MSGraphCallRecordsMediaStreamDirectionValue)val {

    switch(val)
    {
        case MSGraphCallRecordsMediaStreamDirectionCallerToCallee:
            return [MSGraphCallRecordsMediaStreamDirection callerToCallee];
        case MSGraphCallRecordsMediaStreamDirectionCalleeToCaller:
            return [MSGraphCallRecordsMediaStreamDirection calleeToCaller];
        case MSGraphCallRecordsMediaStreamDirectionEndOfEnum:
        default:
            return [MSGraphCallRecordsMediaStreamDirection UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphCallRecordsMediaStreamDirectionCallerToCallee:
            return @"callerToCallee";
        case MSGraphCallRecordsMediaStreamDirectionCalleeToCaller:
            return @"calleeToCaller";
        case MSGraphCallRecordsMediaStreamDirectionEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphCallRecordsMediaStreamDirectionValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphCallRecordsMediaStreamDirection)

- (MSGraphCallRecordsMediaStreamDirection*) toMSGraphCallRecordsMediaStreamDirection{

    if([self isEqualToString:@"callerToCallee"])
    {
          return [MSGraphCallRecordsMediaStreamDirection callerToCallee];
    }
    else if([self isEqualToString:@"calleeToCaller"])
    {
          return [MSGraphCallRecordsMediaStreamDirection calleeToCaller];
    }
    else {
        return [MSGraphCallRecordsMediaStreamDirection UnknownEnumValue];
    }
}

@end
