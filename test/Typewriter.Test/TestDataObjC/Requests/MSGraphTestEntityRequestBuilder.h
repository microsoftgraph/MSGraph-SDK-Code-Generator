// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphTestEntityRequest, MSGraphTestTypeRequestBuilder, MSGraphTestNavRequestBuilder, MSGraphEntityType2RequestBuilder, MSGraphTestInvalidNavRequestBuilder, MSGraphEntityType3RequestBuilder, MSGraphTestExplicitNavRequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphTestEntityRequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphTestTypeRequestBuilder *) testNav;

- (MSGraphEntityType2RequestBuilder *) testInvalidNav;

- (MSGraphEntityType3RequestBuilder *) testExplicitNav;


- (MSGraphTestEntityRequest *) request;

- (MSGraphTestEntityRequest *) requestWithOptions:(NSArray *)options;


@end
