// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphSingletonEntity2Request, MSGraphEntityType3RequestBuilder, MSGraphTestSingleNav2RequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphSingletonEntity2RequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphEntityType3RequestBuilder *) testSingleNav2;


- (MSGraphSingletonEntity2Request *) request;

- (MSGraphSingletonEntity2Request *) requestWithOptions:(NSArray *)options;


@end
