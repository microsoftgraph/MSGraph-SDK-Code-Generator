// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphPlannerGroupRequest;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphPlannerGroupRequestBuilder : MSGraphEntityRequestBuilder


- (MSGraphPlannerGroupRequest *) request;

- (MSGraphPlannerGroupRequest *) requestWithOptions:(NSArray *)options;


@end
