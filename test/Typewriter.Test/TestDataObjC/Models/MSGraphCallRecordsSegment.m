// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsSegment()
{
    NSDate* _startDateTime;
    NSDate* _endDateTime;
    MSGraphCallRecordsEndpoint* _caller;
    MSGraphCallRecordsEndpoint* _callee;
    MSGraphCallRecordsFailureInfo* _failureInfo;
    NSArray* _media;
    NSArray* _refTypes;
    MSGraphCall* _refType;
    MSGraphCallRecordsSession* _sessionRef;
    MSGraphCallRecordsPhoto* _photo;
}
@end

@implementation MSGraphCallRecordsSegment

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.callRecords.segment";
    }
    return self;
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

- (NSArray*) media
{
    if(!_media){
        
    NSMutableArray *mediaResult = [NSMutableArray array];
    NSArray *media = self.dictionary[@"media"];

    if ([media isKindOfClass:[NSArray class]]){
        for (id tempMedia in media){
            [mediaResult addObject:tempMedia];
        }
    }

    _media = mediaResult;
        
    }
    return _media;
}

- (void) setMedia: (NSArray*) val
{
    _media = val;
    self.dictionary[@"media"] = val;
}

- (NSArray*) refTypes
{
    if(!_refTypes){
        
    NSMutableArray *refTypesResult = [NSMutableArray array];
    NSArray *refTypes = self.dictionary[@"refTypes"];

    if ([refTypes isKindOfClass:[NSArray class]]){
        for (id tempEntityType3 in refTypes){
            [refTypesResult addObject:tempEntityType3];
        }
    }

    _refTypes = refTypesResult;
        
    }
    return _refTypes;
}

- (void) setRefTypes: (NSArray*) val
{
    _refTypes = val;
    self.dictionary[@"refTypes"] = val;
}

- (MSGraphCall*) refType
{
    if(!_refType){
        _refType = [[MSGraphCall alloc] initWithDictionary: self.dictionary[@"refType"]];
    }
    return _refType;
}

- (void) setRefType: (MSGraphCall*) val
{
    _refType = val;
    self.dictionary[@"refType"] = val;
}

- (MSGraphCallRecordsSession*) sessionRef
{
    if(!_sessionRef){
        _sessionRef = [[MSGraphCallRecordsSession alloc] initWithDictionary: self.dictionary[@"sessionRef"]];
    }
    return _sessionRef;
}

- (void) setSessionRef: (MSGraphCallRecordsSession*) val
{
    _sessionRef = val;
    self.dictionary[@"sessionRef"] = val;
}

- (MSGraphCallRecordsPhoto*) photo
{
    if(!_photo){
        _photo = [[MSGraphCallRecordsPhoto alloc] initWithDictionary: self.dictionary[@"photo"]];
    }
    return _photo;
}

- (void) setPhoto: (MSGraphCallRecordsPhoto*) val
{
    _photo = val;
    self.dictionary[@"photo"] = val;
}


@end
