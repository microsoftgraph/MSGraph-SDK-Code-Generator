// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphCallRecordsServiceRoleValue){

	MSGraphCallRecordsServiceRoleUnknown = 0,
	MSGraphCallRecordsServiceRoleCustomBot = 1,
    MSGraphCallRecordsServiceRoleEndOfEnum
};

@interface MSGraphCallRecordsServiceRole : NSObject

+(MSGraphCallRecordsServiceRole*) unknown;
+(MSGraphCallRecordsServiceRole*) customBot;

+(MSGraphCallRecordsServiceRole*) UnknownEnumValue;

+(MSGraphCallRecordsServiceRole*) serviceRoleWithEnumValue:(MSGraphCallRecordsServiceRoleValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphCallRecordsServiceRoleValue enumValue;

@end


@interface NSString (MSGraphCallRecordsServiceRole)

- (MSGraphCallRecordsServiceRole*) toMSGraphCallRecordsServiceRole;

@end
