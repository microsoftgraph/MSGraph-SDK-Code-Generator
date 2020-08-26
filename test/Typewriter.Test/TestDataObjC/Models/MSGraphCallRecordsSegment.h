// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.


@class MSGraphCallRecordsEndpoint, MSGraphCallRecordsFailureInfo, MSGraphCallRecordsMedia, MSGraphEntityType3, MSGraphCall, MSGraphCallRecordsSession, MSGraphCallRecordsPhoto; 


#import "MSGraphEntity.h"

@interface MSGraphCallRecordsSegment : MSGraphEntity

  @property (nonnull, nonatomic, setter=setStartDateTime:, getter=startDateTime) NSDate* startDateTime;
    @property (nonnull, nonatomic, setter=setEndDateTime:, getter=endDateTime) NSDate* endDateTime;
    @property (nullable, nonatomic, setter=setCaller:, getter=caller) MSGraphCallRecordsEndpoint* caller;
    @property (nullable, nonatomic, setter=setCallee:, getter=callee) MSGraphCallRecordsEndpoint* callee;
    @property (nullable, nonatomic, setter=setFailureInfo:, getter=failureInfo) MSGraphCallRecordsFailureInfo* failureInfo;
    @property (nullable, nonatomic, setter=setMedia:, getter=media) NSArray* media;
    @property (nullable, nonatomic, setter=setRefTypes:, getter=refTypes) NSArray* refTypes;
    @property (nullable, nonatomic, setter=setRefType:, getter=refType) MSGraphCall* refType;
    @property (nullable, nonatomic, setter=setSessionRef:, getter=sessionRef) MSGraphCallRecordsSession* sessionRef;
    @property (nullable, nonatomic, setter=setPhoto:, getter=photo) MSGraphCallRecordsPhoto* photo;
  
@end
