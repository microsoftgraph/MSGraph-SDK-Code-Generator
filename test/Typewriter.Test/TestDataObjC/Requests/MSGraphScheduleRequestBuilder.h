// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphScheduleRequest, MSGraphTimeOffRequestBuilder, MSGraphScheduleTimesOffCollectionRequestBuilder, MSGraphTimeOffRequestRequestBuilder, MSGraphScheduleTimeOffRequestsCollectionRequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphScheduleRequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphScheduleTimesOffCollectionRequestBuilder *)timesOff;

- (MSGraphTimeOffRequestBuilder *)timesOff:(NSString *)timeOff;

- (MSGraphScheduleTimeOffRequestsCollectionRequestBuilder *)timeOffRequests;

- (MSGraphTimeOffRequestRequestBuilder *)timeOffRequests:(NSString *)timeOffRequest;


- (MSGraphScheduleRequest *) request;

- (MSGraphScheduleRequest *) requestWithOptions:(NSArray *)options;


@end
