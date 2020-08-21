// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphClient


-(MSGraphTestTypesCollectionRequestBuilder *)testTypes
{
    return [[MSGraphTestTypesCollectionRequestBuilder alloc] initWithURL:[self.baseURL URLByAppendingPathComponent:@"testTypes"] 
                                                                   client:self];
}

-(MSGraphEntityType3RequestBuilder*)testTypes:(NSString*)entityType3
{
    return [[self testTypes] entityType3:entityType3];
}

    -(MSGraphSingletonEntity1RequestBuilder *) singletonProperty1
    {
    return [[MSGraphSingletonEntity1RequestBuilder alloc] initWithURL:[self.baseURL URLByAppendingPathComponent:@"singletonProperty1"] 
                                                              client:self];
    }
    -(MSGraphSingletonEntity2RequestBuilder *) singletonProperty2
    {
    return [[MSGraphSingletonEntity2RequestBuilder alloc] initWithURL:[self.baseURL URLByAppendingPathComponent:@"singletonProperty2"] 
                                                              client:self];
    }
    -(MSGraphSingletonEntity1RequestBuilder *) singletonProperty3
    {
    return [[MSGraphSingletonEntity1RequestBuilder alloc] initWithURL:[self.baseURL URLByAppendingPathComponent:@"singletonProperty3"] 
                                                                         client:self];
    }

@end
