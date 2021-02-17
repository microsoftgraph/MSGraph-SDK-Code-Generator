// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphSingletonEntity1Request, MSGraphTestTypeRequestBuilder, MSGraphTestSingleNavRequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphSingletonEntity1RequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphTestTypeRequestBuilder *) testSingleNav;


- (MSGraphSingletonEntity1Request *) request;

- (MSGraphSingletonEntity1Request *) requestWithOptions:(NSArray *)options;


@end
