//  
//
//  Copyright (c) 2014 Microsoft Open Technologies, Inc.
//  All rights reserved.
//

import Foundation

class BaseClient : NSObject{
    var credential = Credentials();
    var url = String();

    init(url : String ,credentials : Credentials){
        self.credential = credentials;
        self.url = url;
    }
    
    var data: NSMutableData = NSMutableData();
    
    func parseData(data : NSData) -> NSMutableArray{
        
        return NSMutableArray();
    }
}