// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphOnenotePageRequest, MSGraphOnenotePageContentRequest, MSGraphOnenotePageForwardRequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphOnenotePageRequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphOnenotePageContentRequest *) contentRequestWithOptions:(NSArray *)options;

- (MSGraphOnenotePageContentRequest *) contentRequest;

- (MSGraphOnenotePageForwardRequestBuilder *)forwardWithToRecipients:(NSArray *)toRecipients details:(NSString *)details comment:(NSString *)comment ;

- (MSGraphOnenotePageForwardRequestBuilder *)forwardWithToRecipients:(NSArray *)toRecipients comment:(NSString *)comment ;


- (MSGraphOnenotePageRequest *) request;

- (MSGraphOnenotePageRequest *) requestWithOptions:(NSArray *)options;


@end
