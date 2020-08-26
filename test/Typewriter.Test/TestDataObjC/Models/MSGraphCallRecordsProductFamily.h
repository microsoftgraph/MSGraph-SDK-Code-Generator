// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphCallRecordsProductFamilyValue){

	MSGraphCallRecordsProductFamilyUnknown = 0,
	MSGraphCallRecordsProductFamilyTeams = 1,
    MSGraphCallRecordsProductFamilyEndOfEnum
};

@interface MSGraphCallRecordsProductFamily : NSObject

+(MSGraphCallRecordsProductFamily*) unknown;
+(MSGraphCallRecordsProductFamily*) teams;

+(MSGraphCallRecordsProductFamily*) UnknownEnumValue;

+(MSGraphCallRecordsProductFamily*) productFamilyWithEnumValue:(MSGraphCallRecordsProductFamilyValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphCallRecordsProductFamilyValue enumValue;

@end


@interface NSString (MSGraphCallRecordsProductFamily)

- (MSGraphCallRecordsProductFamily*) toMSGraphCallRecordsProductFamily;

@end
