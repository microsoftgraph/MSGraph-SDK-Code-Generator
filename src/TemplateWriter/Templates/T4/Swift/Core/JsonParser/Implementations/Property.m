//
//  Property.m
//  JsonParser
//
//  Created by Gustavo on 8/20/14.
//
//

#import "Property.h"

@implementation Property

-(id)initWith : (objc_property_t)property{
    
    NSString *typeString = [NSString stringWithCString:property_getAttributes(property) encoding:NSUTF8StringEncoding];
    NSArray *attributes = [typeString componentsSeparatedByString:@","];
    
    self.Type = [attributes objectAtIndex:0];
    self.Name = [NSString  stringWithCString:property_getName(property) encoding:NSUTF8StringEncoding];
    
    if ([self isComplexType]) {
        self.SubStringType = [self.Type substringWithRange:NSMakeRange(3, [self.Type length] -4)];
    }
    return self;
}

-(bool)isString{
    return [self.SubStringType isEqualToString:@"NSString"];
}

-(bool)isNumber{
    return [self.SubStringType isEqualToString:@"NSInteger"];
}

-(bool)isDate{
    return [self.SubStringType isEqualToString:@"NSDate"];
}

-(bool)isCollection{
    return [self.SubStringType hasPrefix:@"NSMutableArray"] ||
            [self.SubStringType hasPrefix:@"NSArray"] ||
            [self.SubStringType hasPrefix:@"NSDictionary"];
}

-(NSString*)getCollectionEntity{

    NSArray *attributes = [self.SubStringType componentsSeparatedByString:@"<"];
    NSString* att = [attributes objectAtIndex:attributes.count -1];
    return [att substringWithRange:NSMakeRange(0, [att length] -1)];
}

-(bool)isComplexType{
    return [self.Type hasPrefix:@"T@"];
}

@end