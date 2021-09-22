// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphTimeOffRequestRequestBuilder


- (MSGraphTimeOffRequestRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphTimeOffRequestRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphTimeOffRequestRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
