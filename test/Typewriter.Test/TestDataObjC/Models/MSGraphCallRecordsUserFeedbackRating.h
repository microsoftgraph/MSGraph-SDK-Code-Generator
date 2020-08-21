// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#include <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, MSGraphCallRecordsUserFeedbackRatingValue){

	MSGraphCallRecordsUserFeedbackRatingNotRated = 0,
	MSGraphCallRecordsUserFeedbackRatingBad = 1,
    MSGraphCallRecordsUserFeedbackRatingEndOfEnum
};

@interface MSGraphCallRecordsUserFeedbackRating : NSObject

+(MSGraphCallRecordsUserFeedbackRating*) notRated;
+(MSGraphCallRecordsUserFeedbackRating*) bad;

+(MSGraphCallRecordsUserFeedbackRating*) UnknownEnumValue;

+(MSGraphCallRecordsUserFeedbackRating*) userFeedbackRatingWithEnumValue:(MSGraphCallRecordsUserFeedbackRatingValue)val;
-(NSString*) ms_toString;

@property (nonatomic, readonly) MSGraphCallRecordsUserFeedbackRatingValue enumValue;

@end


@interface NSString (MSGraphCallRecordsUserFeedbackRating)

- (MSGraphCallRecordsUserFeedbackRating*) toMSGraphCallRecordsUserFeedbackRating;

@end
