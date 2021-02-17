// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphEndpointRequest;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphEndpointRequestBuilder : MSGraphEntityRequestBuilder


- (MSGraphEndpointRequest *) request;

- (MSGraphEndpointRequest *) requestWithOptions:(NSArray *)options;


@end
