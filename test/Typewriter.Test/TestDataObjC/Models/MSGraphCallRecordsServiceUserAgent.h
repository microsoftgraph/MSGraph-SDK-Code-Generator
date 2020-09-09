// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsServiceRole.h"


#import "MSGraphCallRecordsUserAgent.h"

@interface MSGraphCallRecordsServiceUserAgent : MSGraphCallRecordsUserAgent

@property (nonnull, nonatomic, setter=setRole:, getter=role) MSGraphCallRecordsServiceRole* role;

@end
