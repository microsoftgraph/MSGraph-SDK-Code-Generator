// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphEntityType3()
{
}
@end

@implementation MSGraphEntityType3

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.entityType3";
    }
    return self;
}

@end
