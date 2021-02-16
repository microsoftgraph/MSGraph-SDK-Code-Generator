// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphTestEntityRequestBuilder

-(MSGraphTestTypeRequestBuilder *)testNav
{
    return [[MSGraphTestTypeRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"testNav"] client:self.client];

}

-(MSGraphEntityType2RequestBuilder *)testInvalidNav
{
    return [[MSGraphEntityType2RequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"testInvalidNav"] client:self.client];

}

-(MSGraphEntityType3RequestBuilder *)testExplicitNav
{
    return [[MSGraphEntityType3RequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"testExplicitNav"] client:self.client];

}


- (MSGraphTestEntityRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphTestEntityRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphTestEntityRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
