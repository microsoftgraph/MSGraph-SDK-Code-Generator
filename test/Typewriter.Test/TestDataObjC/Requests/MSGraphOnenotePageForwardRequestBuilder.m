// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "MSGraphODataEntities.h"

@interface MSGraphOnenotePageForwardRequestBuilder()


@property (nonatomic, getter=toRecipients) NSArray * toRecipients;


@property (nonatomic, getter=details) NSString * details;


@property (nonatomic, getter=comment) NSString * comment;

@end

@implementation MSGraphOnenotePageForwardRequestBuilder


- (instancetype)initWithToRecipients:(NSArray *)toRecipients details:(NSString *)details comment:(NSString *)comment URL:(NSURL *)url client:(ODataBaseClient*)client
{
    self = [super initWithURL:url client:client];
    if (self){
        _toRecipients = toRecipients;
        _details = details;
        _comment = comment;
    }
    return self;
}

- (MSGraphOnenotePageForwardRequest *)request
{
    return [self requestWithOptions:nil];
}

- (MSGraphOnenotePageForwardRequest *)requestWithOptions:(NSArray *)options
{

    return [[MSGraphOnenotePageForwardRequest alloc] initWithToRecipients:self.toRecipients
                                                                  details:self.details
                                                                  comment:self.comment
                                                                      URL:self.requestURL
                                                                  options:options
                                                                   client:self.client];

}

@end
