// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphCallRecordsMediaStreamDirectionValue){

	MSGraphCallRecordsMediaStreamDirectionCallerToCallee = 0,
	MSGraphCallRecordsMediaStreamDirectionCalleeToCaller = 1,
    MSGraphCallRecordsMediaStreamDirectionEndOfEnum
};

@interface MSGraphCallRecordsMediaStreamDirection : NSObject

+(MSGraphCallRecordsMediaStreamDirection*) callerToCallee;
+(MSGraphCallRecordsMediaStreamDirection*) calleeToCaller;

+(MSGraphCallRecordsMediaStreamDirection*) UnknownEnumValue;

+(MSGraphCallRecordsMediaStreamDirection*) mediaStreamDirectionWithEnumValue:(MSGraphCallRecordsMediaStreamDirectionValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphCallRecordsMediaStreamDirectionValue enumValue;

@end


@interface NSString (MSGraphCallRecordsMediaStreamDirection)

- (MSGraphCallRecordsMediaStreamDirection*) toMSGraphCallRecordsMediaStreamDirection;

@end
