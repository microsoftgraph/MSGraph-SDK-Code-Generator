// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphOnenotePageRequestBuilder

- (MSGraphOnenotePageContentRequest *) contentRequestWithOptions:(NSArray *)options
{
    NSURL *contentURL = [self.requestURL URLByAppendingPathComponent:@"content"];
    return [[MSGraphOnenotePageContentRequest alloc] initWithURL:contentURL options:options client:self.client];
}

- (MSGraphOnenotePageContentRequest *) contentRequest
{
    return [self contentRequestWithOptions:nil];
}

- (MSGraphOnenotePageForwardRequestBuilder *)forwardWithToRecipients:(NSArray *)toRecipients details:(NSString *)details comment:(NSString *)comment 
{
    NSURL *actionURL = [self.requestURL URLByAppendingPathComponent:@"microsoft.graph.forward"];
    return [[MSGraphOnenotePageForwardRequestBuilder alloc] initWithToRecipients:toRecipients
                                                                         details:details
                                                                         comment:comment
                                                                             URL:actionURL
                                                                          client:self.client];


}

- (MSGraphOnenotePageForwardRequestBuilder *)forwardWithToRecipients:(NSArray *)toRecipients comment:(NSString *)comment 
{
    NSURL *actionURL = [self.requestURL URLByAppendingPathComponent:@"microsoft.graph.forward"];
    return [[MSGraphOnenotePageForwardRequestBuilder alloc] initWithToRecipients:toRecipients
                                                                         comment:comment
                                                                             URL:actionURL
                                                                          client:self.client];


}


- (MSGraphOnenotePageRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphOnenotePageRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphOnenotePageRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
