// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphTestType()
{
    MSGraphDerivedComplexTypeRequest* _propertyAlpha;
    NSArray* _primitiveCollection;
}
@end

@implementation MSGraphTestType

- (id) init
{
    if (self = [super init]) {
        self.oDataType = @"#microsoft.graph.testType";
    }
    return self;
}
- (MSGraphDerivedComplexTypeRequest*) propertyAlpha
{
    if(!_propertyAlpha){
        _propertyAlpha = [[MSGraphDerivedComplexTypeRequest alloc] initWithDictionary: self.dictionary[@"propertyAlpha"]];
    }
    return _propertyAlpha;
}

- (void) setPropertyAlpha: (MSGraphDerivedComplexTypeRequest*) val
{
    _propertyAlpha = val;
    self.dictionary[@"propertyAlpha"] = val;
}

- (NSArray*) primitiveCollection
{
    if([[NSNull null] isEqual:self.dictionary[@"primitiveCollection"]])
    {
        return nil;
    }   
    return self.dictionary[@"primitiveCollection"];
}

- (void) setPrimitiveCollection: (NSArray*) val
{
    self.dictionary[@"primitiveCollection"] = val;
}


@end
