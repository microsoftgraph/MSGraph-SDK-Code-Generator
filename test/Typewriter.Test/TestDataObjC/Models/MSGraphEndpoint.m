// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphEndpoint()
{
    int64_t _property1;
}
@end

@implementation MSGraphEndpoint

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.endpoint";
    }
    return self;
}
- (int64_t) property1
{
    _property1 = [self.dictionary[@"property1"] longLongValue];
    return _property1;
}

- (void) setProperty1: (int64_t) val
{
    _property1 = val;
    self.dictionary[@"property1"] = @(val);
}


@end
