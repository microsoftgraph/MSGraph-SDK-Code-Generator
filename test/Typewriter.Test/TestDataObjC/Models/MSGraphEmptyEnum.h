// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphEmptyEnumValue){

    MSGraphEmptyEnumEndOfEnum
};

@interface MSGraphEmptyEnum : NSObject


+(MSGraphEmptyEnum*) UnknownEnumValue;

+(MSGraphEmptyEnum*) emptyEnumWithEnumValue:(MSGraphEmptyEnumValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphEmptyEnumValue enumValue;

@end


@interface NSString (MSGraphEmptyEnum)

- (MSGraphEmptyEnum*) toMSGraphEmptyEnum;

@end
