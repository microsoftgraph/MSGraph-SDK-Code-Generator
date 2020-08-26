// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.




#import "MSGraphEntity.h"

@interface MSGraphCall : MSGraphEntity

  @property (nullable, nonatomic, setter=setSubject:, getter=subject) NSString* subject;
  
@end
