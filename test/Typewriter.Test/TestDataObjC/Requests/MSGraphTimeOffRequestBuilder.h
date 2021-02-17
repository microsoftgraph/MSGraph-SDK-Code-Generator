// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphTimeOffRequest;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphTimeOffRequestBuilder : MSGraphEntityRequestBuilder


- (MSGraphTimeOffRequest *) request;

- (MSGraphTimeOffRequest *) requestWithOptions:(NSArray *)options;


@end
