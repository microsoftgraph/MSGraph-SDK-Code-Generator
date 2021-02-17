// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;
#import "MSGraphModels.h"
#import "MSRequest.h"

@interface MSGraphEntityType2Request : MSRequest

- (MSURLSessionDataTask *)getWithCompletion:(void (^)(MSGraphEntityType2 *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)update:(MSGraphEntityType2 *)entityType2 withCompletion:(void (^)(MSGraphEntityType2 *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)deleteWithCompletion:(void(^)(NSError *error))completionHandler;

@end
