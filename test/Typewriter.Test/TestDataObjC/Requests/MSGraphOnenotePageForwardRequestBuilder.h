// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"
#import "MSGraphModels.h"
#import "MSRequestBuilder.h"

@class MSGraphOnenotePageForwardRequest;

@interface MSGraphOnenotePageForwardRequestBuilder : MSRequestBuilder

- (instancetype)initWithToRecipients:(NSArray *)toRecipients details:(NSString *)details comment:(NSString *)comment URL:(NSURL *)url client:(ODataBaseClient*)client;

- (MSGraphOnenotePageForwardRequest *)request;

- (MSGraphOnenotePageForwardRequest *)requestWithOptions:(NSArray *)options;

@end
