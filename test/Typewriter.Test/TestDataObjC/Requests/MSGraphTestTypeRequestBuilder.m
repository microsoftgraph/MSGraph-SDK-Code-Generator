// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphTestTypeRequestBuilder

- (MSGraphTestTypeQueryRequestBuilder *)queryWithRequests:(NSArray *)requests 
{
    NSURL *actionURL = [self.requestURL URLByAppendingPathComponent:@"microsoft.graph.query"];
    return [[MSGraphTestTypeQueryRequestBuilder alloc] initWithRequests:requests
                                                                    URL:actionURL
                                                                 client:self.client];


}


- (MSGraphTestTypeRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphTestTypeRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphTestTypeRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
