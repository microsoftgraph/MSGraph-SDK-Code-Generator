// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsServiceUserAgent()
{
    MSGraphCallRecordsServiceRole* _role;
}
@end

@implementation MSGraphCallRecordsServiceUserAgent

- (MSGraphCallRecordsServiceRole*) role
{
    if(!_role){
        _role = [self.dictionary[@"role"] toMSGraphCallRecordsServiceRole];
    }
    return _role;
}

- (void) setRole: (MSGraphCallRecordsServiceRole*) val
{
    _role = val;
    self.dictionary[@"role"] = val;
}

@end
