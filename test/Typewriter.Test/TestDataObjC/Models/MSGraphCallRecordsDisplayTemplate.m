// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsDisplayTemplate()
{
    NSString* _displayTemplateId;
    NSDictionary* _layout;
    int32_t _priority;
}
@end

@implementation MSGraphCallRecordsDisplayTemplate

- (NSString*) displayTemplateId
{
    return self.dictionary[@"id"];
}

- (void) setDisplayTemplateId: (NSString*) val
{
    self.dictionary[@"id"] = val;
}

- (NSDictionary*) layout
{
    if(!_layout){
        _layout = [[NSDictionary alloc] initWithDictionary: self.dictionary[@"layout"]];
    }
    return _layout;
}

- (void) setLayout: (NSDictionary*) val
{
    _layout = val;
    self.dictionary[@"layout"] = val;
}

- (int32_t) priority
{
    _priority = [self.dictionary[@"priority"] intValue];
    return _priority;
}

- (void) setPriority: (int32_t) val
{
    _priority = val;
    self.dictionary[@"priority"] = @(val);
}

@end
