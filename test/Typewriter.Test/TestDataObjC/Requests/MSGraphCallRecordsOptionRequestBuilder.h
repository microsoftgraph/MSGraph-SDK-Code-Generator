// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphOptionRequest;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphOptionRequestBuilder : MSGraphEntityRequestBuilder


- (MSGraphOptionRequest *) request;

- (MSGraphOptionRequest *) requestWithOptions:(NSArray *)options;


@end
