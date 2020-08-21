// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.


@class MSGraphIdentitySet, MSGraphCallRecordsSession, MSGraphEntityType2; 
#import "MSGraphCallRecordsCallType.h"
#import "MSGraphCallRecordsModality.h"


#import "MSGraphEntity.h"

@interface MSGraphCallRecordsCallRecord : MSGraphEntity

  @property (nonatomic, setter=setVersion:, getter=version) int64_t version;
    @property (nonnull, nonatomic, setter=setType:, getter=type) MSGraphCallRecordsCallType* type;
    @property (nonnull, nonatomic, setter=setModalities:, getter=modalities) NSArray* modalities;
    @property (nonnull, nonatomic, setter=setLastModifiedDateTime:, getter=lastModifiedDateTime) NSDate* lastModifiedDateTime;
    @property (nonnull, nonatomic, setter=setStartDateTime:, getter=startDateTime) NSDate* startDateTime;
    @property (nonnull, nonatomic, setter=setEndDateTime:, getter=endDateTime) NSDate* endDateTime;
    @property (nullable, nonatomic, setter=setOrganizer:, getter=organizer) MSGraphIdentitySet* organizer;
    @property (nullable, nonatomic, setter=setParticipants:, getter=participants) NSArray* participants;
    @property (nullable, nonatomic, setter=setJoinWebUrl:, getter=joinWebUrl) NSString* joinWebUrl;
    @property (nullable, nonatomic, setter=setSessions:, getter=sessions) NSArray* sessions;
    @property (nullable, nonatomic, setter=setRecipients:, getter=recipients) NSArray* recipients;
  
@end
