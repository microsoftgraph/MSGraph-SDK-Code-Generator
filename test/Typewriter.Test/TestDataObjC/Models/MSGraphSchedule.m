// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphSchedule()
{
    BOOL _enabled;
    NSArray* _timesOff;
    NSArray* _timeOffRequests;
}
@end

@implementation MSGraphSchedule

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.schedule";
    }
    return self;
}
- (BOOL) enabled
{
    _enabled = [self.dictionary[@"enabled"] boolValue];
    return _enabled;
}

- (void) setEnabled: (BOOL) val
{
    _enabled = val;
    self.dictionary[@"enabled"] = @(val);
}

- (NSArray*) timesOff
{
    if(!_timesOff){
        
    NSMutableArray *timesOffResult = [NSMutableArray array];
    NSArray *timesOff = self.dictionary[@"timesOff"];

    if ([timesOff isKindOfClass:[NSArray class]]){
        for (id tempTimeOff in timesOff){
            [timesOffResult addObject:tempTimeOff];
        }
    }

    _timesOff = timesOffResult;
        
    }
    return _timesOff;
}

- (void) setTimesOff: (NSArray*) val
{
    _timesOff = val;
    self.dictionary[@"timesOff"] = val;
}

- (NSArray*) timeOffRequests
{
    if(!_timeOffRequests){
        
    NSMutableArray *timeOffRequestsResult = [NSMutableArray array];
    NSArray *timeOffRequests = self.dictionary[@"timeOffRequests"];

    if ([timeOffRequests isKindOfClass:[NSArray class]]){
        for (id tempTimeOffRequest in timeOffRequests){
            [timeOffRequestsResult addObject:tempTimeOffRequest];
        }
    }

    _timeOffRequests = timeOffRequestsResult;
        
    }
    return _timeOffRequests;
}

- (void) setTimeOffRequests: (NSArray*) val
{
    _timeOffRequests = val;
    self.dictionary[@"timeOffRequests"] = val;
}


@end
