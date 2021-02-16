// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphEntityType2RequestBuilder


- (MSGraphEntityType2Request *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphEntityType2Request *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphEntityType2Request alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
