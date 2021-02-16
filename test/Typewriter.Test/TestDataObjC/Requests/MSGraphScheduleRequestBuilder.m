// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphScheduleRequestBuilder

- (MSGraphScheduleTimesOffCollectionRequestBuilder *)timesOff
{
    return [[MSGraphScheduleTimesOffCollectionRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"timesOff"]  
                                                                         client:self.client];
}

- (MSGraphTimeOffRequestBuilder *)timesOff:(NSString *)timeOff
{
    return [[self timesOff] timeOff:timeOff];
}

- (MSGraphScheduleTimeOffRequestsCollectionRequestBuilder *)timeOffRequests
{
    return [[MSGraphScheduleTimeOffRequestsCollectionRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"timeOffRequests"]  
                                                                                client:self.client];
}

- (MSGraphTimeOffRequestRequestBuilder *)timeOffRequests:(NSString *)timeOffRequest
{
    return [[self timeOffRequests] timeOffRequest:timeOffRequest];
}


- (MSGraphScheduleRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphScheduleRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphScheduleRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
