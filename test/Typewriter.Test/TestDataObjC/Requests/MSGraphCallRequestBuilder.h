// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphCallRequest;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphCallRequestBuilder : MSGraphEntityRequestBuilder


- (MSGraphCallRequest *) request;

- (MSGraphCallRequest *) requestWithOptions:(NSArray *)options;


@end
