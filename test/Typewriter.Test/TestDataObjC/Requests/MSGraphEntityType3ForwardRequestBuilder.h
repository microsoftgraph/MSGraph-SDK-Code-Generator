// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"
#import "MSGraphModels.h"
#import "MSRequestBuilder.h"

@class MSGraphEntityType3ForwardRequest;

@interface MSGraphEntityType3ForwardRequestBuilder : MSRequestBuilder

- (instancetype)initWithToRecipients:(NSArray *)toRecipients singleRecipient:(MSGraphRecipient *)singleRecipient multipleSessions:(NSArray *)multipleSessions singleSession:(MSGraphCallRecordsSession *)singleSession comment:(NSString *)comment URL:(NSURL *)url client:(ODataBaseClient*)client;

- (MSGraphEntityType3ForwardRequest *)request;

- (MSGraphEntityType3ForwardRequest *)requestWithOptions:(NSArray *)options;

@end
