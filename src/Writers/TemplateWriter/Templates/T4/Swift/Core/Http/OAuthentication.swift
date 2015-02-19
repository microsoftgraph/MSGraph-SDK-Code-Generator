//  
//
//  Copyright (c) 2014 Microsoft Open Technologies, Inc.
//  All rights reserved.
//

import Foundation

class OAuthentication : Credentials{
    
    var token = String();
    
    override func prepareRequest(request: NSMutableURLRequest) {
        request.addValue("Bearer " + self.token, forHTTPHeaderField: "Authorization");
    }
    
    func setToken(token :String){
        self.token = token;
    }
}