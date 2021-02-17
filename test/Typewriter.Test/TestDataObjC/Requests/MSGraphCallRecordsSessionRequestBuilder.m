// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphSessionRequestBuilder

- (MSGraphSessionSegmentsCollectionRequestBuilder *)segments
{
    return [[MSGraphSessionSegmentsCollectionRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"segments"]  
                                                                        client:self.client];
}

- (MSGraphSegmentRequestBuilder *)segments:(NSString *)segment
{
    return [[self segments] segment:segment];
}


- (MSGraphSessionRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphSessionRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphSessionRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
