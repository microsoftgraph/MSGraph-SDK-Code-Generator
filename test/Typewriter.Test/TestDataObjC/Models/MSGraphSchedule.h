// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.


@class MSGraphTimeOff, MSGraphTimeOffRequest; 


#import "MSGraphEntity.h"

@interface MSGraphSchedule : MSGraphEntity

  @property (nonatomic, setter=setEnabled:, getter=enabled) BOOL enabled;
    @property (nullable, nonatomic, setter=setTimesOff:, getter=timesOff) NSArray* timesOff;
    @property (nullable, nonatomic, setter=setTimeOffRequests:, getter=timeOffRequests) NSArray* timeOffRequests;
  
@end
