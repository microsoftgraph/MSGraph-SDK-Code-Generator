// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphCloudCommunicationsRequest, MSGraphCallRequestBuilder, MSGraphCloudCommunicationsCallsCollectionRequestBuilder, MSGraphCallRecordRequestBuilder, MSGraphCloudCommunicationsCallRecordsCollectionRequestBuilder;


#import "MSGraphModels.h"
#import "MSGraphEntityRequestBuilder.h"


@interface MSGraphCloudCommunicationsRequestBuilder : MSGraphEntityRequestBuilder

- (MSGraphCloudCommunicationsCallsCollectionRequestBuilder *)calls;

- (MSGraphCallRequestBuilder *)calls:(NSString *)call;

- (MSGraphCloudCommunicationsCallRecordsCollectionRequestBuilder *)callRecords;

- (MSGraphCallRecordRequestBuilder *)callRecords:(NSString *)callRecord;


- (MSGraphCloudCommunicationsRequest *) request;

- (MSGraphCloudCommunicationsRequest *) requestWithOptions:(NSArray *)options;


@end
