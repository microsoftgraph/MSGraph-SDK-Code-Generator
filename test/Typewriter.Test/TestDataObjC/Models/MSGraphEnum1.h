// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphEnum1Value){

	MSGraphEnum1Value0 = 0,
	MSGraphEnum1Value1 = 1,
    MSGraphEnum1EndOfEnum
};

@interface MSGraphEnum1 : NSObject

+(MSGraphEnum1*) value0;
+(MSGraphEnum1*) value1;

+(MSGraphEnum1*) UnknownEnumValue;

+(MSGraphEnum1*) enum1WithEnumValue:(MSGraphEnum1Value)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphEnum1Value enumValue;

@end


@interface NSString (MSGraphEnum1)

- (MSGraphEnum1*) toMSGraphEnum1;

@end
