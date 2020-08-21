// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphSingletonEntity2()
{
    MSGraphEntityType3* _testSingleNav2;
}
@end

@implementation MSGraphSingletonEntity2

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.singletonEntity2";
    }
    return self;
}
- (MSGraphEntityType3*) testSingleNav2
{
    if(!_testSingleNav2){
        _testSingleNav2 = [[MSGraphEntityType3 alloc] initWithDictionary: self.dictionary[@"testSingleNav2"]];
    }
    return _testSingleNav2;
}

- (void) setTestSingleNav2: (MSGraphEntityType3*) val
{
    _testSingleNav2 = val;
    self.dictionary[@"testSingleNav2"] = val;
}


@end
