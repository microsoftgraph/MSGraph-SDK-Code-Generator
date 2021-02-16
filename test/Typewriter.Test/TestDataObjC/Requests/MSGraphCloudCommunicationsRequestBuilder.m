// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@implementation MSGraphCloudCommunicationsRequestBuilder

- (MSGraphCloudCommunicationsCallsCollectionRequestBuilder *)calls
{
    return [[MSGraphCloudCommunicationsCallsCollectionRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"calls"]  
                                                                                 client:self.client];
}

- (MSGraphCallRequestBuilder *)calls:(NSString *)call
{
    return [[self calls] call:call];
}

- (MSGraphCloudCommunicationsCallRecordsCollectionRequestBuilder *)callRecords
{
    return [[MSGraphCloudCommunicationsCallRecordsCollectionRequestBuilder alloc] initWithURL:[self.requestURL URLByAppendingPathComponent:@"callRecords"]  
                                                                                       client:self.client];
}

- (MSGraphCallRecordRequestBuilder *)callRecords:(NSString *)callRecord
{
    return [[self callRecords] callRecord:callRecord];
}


- (MSGraphCloudCommunicationsRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphCloudCommunicationsRequest *) requestWithOptions:(NSArray *)options
{
    return [[MSGraphCloudCommunicationsRequest alloc] initWithURL:self.requestURL options:options client:self.client];
}


@end
