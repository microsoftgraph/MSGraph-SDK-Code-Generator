// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;
@class MSGraphTimeOffRequestBuilder;
@class MSGraphTimesOffCollectionRequestBuilder;
@class MSGraphTimeOffRequestRequestBuilder;
@class MSGraphTimeOffRequestsCollectionRequestBuilder;
#import "MSGraphModels.h"
#import "MSRequest.h"

@interface MSGraphScheduleRequest : MSRequest

- (MSURLSessionDataTask *)getWithCompletion:(void (^)(MSGraphSchedule *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)update:(MSGraphSchedule *)schedule withCompletion:(void (^)(MSGraphSchedule *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)deleteWithCompletion:(void(^)(NSError *error))completionHandler;

@end
