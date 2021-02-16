// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphSegmentRequest, MSGraphEntityType3RequestBuilder, MSGraphSegmentRefTypesCollectionWithReferencesRequestBuilder, MSGraphCallRequestBuilder, MSGraphRefTypeRequestBuilder, MSGraphSessionRequestBuilder, MSGraphSessionRefRequestBuilder, MSGraphPhotoRequestBuilder, MSGraphPhotoStreamRequest, MSGraphSegmentForwardRequestBuilder, MSGraphSegmentTestActionRequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphSegmentRequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphSegmentRefTypesCollectionWithReferencesRequestBuilder *)refTypes;

- (MSGraphEntityType3RequestBuilder *)refTypes:(NSString *)entityType3;

- (MSGraphCallRequestBuilder *) refType;

- (MSGraphSessionRequestBuilder *) sessionRef;

- (MSGraphPhotoRequestBuilder *) photo;

- (MSGraphPhotoStreamRequest *) photoValueWithOptions:(NSArray *)options;

- (MSGraphPhotoStreamRequest *) photoValue;

- (MSGraphSegmentForwardRequestBuilder *)forwardWithToRecipients:(NSArray *)toRecipients singleRecipient:(MSGraphRecipient *)singleRecipient multipleSessions:(NSArray *)multipleSessions singleSession:(MSGraphCallRecordsSession *)singleSession comment:(NSString *)comment ;

- (MSGraphSegmentTestActionRequestBuilder *)testActionWithValue:(MSGraphIdentitySet *)value ;


- (MSGraphSegmentRequest *) request;

- (MSGraphSegmentRequest *) requestWithOptions:(NSArray *)options;


@end
