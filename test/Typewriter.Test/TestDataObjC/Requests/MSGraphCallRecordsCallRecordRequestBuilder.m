// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphCallRecordRequestBuilder

- (MSGraphCallRecordSessionsCollectionRequestBuilder *)sessions
{
    return [[MSGraphCallRecordSessionsCollectionRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"sessions"]  
                                                                           client:self.client];
}

- (MSGraphSessionRequestBuilder *)sessions:(NSString *)session
{
    return [[self sessions] session:session];
}

- (MSGraphCallRecordRecipientsCollectionRequestBuilder *)recipients
{
    return [[MSGraphCallRecordRecipientsCollectionRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"recipients"]  
                                                                             client:self.client];
}

- (MSGraphEntityType2RequestBuilder *)recipients:(NSString *)entityType2
{
    return [[self recipients] entityType2:entityType2];
}

- (MSGraphCallRecordItemRequestBuilder *)itemWithName:(NSString *)name 
{
    NSURL *actionURL = [self.requestURL URLByAppendingPathComponent:@"microsoft.graph.callRecords.item"];
    return [[MSGraphCallRecordItemRequestBuilder alloc] initWithName:name
                                                                 URL:actionURL
                                                              client:self.client];


}


- (MSGraphCallRecordRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphCallRecordRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphCallRecordRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
