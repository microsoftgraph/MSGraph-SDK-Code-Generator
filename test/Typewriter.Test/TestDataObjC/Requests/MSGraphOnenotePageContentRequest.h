// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.


@class MSURLSessionDownloadTask, MSURLSessionUploadTask, MSGraphOnenotePage;

typedef void(^MSGraphOnenotePageUploadCompletionHandler)(MSGraphOnenotePage *response, NSError* error);

#import "MSRequest.h"

@interface MSGraphOnenotePageContentRequest : MSRequest

- (MSURLSessionDownloadTask *) downloadWithCompletion:(MSDownloadCompletionHandler)completionHandler;

- (MSURLSessionUploadTask *) uploadFromData:(NSData *)data
                                 completion:(MSGraphOnenotePageUploadCompletionHandler)completionHandler;

- (MSURLSessionUploadTask *) uploadFromFile:(NSURL *)fileUrl
                                 completion:(MSGraphOnenotePageUploadCompletionHandler)completionHandler;

@end
