// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.


@class MSGraphDerivedComplexTypeRequest; 


#import "MSGraphEntity.h"

@interface MSGraphTestType : MSGraphEntity

  @property (nullable, nonatomic, setter=setPropertyAlpha:, getter=propertyAlpha) MSGraphDerivedComplexTypeRequest* propertyAlpha;
  
@end
