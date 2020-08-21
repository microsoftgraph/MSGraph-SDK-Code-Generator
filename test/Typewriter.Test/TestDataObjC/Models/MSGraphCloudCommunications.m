// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCloudCommunications()
{
    NSArray* _calls;
    NSArray* _callRecords;
}
@end

@implementation MSGraphCloudCommunications

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.cloudCommunications";
    }
    return self;
}
- (NSArray*) calls
{
    if(!_calls){
        
    NSMutableArray *callsResult = [NSMutableArray array];
    NSArray *calls = self.dictionary[@"calls"];

    if ([calls isKindOfClass:[NSArray class]]){
        for (id tempCall in calls){
            [callsResult addObject:tempCall];
        }
    }

    _calls = callsResult;
        
    }
    return _calls;
}

- (void) setCalls: (NSArray*) val
{
    _calls = val;
    self.dictionary[@"calls"] = val;
}

- (NSArray*) callRecords
{
    if(!_callRecords){
        
    NSMutableArray *callRecordsResult = [NSMutableArray array];
    NSArray *callRecords = self.dictionary[@"callRecords"];

    if ([callRecords isKindOfClass:[NSArray class]]){
        for (id tempCallRecord in callRecords){
            [callRecordsResult addObject:tempCallRecord];
        }
    }

    _callRecords = callRecordsResult;
        
    }
    return _callRecords;
}

- (void) setCallRecords: (NSArray*) val
{
    _callRecords = val;
    self.dictionary[@"callRecords"] = val;
}


@end
