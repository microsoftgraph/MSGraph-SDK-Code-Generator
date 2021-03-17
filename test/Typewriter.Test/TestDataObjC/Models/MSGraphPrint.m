// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphPrint()
{
    NSString* _settings;
}
@end

@implementation MSGraphPrint

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.print";
    }
    return self;
}
- (NSString*) oDataType
{
    return self.dictionary[@"@odata.type"];
}
- (void) setODataType: (NSString*) val
{
    self.dictionary[@"@odata.type"] = val;
}
- (NSString*) oDataEtag
{
    return self.dictionary[@"@odata.etag"];
}
- (void) setODataEtag: (NSString*) val
{
    self.dictionary[@"@odata.etag"] = val;
}
- (NSString*) settings
{
    if([[NSNull null] isEqual:self.dictionary[@"settings"]])
    {
        return nil;
    }   
    return self.dictionary[@"settings"];
}

- (void) setSettings: (NSString*) val
{
    self.dictionary[@"settings"] = val;
}


@end
