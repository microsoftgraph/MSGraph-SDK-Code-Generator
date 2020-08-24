// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsEndpoint()
{
    MSGraphCallRecordsUserAgent* _userAgent;
}
@end

@implementation MSGraphCallRecordsEndpoint

- (MSGraphCallRecordsUserAgent*) userAgent
{
    if(!_userAgent){
        _userAgent = [[MSGraphCallRecordsUserAgent alloc] initWithDictionary: self.dictionary[@"userAgent"]];
    }
    return _userAgent;
}

- (void) setUserAgent: (MSGraphCallRecordsUserAgent*) val
{
    _userAgent = val;
    self.dictionary[@"userAgent"] = val;
}

@end
