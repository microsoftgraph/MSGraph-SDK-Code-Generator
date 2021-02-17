// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphEntityType3RequestBuilder

- (MSGraphEntityType3ForwardRequestBuilder *)forwardWithToRecipients:(NSArray *)toRecipients singleRecipient:(MSGraphRecipient *)singleRecipient multipleSessions:(NSArray *)multipleSessions singleSession:(MSGraphCallRecordsSession *)singleSession comment:(NSString *)comment 
{
    NSURL *actionURL = [self.requestURL URLByAppendingPathComponent:@"microsoft.graph.forward"];
    return [[MSGraphEntityType3ForwardRequestBuilder alloc] initWithToRecipients:toRecipients
                                                                 singleRecipient:singleRecipient
                                                                multipleSessions:multipleSessions
                                                                   singleSession:singleSession
                                                                         comment:comment
                                                                             URL:actionURL
                                                                          client:self.client];


}


- (MSGraphEntityType3Request *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphEntityType3Request *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphEntityType3Request alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
