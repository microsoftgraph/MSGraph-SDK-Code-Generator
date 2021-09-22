// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphPhotoRequestBuilder


- (MSGraphPhotoRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphPhotoRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphPhotoRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
