// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphSessionRequest, MSGraphSegmentRequestBuilder, MSGraphSessionSegmentsCollectionRequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphSessionRequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphSessionSegmentsCollectionRequestBuilder *)segments;

- (MSGraphSegmentRequestBuilder *)segments:(NSString *)segment;


- (MSGraphSessionRequest *) request;

- (MSGraphSessionRequest *) requestWithOptions:(NSArray *)options;


@end
