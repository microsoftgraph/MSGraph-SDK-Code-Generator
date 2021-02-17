// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@interface MSGraphSegmentTestActionRequestBuilder()


@property (nonatomic, getter=value) MSGraphIdentitySet * value;

@end

@implementation MSGraphSegmentTestActionRequestBuilder


- (instancetype)initWithValue:(MSGraphIdentitySet *)value URL:(NSURL *)url client:(ODataBaseClient*)client
{
    self = [super initWithURL:url client:client];
    if (self){
        _value = value;
    }
    return self;
}

- (MSGraphSegmentTestActionRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphSegmentTestActionRequest *)requestWithOptions:(NSArray *)options
{

    return [[MSGraphSegmentTestActionRequest alloc] initWithValue:self.value
                                                              URL:self.requestURL
                                                          options:options
                                                           client:self.client];

}

@end
