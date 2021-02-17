// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;
@class MSGraphSessionRequestBuilder;
@class MSGraphSessionsCollectionRequestBuilder;
@class MSGraphEntityType2RequestBuilder;
@class MSGraphRecipientsCollectionRequestBuilder;
#import "MSGraphModels.h"
#import "MSRequest.h"

@interface MSGraphCallRecordRequest : MSRequest

- (MSURLSessionDataTask *)getWithCompletion:(void (^)(MSGraphCallRecord *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)update:(MSGraphCallRecord *)callRecord withCompletion:(void (^)(MSGraphCallRecord *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)deleteWithCompletion:(void(^)(NSError *error))completionHandler;

@end
