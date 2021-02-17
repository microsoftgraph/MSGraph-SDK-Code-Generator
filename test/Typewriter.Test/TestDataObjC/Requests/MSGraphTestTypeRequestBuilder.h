// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphTestTypeRequest, MSGraphTestTypeQueryRequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphTestTypeRequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphTestTypeQueryRequestBuilder *)queryWithRequests:(NSArray *)requests ;


- (MSGraphTestTypeRequest *) request;

- (MSGraphTestTypeRequest *) requestWithOptions:(NSArray *)options;


@end
