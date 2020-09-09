// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphCallRecordsClientPlatformValue){

	MSGraphCallRecordsClientPlatformUnknown = 0,
	MSGraphCallRecordsClientPlatformWindows = 1,
    MSGraphCallRecordsClientPlatformEndOfEnum
};

@interface MSGraphCallRecordsClientPlatform : NSObject

+(MSGraphCallRecordsClientPlatform*) unknown;
+(MSGraphCallRecordsClientPlatform*) windows;

+(MSGraphCallRecordsClientPlatform*) UnknownEnumValue;

+(MSGraphCallRecordsClientPlatform*) clientPlatformWithEnumValue:(MSGraphCallRecordsClientPlatformValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphCallRecordsClientPlatformValue enumValue;

@end


@interface NSString (MSGraphCallRecordsClientPlatform)

- (MSGraphCallRecordsClientPlatform*) toMSGraphCallRecordsClientPlatform;

@end
