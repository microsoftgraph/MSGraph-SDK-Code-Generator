// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphEntityRequestBuilder


- (MSGraphEntityRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphEntityRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphEntityRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
