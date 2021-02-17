// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSURLSessionDataTask;

#import "MSCollectionRequest.h"

@interface MSGraphSegmentTestActionRequest : MSCollectionRequest

@property (readonly) NSMutableURLRequest *mutableRequest;

- (instancetype)initWithValue:(MSGraphIdentitySet *)value URL:(NSURL *)url options:(NSArray *)options client:(ODataBaseClient*)client;

- (MSURLSessionDataTask *)executeWithCompletion:(void (^)(MSCollection *response, MSGraphSegmentTestActionRequest *nextRequest, NSError *error))completionHandler;

@end
