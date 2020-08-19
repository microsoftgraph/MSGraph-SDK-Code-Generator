// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



#import "NSDate+MSSerialization.h"

#import "MSGraphClientModels.h"

@interface MSObject()

@property (strong, nonatomic) NSMutableDictionary *dictionary;

@end

@interface MSGraphIdentitySet()
{
    MSGraphIdentity* _application;
    MSGraphIdentity* _device;
    MSGraphIdentity* _user;
}
@end

@implementation MSGraphIdentitySet

- (MSGraphIdentity*) application
{
    if(!_application){
        _application = [[MSGraphIdentity alloc] initWithDictionary: self.dictionary[@"application"]];
    }
    return _application;
}

- (void) setApplication: (MSGraphIdentity*) val
{
    _application = val;
    self.dictionary[@"application"] = val;
}

- (MSGraphIdentity*) device
{
    if(!_device){
        _device = [[MSGraphIdentity alloc] initWithDictionary: self.dictionary[@"device"]];
    }
    return _device;
}

- (void) setDevice: (MSGraphIdentity*) val
{
    _device = val;
    self.dictionary[@"device"] = val;
}

- (MSGraphIdentity*) user
{
    if(!_user){
        _user = [[MSGraphIdentity alloc] initWithDictionary: self.dictionary[@"user"]];
    }
    return _user;
}

- (void) setUser: (MSGraphIdentity*) val
{
    _user = val;
    self.dictionary[@"user"] = val;
}

@end
