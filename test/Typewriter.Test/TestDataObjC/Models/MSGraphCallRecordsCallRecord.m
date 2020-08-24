// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsCallRecord()
{
    int64_t _version;
    MSGraphCallRecordsCallType* _type;
    NSArray* _modalities;
    NSDate* _lastModifiedDateTime;
    NSDate* _startDateTime;
    NSDate* _endDateTime;
    MSGraphIdentitySet* _organizer;
    NSArray* _participants;
    NSString* _joinWebUrl;
    NSArray* _sessions;
    NSArray* _recipients;
}
@end

@implementation MSGraphCallRecordsCallRecord

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.callRecords.callRecord";
    }
    return self;
}
- (int64_t) version
{
    _version = [self.dictionary[@"version"] longLongValue];
    return _version;
}

- (void) setVersion: (int64_t) val
{
    _version = val;
    self.dictionary[@"version"] = @(val);
}

- (MSGraphCallRecordsCallType*) type
{
    if(!_type){
        _type = [self.dictionary[@"type"] toMSGraphCallRecordsCallType];
    }
    return _type;
}

- (void) setType: (MSGraphCallRecordsCallType*) val
{
    _type = val;
    self.dictionary[@"type"] = val;
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

- (NSDate*) lastModifiedDateTime
{
    if(!_lastModifiedDateTime){
        _lastModifiedDateTime = [NSDate ms_dateFromString: self.dictionary[@"lastModifiedDateTime"]];
    }
    return _lastModifiedDateTime;
}

- (void) setLastModifiedDateTime: (NSDate*) val
{
    _lastModifiedDateTime = val;
    self.dictionary[@"lastModifiedDateTime"] = [val ms_toString];
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

- (MSGraphIdentitySet*) organizer
{
    if(!_organizer){
        _organizer = [[MSGraphIdentitySet alloc] initWithDictionary: self.dictionary[@"organizer"]];
    }
    return _organizer;
}

- (void) setOrganizer: (MSGraphIdentitySet*) val
{
    _organizer = val;
    self.dictionary[@"organizer"] = val;
}

- (NSArray*) participants
{
    if(!_participants){
        
    NSMutableArray *participantsResult = [NSMutableArray array];
    NSArray *participants = self.dictionary[@"participants"];

    if ([participants isKindOfClass:[NSArray class]]){
        for (id tempIdentitySet in participants){
            [participantsResult addObject:tempIdentitySet];
        }
    }

    _participants = participantsResult;
        
    }
    return _participants;
}

- (void) setParticipants: (NSArray*) val
{
    _participants = val;
    self.dictionary[@"participants"] = val;
}

- (NSString*) joinWebUrl
{
    if([[NSNull null] isEqual:self.dictionary[@"joinWebUrl"]])
    {
        return nil;
    }   
    return self.dictionary[@"joinWebUrl"];
}

- (void) setJoinWebUrl: (NSString*) val
{
    self.dictionary[@"joinWebUrl"] = val;
}

- (NSArray*) sessions
{
    if(!_sessions){
        
    NSMutableArray *sessionsResult = [NSMutableArray array];
    NSArray *sessions = self.dictionary[@"sessions"];

    if ([sessions isKindOfClass:[NSArray class]]){
        for (id tempSession in sessions){
            [sessionsResult addObject:tempSession];
        }
    }

    _sessions = sessionsResult;
        
    }
    return _sessions;
}

- (void) setSessions: (NSArray*) val
{
    _sessions = val;
    self.dictionary[@"sessions"] = val;
}

- (NSArray*) recipients
{
    if(!_recipients){
        
    NSMutableArray *recipientsResult = [NSMutableArray array];
    NSArray *recipients = self.dictionary[@"recipients"];

    if ([recipients isKindOfClass:[NSArray class]]){
        for (id tempEntityType2 in recipients){
            [recipientsResult addObject:tempEntityType2];
        }
    }

    _recipients = recipientsResult;
        
    }
    return _recipients;
}

- (void) setRecipients: (NSArray*) val
{
    _recipients = val;
    self.dictionary[@"recipients"] = val;
}


@end
