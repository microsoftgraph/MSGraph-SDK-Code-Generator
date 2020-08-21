// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphCallRecordsModalityValue){

	MSGraphCallRecordsModalityAudio = 0,
	MSGraphCallRecordsModalityVideo = 1,
    MSGraphCallRecordsModalityEndOfEnum
};

@interface MSGraphCallRecordsModality : NSObject

+(MSGraphCallRecordsModality*) audio;
+(MSGraphCallRecordsModality*) video;

+(MSGraphCallRecordsModality*) UnknownEnumValue;

+(MSGraphCallRecordsModality*) modalityWithEnumValue:(MSGraphCallRecordsModalityValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphCallRecordsModalityValue enumValue;

@end


@interface NSString (MSGraphCallRecordsModality)

- (MSGraphCallRecordsModality*) toMSGraphCallRecordsModality;

@end
