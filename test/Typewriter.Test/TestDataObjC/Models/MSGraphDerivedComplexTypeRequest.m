// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphDerivedComplexTypeRequest()
{
    NSString* _property1;
    NSString* _property2;
    MSGraphEnum1* _enumProperty;
}
@end

@implementation MSGraphDerivedComplexTypeRequest

- (NSString*) property1
{
    if([[NSNull null] isEqual:self.dictionary[@"property1"]])
    {
        return nil;
    }   
    return self.dictionary[@"property1"];
}

- (void) setProperty1: (NSString*) val
{
    self.dictionary[@"property1"] = val;
}

- (NSString*) property2
{
    if([[NSNull null] isEqual:self.dictionary[@"property2"]])
    {
        return nil;
    }   
    return self.dictionary[@"property2"];
}

- (void) setProperty2: (NSString*) val
{
    self.dictionary[@"property2"] = val;
}

- (MSGraphEnum1*) enumProperty
{
    if(!_enumProperty){
        _enumProperty = [self.dictionary[@"enumProperty"] toMSGraphEnum1];
    }
    return _enumProperty;
}

- (void) setEnumProperty: (MSGraphEnum1*) val
{
    _enumProperty = val;
    self.dictionary[@"enumProperty"] = val;
}

@end
