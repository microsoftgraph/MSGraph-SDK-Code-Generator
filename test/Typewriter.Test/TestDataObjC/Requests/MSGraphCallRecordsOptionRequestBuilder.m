// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphOptionRequestBuilder


- (MSGraphOptionRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphOptionRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphOptionRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
