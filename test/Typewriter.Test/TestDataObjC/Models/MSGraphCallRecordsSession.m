// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsSession()
{
    NSArray* _modalities;
    NSDate* _startDateTime;
    NSDate* _endDateTime;
    MSGraphCallRecordsEndpoint* _caller;
    MSGraphCallRecordsEndpoint* _callee;
    MSGraphCallRecordsFailureInfo* _failureInfo;
    NSArray* _segments;
}
@end

@implementation MSGraphCallRecordsSession

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.callRecords.session";
    }
    return self;
}
- (NSArray*) modalities
{
    if(!_modalities){
        
    NSMutableArray *modalitiesResult = [NSMutableArray array];
    NSArray *modalities = self.dictionary[@"modalities"];

    if ([modalities isKindOfClass:[NSArray class]]){
        for (id tempModality in modalities){
            [modalitiesResult addObject:tempModality];
        }
    }

    _modalities = modalitiesResult;
        
    }
    return _modalities;
}

- (void) setModalities: (NSArray*) val
{
    _modalities = val;
    self.dictionary[@"modalities"] = val;
}

- (NSDate*) startDateTime
{
    if(!_startDateTime){
        _startDateTime = [NSDate ms_dateFromString: self.dictionary[@"startDateTime"]];
    }
    return _startDateTime;
}

- (void) setStartDateTime: (NSDate*) val
{
    _startDateTime = val;
    self.dictionary[@"startDateTime"] = [val ms_toString];
}

- (NSDate*) endDateTime
{
    if(!_endDateTime){
        _endDateTime = [NSDate ms_dateFromString: self.dictionary[@"endDateTime"]];
    }
    return _endDateTime;
}

- (void) setEndDateTime: (NSDate*) val
{
    _endDateTime = val;
    self.dictionary[@"endDateTime"] = [val ms_toString];
}

- (MSGraphCallRecordsEndpoint*) caller
{
    if(!_caller){
        _caller = [[MSGraphCallRecordsEndpoint alloc] initWithDictionary: self.dictionary[@"caller"]];
    }
    return _caller;
}

- (void) setCaller: (MSGraphCallRecordsEndpoint*) val
{
    _caller = val;
    self.dictionary[@"caller"] = val;
}

- (MSGraphCallRecordsEndpoint*) callee
{
    if(!_callee){
        _callee = [[MSGraphCallRecordsEndpoint alloc] initWithDictionary: self.dictionary[@"callee"]];
    }
    return _callee;
}

- (void) setCallee: (MSGraphCallRecordsEndpoint*) val
{
    _callee = val;
    self.dictionary[@"callee"] = val;
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

- (NSArray*) segments
{
    if(!_segments){
        
    NSMutableArray *segmentsResult = [NSMutableArray array];
    NSArray *segments = self.dictionary[@"segments"];

    if ([segments isKindOfClass:[NSArray class]]){
        for (id tempSegment in segments){
            [segmentsResult addObject:tempSegment];
        }
    }

    _segments = segmentsResult;
        
    }
    return _segments;
}

- (void) setSegments: (NSArray*) val
{
    _segments = val;
    self.dictionary[@"segments"] = val;
}


@end
