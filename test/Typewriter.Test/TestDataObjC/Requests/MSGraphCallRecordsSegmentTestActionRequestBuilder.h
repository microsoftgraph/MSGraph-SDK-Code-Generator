// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"
#import "MSGraphModels.h"
#import "MSRequestBuilder.h"

@class MSGraphSegmentTestActionRequest;

@interface MSGraphSegmentTestActionRequestBuilder : MSRequestBuilder

- (instancetype)initWithValue:(MSGraphIdentitySet *)value URL:(NSURL *)url client:(ODataBaseClient*)client;

- (MSGraphSegmentTestActionRequest *)request;

- (MSGraphSegmentTestActionRequest *)requestWithOptions:(NSArray *)options;

@end
