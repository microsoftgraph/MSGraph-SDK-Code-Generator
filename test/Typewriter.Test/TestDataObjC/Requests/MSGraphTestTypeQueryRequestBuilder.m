// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@interface MSGraphTestTypeQueryRequestBuilder()


@property (nonatomic, getter=requests) NSArray * requests;

@end

@implementation MSGraphTestTypeQueryRequestBuilder


- (instancetype)initWithRequests:(NSArray *)requests URL:(NSURL *)url client:(ODataBaseClient*)client
{
    self = [super initWithURL:url client:client];
    if (self){
        _requests = requests;
    }
    return self;
}

- (MSGraphTestTypeQueryRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphTestTypeQueryRequest *)requestWithOptions:(NSArray *)options
{

    return [[MSGraphTestTypeQueryRequest alloc] initWithRequests:self.requests
                                                             URL:self.requestURL
                                                         options:options
                                                          client:self.client];

}

@end
