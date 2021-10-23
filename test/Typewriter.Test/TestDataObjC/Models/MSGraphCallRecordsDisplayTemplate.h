// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class NSDictionary; 


#import "MSObject.h"

@interface MSGraphCallRecordsDisplayTemplate : MSObject

@property (nonnull, nonatomic, setter=setDisplayTemplateId:, getter=displayTemplateId) NSString* displayTemplateId;
@property (nonnull, nonatomic, setter=setLayout:, getter=layout) NSDictionary* layout;
@property (nonatomic, setter=setPriority:, getter=priority) int32_t priority;

@end
