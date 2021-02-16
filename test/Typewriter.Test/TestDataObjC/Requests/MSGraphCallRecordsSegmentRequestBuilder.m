// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphSegmentRequestBuilder

- (MSGraphSegmentRefTypesCollectionWithReferencesRequestBuilder *)refTypes
{
    return [[MSGraphSegmentRefTypesCollectionWithReferencesRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"refTypes"]  
                                                                                      client:self.client];
}

- (MSGraphEntityType3RequestBuilder *)refTypes:(NSString *)entityType3
{
    return [[self refTypes] entityType3:entityType3];
}

-(MSGraphCallRequestBuilder *)refType
{
    return [[MSGraphCallRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"refType"] client:self.client];

}

-(MSGraphSessionRequestBuilder *)sessionRef
{
    return [[MSGraphSessionRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"sessionRef"] client:self.client];

}

-(MSGraphPhotoRequestBuilder *)photo
{
    return [[MSGraphPhotoRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"photo"] client:self.client];

}

- (MSGraphPhotoStreamRequest *) photoValueWithOptions:(NSArray *)options
{
    NSURL *photoURL = [self.requestURL URLByAppendingPathComponent:@"photo/$value"];
    return [[MSGraphPhotoStreamRequest alloc] initWithURL:photoURL options:options client:self.client];
}

- (MSGraphPhotoStreamRequest *) photoValue
{
    return [self photoValueWithOptions:nil];
}

- (MSGraphSegmentForwardRequestBuilder *)forwardWithToRecipients:(NSArray *)toRecipients singleRecipient:(MSGraphRecipient *)singleRecipient multipleSessions:(NSArray *)multipleSessions singleSession:(MSGraphCallRecordsSession *)singleSession comment:(NSString *)comment 
{
    NSURL *actionURL = [self.requestURL URLByAppendingPathComponent:@"microsoft.graph.callRecords.forward"];
    return [[MSGraphSegmentForwardRequestBuilder alloc] initWithToRecipients:toRecipients
                                                             singleRecipient:singleRecipient
                                                            multipleSessions:multipleSessions
                                                               singleSession:singleSession
                                                                     comment:comment
                                                                         URL:actionURL
                                                                      client:self.client];


}

- (MSGraphSegmentTestActionRequestBuilder *)testActionWithValue:(MSGraphIdentitySet *)value 
{
    NSURL *actionURL = [self.requestURL URLByAppendingPathComponent:@"microsoft.graph.callRecords.testAction"];
    return [[MSGraphSegmentTestActionRequestBuilder alloc] initWithValue:value
                                                                     URL:actionURL
                                                                  client:self.client];


}


- (MSGraphSegmentRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphSegmentRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphSegmentRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
