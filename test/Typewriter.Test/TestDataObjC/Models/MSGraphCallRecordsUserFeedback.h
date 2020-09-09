// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphCallRecordsFeedbackTokenSet; 
#import "MSGraphCallRecordsUserFeedbackRating.h"


#import "MSObject.h"

@interface MSGraphCallRecordsUserFeedback : MSObject

@property (nullable, nonatomic, setter=setText:, getter=text) NSString* text;
@property (nonnull, nonatomic, setter=setRating:, getter=rating) MSGraphCallRecordsUserFeedbackRating* rating;
@property (nullable, nonatomic, setter=setTokens:, getter=tokens) MSGraphCallRecordsFeedbackTokenSet* tokens;

@end
