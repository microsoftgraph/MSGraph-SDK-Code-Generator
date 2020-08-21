// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphCallRecordsClientUserAgent()
{
    MSGraphCallRecordsClientPlatform* _platform;
    MSGraphCallRecordsProductFamily* _productFamily;
}
@end

@implementation MSGraphCallRecordsClientUserAgent

- (MSGraphCallRecordsClientPlatform*) platform
{
    if(!_platform){
        _platform = [self.dictionary[@"platform"] toMSGraphCallRecordsClientPlatform];
    }
    return _platform;
}

- (void) setPlatform: (MSGraphCallRecordsClientPlatform*) val
{
    _platform = val;
    self.dictionary[@"platform"] = val;
}

- (MSGraphCallRecordsProductFamily*) productFamily
{
    if(!_productFamily){
        _productFamily = [self.dictionary[@"productFamily"] toMSGraphCallRecordsProductFamily];
    }
    return _productFamily;
}

- (void) setProductFamily: (MSGraphCallRecordsProductFamily*) val
{
    _productFamily = val;
    self.dictionary[@"productFamily"] = val;
}

@end
