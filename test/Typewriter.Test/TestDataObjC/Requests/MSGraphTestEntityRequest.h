// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;
@class MSGraphTestTypeRequestBuilder;
@class MSGraphEntityType2RequestBuilder;
@class MSGraphEntityType3RequestBuilder;
#import "MSGraphModels.h"
#import "MSRequest.h"

@interface MSGraphTestEntityRequest : MSRequest

- (MSURLSessionDataTask *)getWithCompletion:(void (^)(MSGraphTestEntity *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)update:(MSGraphTestEntity *)testEntity withCompletion:(void (^)(MSGraphTestEntity *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)deleteWithCompletion:(void(^)(NSError *error))completionHandler;

@end
