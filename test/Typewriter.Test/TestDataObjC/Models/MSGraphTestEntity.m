// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphTestEntity()
{
    MSGraphTestType* _testNav;
    MSGraphEntityType2* _testInvalidNav;
    MSGraphEntityType3* _testExplicitNav;
}
@end

@implementation MSGraphTestEntity

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.testEntity";
    }
    return self;
}
- (MSGraphTestType*) testNav
{
    if(!_testNav){
        _testNav = [[MSGraphTestType alloc] initWithDictionary: self.dictionary[@"testNav"]];
    }
    return _testNav;
}

- (void) setTestNav: (MSGraphTestType*) val
{
    _testNav = val;
    self.dictionary[@"testNav"] = val;
}

- (MSGraphEntityType2*) testInvalidNav
{
    if(!_testInvalidNav){
        _testInvalidNav = [[MSGraphEntityType2 alloc] initWithDictionary: self.dictionary[@"testInvalidNav"]];
    }
    return _testInvalidNav;
}

- (void) setTestInvalidNav: (MSGraphEntityType2*) val
{
    _testInvalidNav = val;
    self.dictionary[@"testInvalidNav"] = val;
}

- (MSGraphEntityType3*) testExplicitNav
{
    if(!_testExplicitNav){
        _testExplicitNav = [[MSGraphEntityType3 alloc] initWithDictionary: self.dictionary[@"testExplicitNav"]];
    }
    return _testExplicitNav;
}

- (void) setTestExplicitNav: (MSGraphEntityType3*) val
{
    _testExplicitNav = val;
    self.dictionary[@"testExplicitNav"] = val;
}


@end
