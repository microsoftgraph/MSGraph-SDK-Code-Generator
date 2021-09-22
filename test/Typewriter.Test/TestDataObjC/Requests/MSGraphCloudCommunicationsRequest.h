// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;
@class MSGraphCallRequestBuilder;
@class MSGraphCallsCollectionRequestBuilder;
@class MSGraphCallRecordRequestBuilder;
@class MSGraphCallRecordsCollectionRequestBuilder;
#import "MSGraphModels.h"
#import "MSRequest.h"

@interface MSGraphCloudCommunicationsRequest : MSRequest

- (MSURLSessionDataTask *)getWithCompletion:(void (^)(MSGraphCloudCommunications *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)update:(MSGraphCloudCommunications *)cloudCommunications withCompletion:(void (^)(MSGraphCloudCommunications *response, NSError *error))completionHandler;

- (MSURLSessionDataTask *)deleteWithCompletion:(void(^)(NSError *error))completionHandler;

@end
