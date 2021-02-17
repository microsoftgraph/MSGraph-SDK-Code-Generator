// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphEndpointRequestBuilder


- (MSGraphEndpointRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphEndpointRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphEndpointRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
