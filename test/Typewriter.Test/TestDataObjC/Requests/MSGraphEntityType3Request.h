// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;
#import "MSGraphModels.h"
#import "MSRequest.h"

@interface MSGraphEntityType3Request : MSRequest

- (MSURLSessionDataTask *)getWithCompletion:(void (^)(MSGraphEntityType3 *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)update:(MSGraphEntityType3 *)entityType3 withCompletion:(void (^)(MSGraphEntityType3 *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)deleteWithCompletion:(void(^)(NSError *error))completionHandler;

@end
