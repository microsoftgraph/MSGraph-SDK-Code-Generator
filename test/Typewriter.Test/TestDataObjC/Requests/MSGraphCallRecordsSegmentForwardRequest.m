// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.




#import "MSGraphODataEntities.h"
#import "MSGraphModels.h"
#import "MSURLSessionDataTask.h"





@interface MSRequest()

@property NSMutableArray *options;

- (NSMutableURLRequest *)requestWithMethod:(NSString *)method
                                      body:(NSData *)body
                                   headers:(NSDictionary *)headers;

@end

@interface MSGraphSegmentForwardRequest()


@property (nonatomic, getter=toRecipients) NSArray * toRecipients;


@property (nonatomic, getter=singleRecipient) MSGraphRecipient * singleRecipient;


@property (nonatomic, getter=multipleSessions) NSArray * multipleSessions;


@property (nonatomic, getter=singleSession) MSGraphCallRecordsSession * singleSession;


@property (nonatomic, getter=comment) NSString * comment;

@end

@implementation MSGraphSegmentForwardRequest


- (instancetype)initWithToRecipients:(NSArray *)toRecipients singleRecipient:(MSGraphRecipient *)singleRecipient multipleSessions:(NSArray *)multipleSessions singleSession:(MSGraphCallRecordsSession *)singleSession comment:(NSString *)comment URL:(NSURL *)url options:(NSArray *)options client:(ODataBaseClient*)client
{
    NSParameterAssert(toRecipients);
    NSParameterAssert(singleRecipient);
    NSParameterAssert(multipleSessions);
    NSParameterAssert(singleSession);
    self = [super initWithURL:url options:options client:client];
    if (self){
        _toRecipients = toRecipients;
        _singleRecipient = singleRecipient;
        _multipleSessions = multipleSessions;
        _singleSession = singleSession;
        _comment = comment;
    }
    return self;
}

- (NSMutableURLRequest *)mutableRequest
{
    NSDictionary *params = [[NSDictionary alloc] initWithObjectsAndKeys:[MSObject getNSJsonSerializationCompatibleValue:_toRecipients],@"ToRecipients",[MSObject getNSJsonSerializationCompatibleValue:_singleRecipient],@"SingleRecipient",[MSObject getNSJsonSerializationCompatibleValue:_multipleSessions],@"MultipleSessions",[MSObject getNSJsonSerializationCompatibleValue:_singleSession],@"SingleSession",[MSObject getNSJsonSerializationCompatibleValue:_comment],@"Comment",nil];


    NSData *body = [NSJSONSerialization dataWithJSONObject:params options:0 error:nil];
    return [self requestWithMethod:@"POST" body:body headers:nil];
}


- (MSURLSessionDataTask *)executeWithCompletion:(void (^)(NSDictionary *response, NSError *error))completionHandler
{

    MSURLSessionDataTask *task = [self taskWithRequest:self.mutableRequest
                                odObjectWithDictionary:^(id responseObject){
                                                           return [[NSDictionary alloc] initWithDictionary:responseObject];
                                                       }
                                            completion:completionHandler];
    [task execute];
    return task;
}

@end
