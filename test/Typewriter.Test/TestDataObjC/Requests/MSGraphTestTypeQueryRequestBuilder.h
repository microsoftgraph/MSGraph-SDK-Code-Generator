// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"
#import "MSGraphModels.h"
#import "MSRequestBuilder.h"

@class MSGraphTestTypeQueryRequest;

@interface MSGraphTestTypeQueryRequestBuilder : MSRequestBuilder

- (instancetype)initWithRequests:(NSArray *)requests URL:(NSURL *)url client:(ODataBaseClient*)client;

- (MSGraphTestTypeQueryRequest *)request;

- (MSGraphTestTypeQueryRequest *)requestWithOptions:(NSArray *)options;

@end
