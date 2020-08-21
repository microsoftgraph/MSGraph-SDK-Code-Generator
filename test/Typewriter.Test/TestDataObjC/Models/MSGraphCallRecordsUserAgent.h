// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.





#import "MSObject.h"

@interface MSGraphCallRecordsUserAgent : MSObject

@property (nullable, nonatomic, setter=setHeaderValue:, getter=headerValue) NSString* headerValue;
@property (nullable, nonatomic, setter=setApplicationVersion:, getter=applicationVersion) NSString* applicationVersion;

@end
