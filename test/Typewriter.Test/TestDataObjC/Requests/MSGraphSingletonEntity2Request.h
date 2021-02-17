// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;
@class MSGraphEntityType3RequestBuilder;
#import "MSGraphModels.h"
#import "MSRequest.h"

@interface MSGraphSingletonEntity2Request : MSRequest

- (MSURLSessionDataTask *)getWithCompletion:(void (^)(MSGraphSingletonEntity2 *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)update:(MSGraphSingletonEntity2 *)singletonEntity2 withCompletion:(void (^)(MSGraphSingletonEntity2 *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)deleteWithCompletion:(void(^)(NSError *error))completionHandler;

@end
