// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphCallRecordsCallTypeValue){

	MSGraphCallRecordsCallTypeUnknown = 0,
	MSGraphCallRecordsCallTypeGroupCall = 1,
    MSGraphCallRecordsCallTypeEndOfEnum
};

@interface MSGraphCallRecordsCallType : NSObject

+(MSGraphCallRecordsCallType*) unknown;
+(MSGraphCallRecordsCallType*) groupCall;

+(MSGraphCallRecordsCallType*) UnknownEnumValue;

+(MSGraphCallRecordsCallType*) callTypeWithEnumValue:(MSGraphCallRecordsCallTypeValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphCallRecordsCallTypeValue enumValue;

@end


@interface NSString (MSGraphCallRecordsCallType)

- (MSGraphCallRecordsCallType*) toMSGraphCallRecordsCallType;

@end
