// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphSecurityAlertClassificationValue){

	MSGraphSecurityAlertClassificationUnknown = 0,
	MSGraphSecurityAlertClassificationFalsePositive = 10,
	MSGraphSecurityAlertClassificationTruePositive = 20,
	MSGraphSecurityAlertClassificationInformationalExpectedActivity = 30,
	MSGraphSecurityAlertClassificationUnknownFutureValue = 39,
    MSGraphSecurityAlertClassificationEndOfEnum
};

@interface MSGraphSecurityAlertClassification : NSObject

+(MSGraphSecurityAlertClassification*) unknown;
+(MSGraphSecurityAlertClassification*) falsePositive;
+(MSGraphSecurityAlertClassification*) truePositive;
+(MSGraphSecurityAlertClassification*) informationalExpectedActivity;
+(MSGraphSecurityAlertClassification*) unknownFutureValue;

+(MSGraphSecurityAlertClassification*) UnknownEnumValue;

+(MSGraphSecurityAlertClassification*) alertClassificationWithEnumValue:(MSGraphSecurityAlertClassificationValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphSecurityAlertClassificationValue enumValue;

@end


@interface NSString (MSGraphSecurityAlertClassification)

- (MSGraphSecurityAlertClassification*) toMSGraphSecurityAlertClassification;

@end
