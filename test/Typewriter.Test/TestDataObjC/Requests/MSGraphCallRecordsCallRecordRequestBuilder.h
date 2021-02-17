// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphCallRecordRequest, MSGraphSessionRequestBuilder, MSGraphCallRecordSessionsCollectionRequestBuilder, MSGraphEntityType2RequestBuilder, MSGraphCallRecordRecipientsCollectionRequestBuilder, MSGraphCallRecordItemRequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphCallRecordRequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphCallRecordSessionsCollectionRequestBuilder *)sessions;

- (MSGraphSessionRequestBuilder *)sessions:(NSString *)session;

- (MSGraphCallRecordRecipientsCollectionRequestBuilder *)recipients;

- (MSGraphEntityType2RequestBuilder *)recipients:(NSString *)entityType2;

- (MSGraphCallRecordItemRequestBuilder *)itemWithName:(NSString *)name ;


- (MSGraphCallRecordRequest *) request;

- (MSGraphCallRecordRequest *) requestWithOptions:(NSArray *)options;


@end
