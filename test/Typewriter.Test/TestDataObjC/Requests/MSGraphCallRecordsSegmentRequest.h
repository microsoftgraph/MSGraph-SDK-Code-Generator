// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;
@class MSGraphEntityType3RequestBuilder;
@class MSGraphRefTypesCollectionRequestBuilder;
@class MSGraphCallRequestBuilder;
@class MSGraphSessionRequestBuilder;
@class MSGraphPhotoRequestBuilder;
#import "MSGraphModels.h"
#import "MSRequest.h"

@interface MSGraphSegmentRequest : MSRequest

- (MSURLSessionDataTask *)getWithCompletion:(void (^)(MSGraphSegment *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)update:(MSGraphSegment *)segment withCompletion:(void (^)(MSGraphSegment *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)deleteWithCompletion:(void(^)(NSError *error))completionHandler;

@end
