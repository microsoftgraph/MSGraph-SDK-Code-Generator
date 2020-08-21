// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsSingletonEntity1()
{
    MSGraphTestType* _testSingleNav;
}
@end

@implementation MSGraphCallRecordsSingletonEntity1

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.callRecords.singletonEntity1";
    }
    return self;
}
- (MSGraphTestType*) testSingleNav
{
    if(!_testSingleNav){
        _testSingleNav = [[MSGraphTestType alloc] initWithDictionary: self.dictionary[@"testSingleNav"]];
    }
    return _testSingleNav;
}

- (void) setTestSingleNav: (MSGraphTestType*) val
{
    _testSingleNav = val;
    self.dictionary[@"testSingleNav"] = val;
}


@end
