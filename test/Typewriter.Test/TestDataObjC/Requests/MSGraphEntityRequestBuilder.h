// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphEntityRequest;


#import "MSGraphModels.h"
#import "MSRequestBuilder.h"


@interface MSGraphEntityRequestBuilder : MSRequestBuilder


- (MSGraphEntityRequest *) request;

- (MSGraphEntityRequest *) requestWithOptions:(NSArray *)options;


@end
