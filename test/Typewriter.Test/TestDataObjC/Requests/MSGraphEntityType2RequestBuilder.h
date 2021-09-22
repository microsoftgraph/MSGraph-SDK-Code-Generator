// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphEntityType2Request;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphEntityType2RequestBuilder : MSGraphEntityRequestBuilder


- (MSGraphEntityType2Request *) request;

- (MSGraphEntityType2Request *) requestWithOptions:(NSArray *)options;


@end
