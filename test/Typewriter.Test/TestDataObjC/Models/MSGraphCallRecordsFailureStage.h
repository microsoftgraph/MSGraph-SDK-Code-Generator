// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphCallRecordsFailureStageValue){

	MSGraphCallRecordsFailureStageUnknown = 0,
	MSGraphCallRecordsFailureStageCallSetup = 1,
    MSGraphCallRecordsFailureStageEndOfEnum
};

@interface MSGraphCallRecordsFailureStage : NSObject

+(MSGraphCallRecordsFailureStage*) unknown;
+(MSGraphCallRecordsFailureStage*) callSetup;

+(MSGraphCallRecordsFailureStage*) UnknownEnumValue;

+(MSGraphCallRecordsFailureStage*) failureStageWithEnumValue:(MSGraphCallRecordsFailureStageValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphCallRecordsFailureStageValue enumValue;

@end


@interface NSString (MSGraphCallRecordsFailureStage)

- (MSGraphCallRecordsFailureStage*) toMSGraphCallRecordsFailureStage;

@end
