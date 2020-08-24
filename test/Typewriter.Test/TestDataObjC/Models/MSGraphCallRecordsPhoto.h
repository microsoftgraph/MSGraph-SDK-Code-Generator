// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.


@class MSGraphCallRecordsFailureInfo, MSGraphCallRecordsOption; 


#import "MSGraphEntity.h"

@interface MSGraphCallRecordsPhoto : MSGraphEntity

  @property (nullable, nonatomic, setter=setFailureInfo:, getter=failureInfo) MSGraphCallRecordsFailureInfo* failureInfo;
    @property (nullable, nonatomic, setter=setOption:, getter=option) MSGraphCallRecordsOption* option;
  
@end
