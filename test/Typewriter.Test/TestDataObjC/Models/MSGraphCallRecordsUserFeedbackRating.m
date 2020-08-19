// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphCallRecordsUserFeedbackRating.h"

@interface MSGraphCallRecordsUserFeedbackRating () {
    MSGraphCallRecordsUserFeedbackRatingValue _enumValue;
}
@property (nonatomic, readwrite) MSGraphCallRecordsUserFeedbackRatingValue enumValue;
@end

@implementation MSGraphCallRecordsUserFeedbackRating

+ (MSGraphCallRecordsUserFeedbackRating*) notRated {
    static MSGraphCallRecordsUserFeedbackRating *_notRated;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _notRated = [[MSGraphCallRecordsUserFeedbackRating alloc] init];
        _notRated.enumValue = MSGraphCallRecordsUserFeedbackRatingNotRated;
    });
    return _notRated;
}
+ (MSGraphCallRecordsUserFeedbackRating*) bad {
    static MSGraphCallRecordsUserFeedbackRating *_bad;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _bad = [[MSGraphCallRecordsUserFeedbackRating alloc] init];
        _bad.enumValue = MSGraphCallRecordsUserFeedbackRatingBad;
    });
    return _bad;
}

+ (MSGraphCallRecordsUserFeedbackRating*) UnknownEnumValue {
    static MSGraphCallRecordsUserFeedbackRating *_unknownValue;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _unknownValue = [[MSGraphCallRecordsUserFeedbackRating alloc] init];
        _unknownValue.enumValue = MSGraphCallRecordsUserFeedbackRatingEndOfEnum;
    });
    return _unknownValue;
}


+ (MSGraphCallRecordsUserFeedbackRating*) userFeedbackRatingWithEnumValue:(MSGraphCallRecordsUserFeedbackRatingValue)val {

    switch(val)
    {
        case MSGraphCallRecordsUserFeedbackRatingNotRated:
            return [MSGraphCallRecordsUserFeedbackRating notRated];
        case MSGraphCallRecordsUserFeedbackRatingBad:
            return [MSGraphCallRecordsUserFeedbackRating bad];
        case MSGraphCallRecordsUserFeedbackRatingEndOfEnum:
        default:
            return [MSGraphCallRecordsUserFeedbackRating UnknownEnumValue];
    }

    return nil;
}

- (NSString*) ms_toString {

    switch(self.enumValue)
    {
        case MSGraphCallRecordsUserFeedbackRatingNotRated:
            return @"notRated";
        case MSGraphCallRecordsUserFeedbackRatingBad:
            return @"bad";
        case MSGraphCallRecordsUserFeedbackRatingEndOfEnum:
        default:
            return nil;
    }

    return nil;
}

- (MSGraphCallRecordsUserFeedbackRatingValue) enumValue {
    return _enumValue;
}

@end

@implementation NSString (MSGraphCallRecordsUserFeedbackRating)

- (MSGraphCallRecordsUserFeedbackRating*) toMSGraphCallRecordsUserFeedbackRating{

    if([self isEqualToString:@"notRated"])
    {
          return [MSGraphCallRecordsUserFeedbackRating notRated];
    }
    else if([self isEqualToString:@"bad"])
    {
          return [MSGraphCallRecordsUserFeedbackRating bad];
    }
    else {
        return [MSGraphCallRecordsUserFeedbackRating UnknownEnumValue];
    }
}

@end
