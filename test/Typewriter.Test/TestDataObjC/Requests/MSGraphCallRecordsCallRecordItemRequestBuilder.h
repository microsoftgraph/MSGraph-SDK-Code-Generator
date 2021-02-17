// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"
#import "MSGraphModels.h"
#import "MSRequestBuilder.h"

@class MSGraphCallRecordItemRequest;

@interface MSGraphCallRecordItemRequestBuilder : MSRequestBuilder

- (instancetype)initWithName:(NSString *)name URL:(NSURL *)url client:(ODataBaseClient*)client;

- (MSGraphCallRecordItemRequest *)request;

- (MSGraphCallRecordItemRequest *)requestWithOptions:(NSArray *)options;

@end
