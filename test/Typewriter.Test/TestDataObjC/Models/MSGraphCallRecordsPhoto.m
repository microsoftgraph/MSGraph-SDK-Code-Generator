// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsPhoto()
{
    MSGraphCallRecordsFailureInfo* _failureInfo;
    MSGraphCallRecordsOption* _option;
}
@end

@implementation MSGraphCallRecordsPhoto

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.callRecords.photo";
    }
    return self;
}
- (MSGraphCallRecordsFailureInfo*) failureInfo
{
    if(!_failureInfo){
        _failureInfo = [[MSGraphCallRecordsFailureInfo alloc] initWithDictionary: self.dictionary[@"failureInfo"]];
    }
    return _failureInfo;
}

- (void) setFailureInfo: (MSGraphCallRecordsFailureInfo*) val
{
    _failureInfo = val;
    self.dictionary[@"failureInfo"] = val;
}

- (MSGraphCallRecordsOption*) option
{
    if(!_option){
        _option = [[MSGraphCallRecordsOption alloc] initWithDictionary: self.dictionary[@"option"]];
    }
    return _option;
}

- (void) setOption: (MSGraphCallRecordsOption*) val
{
    _option = val;
    self.dictionary[@"option"] = val;
}


@end
