// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsClientPlatform.h"
#import "MSGraphCallRecordsProductFamily.h"


#import "MSGraphCallRecordsUserAgent.h"

@interface MSGraphCallRecordsClientUserAgent : MSGraphCallRecordsUserAgent

@property (nonnull, nonatomic, setter=setPlatform:, getter=platform) MSGraphCallRecordsClientPlatform* platform;
@property (nonnull, nonatomic, setter=setProductFamily:, getter=productFamily) MSGraphCallRecordsProductFamily* productFamily;

@end
