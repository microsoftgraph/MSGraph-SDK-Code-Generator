// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphPrintRequestBuilder


- (MSGraphPrintRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphPrintRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphPrintRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
