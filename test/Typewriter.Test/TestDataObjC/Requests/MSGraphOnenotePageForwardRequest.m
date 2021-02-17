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

@interface MSGraphOnenotePageForwardRequest()


@property (nonatomic, getter=toRecipients) NSArray * toRecipients;


@property (nonatomic, getter=details) NSString * details;


@property (nonatomic, getter=comment) NSString * comment;

@end

@implementation MSGraphOnenotePageForwardRequest


- (instancetype)initWithToRecipients:(NSArray *)toRecipients details:(NSString *)details comment:(NSString *)comment URL:(NSURL *)url options:(NSArray *)options client:(ODataBaseClient*)client
{
    NSParameterAssert(toRecipients);
    self = [super initWithURL:url options:options client:client];
    if (self){
        _toRecipients = toRecipients;
        _details = details;
        _comment = comment;
    }
    return self;
}

- (NSMutableURLRequest *)mutableRequest
{
    NSDictionary *params = [[NSDictionary alloc] initWithObjectsAndKeys:[MSObject getNSJsonSerializationCompatibleValue:_toRecipients],@"ToRecipients",[MSObject getNSJsonSerializationCompatibleValue:_details],@"Details",[MSObject getNSJsonSerializationCompatibleValue:_comment],@"Comment",nil];


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
