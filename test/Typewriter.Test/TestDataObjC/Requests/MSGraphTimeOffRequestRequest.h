// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;
#import "MSGraphModels.h"
#import "MSRequest.h"

@interface MSGraphTimeOffRequestRequest : MSRequest

- (MSURLSessionDataTask *)getWithCompletion:(void (^)(MSGraphTimeOffRequest *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)update:(MSGraphTimeOffRequest *)timeOffRequest withCompletion:(void (^)(MSGraphTimeOffRequest *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)deleteWithCompletion:(void(^)(NSError *error))completionHandler;

@end
