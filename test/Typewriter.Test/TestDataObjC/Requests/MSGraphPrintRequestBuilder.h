// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphPrintRequest;


#import "MSGraphModels.h"
#import "MSRequestBuilder.h"


@interface MSGraphPrintRequestBuilder : MSRequestBuilder


- (MSGraphPrintRequest *) request;

- (MSGraphPrintRequest *) requestWithOptions:(NSArray *)options;


@end
