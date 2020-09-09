// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.


@class MSGraphCall, MSGraphCallRecordsCallRecord; 


#import "MSGraphEntity.h"

@interface MSGraphCloudCommunications : MSGraphEntity

  @property (nullable, nonatomic, setter=setCalls:, getter=calls) NSArray* calls;
    @property (nullable, nonatomic, setter=setCallRecords:, getter=callRecords) NSArray* callRecords;
  
@end
