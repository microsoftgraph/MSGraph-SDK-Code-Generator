// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphTimeOffRequestBuilder


- (MSGraphTimeOffRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphTimeOffRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphTimeOffRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
