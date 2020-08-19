// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphEnum1.h"


#import "MSGraphEmptyBaseComplexTypeRequest.h"

@interface MSGraphDerivedComplexTypeRequest : MSGraphEmptyBaseComplexTypeRequest

@property (nullable, nonatomic, setter=setProperty1:, getter=property1) NSString* property1;
@property (nullable, nonatomic, setter=setProperty2:, getter=property2) NSString* property2;
@property (nullable, nonatomic, setter=setEnumProperty:, getter=enumProperty) MSGraphEnum1* enumProperty;

@end
