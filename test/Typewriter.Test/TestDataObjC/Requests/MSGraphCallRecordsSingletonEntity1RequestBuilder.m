// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphSingletonEntity1RequestBuilder

-(MSGraphTestTypeRequestBuilder *)testSingleNav
{
    return [[MSGraphTestTypeRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"testSingleNav"] client:self.client];

}


- (MSGraphSingletonEntity1Request *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphSingletonEntity1Request *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphSingletonEntity1Request alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
