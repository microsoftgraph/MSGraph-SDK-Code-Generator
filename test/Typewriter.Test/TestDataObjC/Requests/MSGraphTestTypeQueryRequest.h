// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;

#import "MSCollectionRequest.h"

@interface MSGraphTestTypeQueryRequest : MSCollectionRequest

@property (readonly) NSMutableURLRequest *mutableRequest;

- (instancetype)initWithRequests:(NSArray *)requests URL:(NSURL *)url options:(NSArray *)options client:(ODataBaseClient*)client;

- (MSURLSessionDataTask *)executeWithCompletion:(void (^)(MSCollection *response, MSGraphTestTypeQueryRequest *nextRequest, NSError *error))completionHandler;

@end
