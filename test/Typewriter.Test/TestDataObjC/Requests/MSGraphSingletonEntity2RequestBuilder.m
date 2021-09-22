// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphSingletonEntity2RequestBuilder

-(MSGraphEntityType3RequestBuilder *)testSingleNav2
{
    return [[MSGraphEntityType3RequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"testSingleNav2"] client:self.client];

}


- (MSGraphSingletonEntity2Request *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphSingletonEntity2Request *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphSingletonEntity2Request alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
