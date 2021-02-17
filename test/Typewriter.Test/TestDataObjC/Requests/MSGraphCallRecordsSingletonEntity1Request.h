// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;
@class MSGraphTestTypeRequestBuilder;
#import "MSGraphModels.h"
#import "MSRequest.h"

@interface MSGraphSingletonEntity1Request : MSRequest

- (MSURLSessionDataTask *)getWithCompletion:(void (^)(MSGraphSingletonEntity1 *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)update:(MSGraphSingletonEntity1 *)singletonEntity1 withCompletion:(void (^)(MSGraphSingletonEntity1 *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)deleteWithCompletion:(void(^)(NSError *error))completionHandler;

@end
