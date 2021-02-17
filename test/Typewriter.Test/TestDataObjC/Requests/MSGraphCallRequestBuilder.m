// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphCallRequestBuilder


- (MSGraphCallRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphCallRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphCallRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
