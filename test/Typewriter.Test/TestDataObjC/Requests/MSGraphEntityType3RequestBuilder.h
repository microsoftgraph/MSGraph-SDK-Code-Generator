// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphEntityType3Request, MSGraphEntityType3ForwardRequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphEntityType3RequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphEntityType3ForwardRequestBuilder *)forwardWithToRecipients:(NSArray *)toRecipients singleRecipient:(MSGraphRecipient *)singleRecipient multipleSessions:(NSArray *)multipleSessions singleSession:(MSGraphCallRecordsSession *)singleSession comment:(NSString *)comment ;


- (MSGraphEntityType3Request *) request;

- (MSGraphEntityType3Request *) requestWithOptions:(NSArray *)options;


@end
