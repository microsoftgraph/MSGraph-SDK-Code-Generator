// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsUserFeedback()
{
    NSString* _text;
    MSGraphCallRecordsUserFeedbackRating* _rating;
    MSGraphCallRecordsFeedbackTokenSet* _tokens;
}
@end

@implementation MSGraphCallRecordsUserFeedback

- (NSString*) text
{
    if([[NSNull null] isEqual:self.dictionary[@"text"]])
    {
        return nil;
    }   
    return self.dictionary[@"text"];
}

- (void) setText: (NSString*) val
{
    self.dictionary[@"text"] = val;
}

- (MSGraphCallRecordsUserFeedbackRating*) rating
{
    if(!_rating){
        _rating = [self.dictionary[@"rating"] toMSGraphCallRecordsUserFeedbackRating];
    }
    return _rating;
}

- (void) setRating: (MSGraphCallRecordsUserFeedbackRating*) val
{
    _rating = val;
    self.dictionary[@"rating"] = val;
}

- (MSGraphCallRecordsFeedbackTokenSet*) tokens
{
    if(!_tokens){
        _tokens = [[MSGraphCallRecordsFeedbackTokenSet alloc] initWithDictionary: self.dictionary[@"tokens"]];
    }
    return _tokens;
}

- (void) setTokens: (MSGraphCallRecordsFeedbackTokenSet*) val
{
    _tokens = val;
    self.dictionary[@"tokens"] = val;
}

@end
