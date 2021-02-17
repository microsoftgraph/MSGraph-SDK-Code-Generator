// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@interface MSGraphCallRecordItemRequestBuilder()


@property (nonatomic, getter=name) NSString * name;

@end

@implementation MSGraphCallRecordItemRequestBuilder


- (instancetype)initWithName:(NSString *)name URL:(NSURL *)url client:(ODataBaseClient*)client
{
    self = [super initWithURL:url client:client];
    if (self){
        _name = name;
    }
    return self;
}

- (MSGraphCallRecordItemRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphCallRecordItemRequest *)requestWithOptions:(NSArray *)options
{

    return [[MSGraphCallRecordItemRequest alloc] initWithName:self.name
                                                          URL:self.requestURL
                                                      options:options
                                                       client:self.client];

}

@end
