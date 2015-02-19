//
//  JsonParser.h
//  JsonParser
//
//  Created by Gustavo on 8/20/14.
//
//

#import <Foundation/Foundation.h>

@interface JsonParser : NSObject

-(id)parseWithData : (NSData*)data forType : (Class) type selector:(NSArray* )keys;
-(NSString*)toJsonString : (id)object :(Class)type;
-(NSString*)toJsonString:(id)object Property:(NSString*)name;
@end