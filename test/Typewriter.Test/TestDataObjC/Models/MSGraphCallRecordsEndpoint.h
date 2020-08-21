// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphCallRecordsUserAgent; 


#import "MSObject.h"

@interface MSGraphCallRecordsEndpoint : MSObject

@property (nullable, nonatomic, setter=setUserAgent:, getter=userAgent) MSGraphCallRecordsUserAgent* userAgent;

@end
