// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphEnum1.h"

@interface MSGraphEnum1 () {
    MSGraphEnum1Value _enumValue;
}
@property (nonatomic, readwrite) MSGraphEnum1Value enumValue;
@end

@implementation MSGraphEnum1

+ (MSGraphEnum1*) value0 {
    static MSGraphEnum1 *_value0;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _value0 = [[MSGraphEnum1 alloc] init];
        _value0.enumValue = MSGraphEnum1Value0;
    });
    return _value0;
}
+ (MSGraphEnum1*) value1 {
    static MSGraphEnum1 *_value1;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _value1 = [[MSGraphEnum1 alloc] init];
        _value1.enumValue = MSGraphEnum1Value1;
    });
    return _value1;
}

+ (MSGraphEnum1*) UnknownEnumValue {
    static MSGraphEnum1 *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphEnum1 alloc] init];
        _unknownValue.enumValue = MSGraphEnum1EndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphEnum1*) enum1WithEnumValue:(MSGraphEnum1Value)val {

    switch(val)
    {
        case MSGraphEnum1Value0:
            return [MSGraphEnum1 value0];
        case MSGraphEnum1Value1:
            return [MSGraphEnum1 value1];
        case MSGraphEnum1EndOfEnum:
        default:
            return [MSGraphEnum1 UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphEnum1Value0:
            return @"value0";
        case MSGraphEnum1Value1:
            return @"value1";
        case MSGraphEnum1EndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphEnum1Value) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphEnum1)

- (MSGraphEnum1*) toMSGraphEnum1{

    if([self isEqualToString:@"value0"])
    {
          return [MSGraphEnum1 value0];
    }
    else if([self isEqualToString:@"value1"])
    {
          return [MSGraphEnum1 value1];
    }
    else {
        return [MSGraphEnum1 UnknownEnumValue];
    }
}

@end
