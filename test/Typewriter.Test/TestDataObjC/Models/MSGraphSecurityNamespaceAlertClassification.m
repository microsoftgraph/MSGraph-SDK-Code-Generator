// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphSecurityAlertClassification.h"

@interface MSGraphSecurityAlertClassification () {
    MSGraphSecurityAlertClassificationValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphSecurityAlertClassificationValue enumValue;
@end

@implementation MSGraphSecurityAlertClassification

+ (MSGraphSecurityAlertClassification*) unknown {
    static MSGraphSecurityAlertClassification *_unknown;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknown = [[MSGraphSecurityAlertClassification alloc] init];
        _unknown.enumValue = MSGraphSecurityAlertClassificationUnknown;
    });
    return _unknown;
}
+ (MSGraphSecurityAlertClassification*) falsePositive {
    static MSGraphSecurityAlertClassification *_falsePositive;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _falsePositive = [[MSGraphSecurityAlertClassification alloc] init];
        _falsePositive.enumValue = MSGraphSecurityAlertClassificationFalsePositive;
    });
    return _falsePositive;
}
+ (MSGraphSecurityAlertClassification*) truePositive {
    static MSGraphSecurityAlertClassification *_truePositive;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _truePositive = [[MSGraphSecurityAlertClassification alloc] init];
        _truePositive.enumValue = MSGraphSecurityAlertClassificationTruePositive;
    });
    return _truePositive;
}
+ (MSGraphSecurityAlertClassification*) informationalExpectedActivity {
    static MSGraphSecurityAlertClassification *_informationalExpectedActivity;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _informationalExpectedActivity = [[MSGraphSecurityAlertClassification alloc] init];
        _informationalExpectedActivity.enumValue = MSGraphSecurityAlertClassificationInformationalExpectedActivity;
    });
    return _informationalExpectedActivity;
}
+ (MSGraphSecurityAlertClassification*) unknownFutureValue {
    static MSGraphSecurityAlertClassification *_unknownFutureValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownFutureValue = [[MSGraphSecurityAlertClassification alloc] init];
        _unknownFutureValue.enumValue = MSGraphSecurityAlertClassificationUnknownFutureValue;
    });
    return _unknownFutureValue;
}

+ (MSGraphSecurityAlertClassification*) UnknownEnumValue {
    static MSGraphSecurityAlertClassification *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphSecurityAlertClassification alloc] init];
        _unknownValue.enumValue = MSGraphSecurityAlertClassificationEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphSecurityAlertClassification*) alertClassificationWithEnumValue:(MSGraphSecurityAlertClassificationValue)val {

    switch(val)
    {
        case MSGraphSecurityAlertClassificationUnknown:
            return [MSGraphSecurityAlertClassification unknown];
        case MSGraphSecurityAlertClassificationFalsePositive:
            return [MSGraphSecurityAlertClassification falsePositive];
        case MSGraphSecurityAlertClassificationTruePositive:
            return [MSGraphSecurityAlertClassification truePositive];
        case MSGraphSecurityAlertClassificationInformationalExpectedActivity:
            return [MSGraphSecurityAlertClassification informationalExpectedActivity];
        case MSGraphSecurityAlertClassificationUnknownFutureValue:
            return [MSGraphSecurityAlertClassification unknownFutureValue];
        case MSGraphSecurityAlertClassificationEndOfEnum:
        default:
            return [MSGraphSecurityAlertClassification UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphSecurityAlertClassificationUnknown:
            return @"unknown";
        case MSGraphSecurityAlertClassificationFalsePositive:
            return @"falsePositive";
        case MSGraphSecurityAlertClassificationTruePositive:
            return @"truePositive";
        case MSGraphSecurityAlertClassificationInformationalExpectedActivity:
            return @"informationalExpectedActivity";
        case MSGraphSecurityAlertClassificationUnknownFutureValue:
            return @"unknownFutureValue";
        case MSGraphSecurityAlertClassificationEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphSecurityAlertClassificationValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphSecurityAlertClassification)

- (MSGraphSecurityAlertClassification*) toMSGraphSecurityAlertClassification{

    if([self isEqualToString:@"unknown"])
    {
          return [MSGraphSecurityAlertClassification unknown];
    }
    else if([self isEqualToString:@"falsePositive"])
    {
          return [MSGraphSecurityAlertClassification falsePositive];
    }
    else if([self isEqualToString:@"truePositive"])
    {
          return [MSGraphSecurityAlertClassification truePositive];
    }
    else if([self isEqualToString:@"informationalExpectedActivity"])
    {
          return [MSGraphSecurityAlertClassification informationalExpectedActivity];
    }
    else if([self isEqualToString:@"unknownFutureValue"])
    {
          return [MSGraphSecurityAlertClassification unknownFutureValue];
    }
    else {
        return [MSGraphSecurityAlertClassification UnknownEnumValue];
    }
}

@end
