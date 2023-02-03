// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphEmptyEnum.h"

@interface MSGraphEmptyEnum () {
    MSGraphEmptyEnumValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphEmptyEnumValue enumValue;
@end

@implementation MSGraphEmptyEnum


+ (MSGraphEmptyEnum*) UnknownEnumValue {
    static MSGraphEmptyEnum *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphEmptyEnum alloc] init];
        _unknownValue.enumValue = MSGraphEmptyEnumEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphEmptyEnum*) emptyEnumWithEnumValue:(MSGraphEmptyEnumValue)val {

    switch(val)
    {
        case MSGraphEmptyEnumEndOfEnum:
        default:
            return [MSGraphEmptyEnum UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphEmptyEnumEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphEmptyEnumValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphEmptyEnum)

- (MSGraphEmptyEnum*) toMSGraphEmptyEnum{

    {
        return [MSGraphEmptyEnum UnknownEnumValue];
    }
}

@end
