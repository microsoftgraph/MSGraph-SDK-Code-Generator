// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphPhotoRequest;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphPhotoRequestBuilder : MSGraphEntityRequestBuilder


- (MSGraphPhotoRequest *) request;

- (MSGraphPhotoRequest *) requestWithOptions:(NSArray *)options;


@end
