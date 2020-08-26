// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.


@class MSGraphTestType; 


#import "MSGraphEntity.h"

@interface MSGraphCallRecordsSingletonEntity1 : MSGraphEntity

  @property (nullable, nonatomic, setter=setTestSingleNav:, getter=testSingleNav) MSGraphTestType* testSingleNav;
  
@end
