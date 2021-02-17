// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@interface MSGraphEntityType3ForwardRequestBuilder()


@property (nonatomic, getter=toRecipients) NSArray * toRecipients;


@property (nonatomic, getter=singleRecipient) MSGraphRecipient * singleRecipient;


@property (nonatomic, getter=multipleSessions) NSArray * multipleSessions;


@property (nonatomic, getter=singleSession) MSGraphCallRecordsSession * singleSession;


@property (nonatomic, getter=comment) NSString * comment;

@end

@implementation MSGraphEntityType3ForwardRequestBuilder


- (instancetype)initWithToRecipients:(NSArray *)toRecipients singleRecipient:(MSGraphRecipient *)singleRecipient multipleSessions:(NSArray *)multipleSessions singleSession:(MSGraphCallRecordsSession *)singleSession comment:(NSString *)comment URL:(NSURL *)url client:(ODataBaseClient*)client
{
    self = [super initWithURL:url client:client];
    if (self){
        _toRecipients = toRecipients;
        _singleRecipient = singleRecipient;
        _multipleSessions = multipleSessions;
        _singleSession = singleSession;
        _comment = comment;
    }
    return self;
}

- (MSGraphEntityType3ForwardRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphEntityType3ForwardRequest *)requestWithOptions:(NSArray *)options
{

    return [[MSGraphEntityType3ForwardRequest alloc] initWithToRecipients:self.toRecipients
                                                          singleRecipient:self.singleRecipient
                                                         multipleSessions:self.multipleSessions
                                                            singleSession:self.singleSession
                                                                  comment:self.comment
                                                                      URL:self.requestURL
                                                                  options:options
                                                                   client:self.client];

}

@end
