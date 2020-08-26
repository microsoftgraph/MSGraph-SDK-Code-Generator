// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.


@class MSGraphTestType, MSGraphEntityType2, MSGraphEntityType3; 


#import "MSGraphEntity.h"

@interface MSGraphTestEntity : MSGraphEntity

  @property (nullable, nonatomic, setter=setTestNav:, getter=testNav) MSGraphTestType* testNav;
    @property (nullable, nonatomic, setter=setTestInvalidNav:, getter=testInvalidNav) MSGraphEntityType2* testInvalidNav;
    @property (nullable, nonatomic, setter=setTestExplicitNav:, getter=testExplicitNav) MSGraphEntityType3* testExplicitNav;
  
@end
