// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.


#import "MSGraphModels.h"
#import "MSGraphODataEntities.h"
#import "MSURLSessionDownloadTask.h"
#import "MSURLSessionUploadTask.h"

@interface MSRequest()

- (NSMutableURLRequest *)requestWithMethod:(NSString *)method
                                      body:(NSData *)body
                                   headers:(NSDictionary *)headers;

@end

@implementation MSGraphOnenotePageContentRequest

- (NSMutableURLRequest *) download
{
    return [self requestWithMethod:@"GET"
                              body:nil
                           headers:nil];
}

- (MSURLSessionDownloadTask *) downloadWithCompletion:(MSDownloadCompletionHandler)completionHandler
{
    MSURLSessionDownloadTask *task = [[MSURLSessionDownloadTask alloc] initWithRequest:[self download]
                                                                                client:self.client
                                                                     completionHandler:completionHandler];
    [task execute];
    return task;
}

- (NSMutableURLRequest *) upload
{
    //when creating an upload task the body will be reset
    return [self requestWithMethod:@"PUT"
                              body:nil
                           headers:nil];
}

- (MSURLSessionUploadTask *) uploadFromData:(NSData *)data
                                 completion:(MSGraphOnenotePageUploadCompletionHandler)completionHandler
{
    MSURLSessionUploadTask *task = [self uploadTaskWithRequest:[self upload]
                                                      fromData:data
                                        odobjectWithDictionary:^(NSDictionary *response){
                                                return [[MSGraphOnenotePage alloc] initWithDictionary:response];
                                        }
                                             completionHandler:completionHandler];
    [task execute];
    return task;

}

- (MSURLSessionUploadTask *) uploadFromFile:(NSURL *)fileURL
                                 completion:(MSGraphOnenotePageUploadCompletionHandler)completionHandler
{
    MSURLSessionUploadTask *task = [self uploadTaskWithRequest:[self upload]
                                                      fromFile:fileURL
                                        odobjectWithDictionary:^(NSDictionary *response){
                                                return [[MSGraphOnenotePage alloc] initWithDictionary:response];
                                        }
                                             completionHandler:completionHandler];
    [task execute];
    return task;
}

@end
