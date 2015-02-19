//
//  Property.h
//  JsonParser
//
//  Created by Gustavo on 8/20/14.
//
//

#import <Foundation/Foundation.h>
#import <objc/runtime.h>

@interface Property : NSObject
@property NSString *Type;
@property NSString *SubStringType;
@property NSString *Name;

-(id)initWith : (objc_property_t)property;
-(bool)isString;
-(bool)isDate;
-(bool)isCollection;
-(bool)isComplexType;
-(NSString*)getCollectionEntity;
-(bool)isNumber;

@end