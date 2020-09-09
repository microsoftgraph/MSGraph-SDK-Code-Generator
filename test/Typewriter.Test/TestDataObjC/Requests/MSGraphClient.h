// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.



@class MSGraphTestTypesCollectionRequestBuilder;
@class MSGraphEntityType3RequestBuilder;
@class MSGraphSingletonProperty1CollectionRequestBuilder;
@class MSGraphSingletonEntity1RequestBuilder;
@class MSGraphSingletonProperty2CollectionRequestBuilder;
@class MSGraphSingletonEntity2RequestBuilder;
@class MSGraphSingletonProperty3CollectionRequestBuilder;
@class MSGraphSingletonEntity1RequestBuilder;

#import "ODataBaseClient.h"
#import "MSGraphModels.h"
#import "MSHttpProvider.h"
#import "MSAuthenticationProvider.h"
#import "MSLoggerProtocol.h"

/**
* The header for type MSGraphClient.
*/

@interface MSGraphClient : ODataBaseClient

-(MSGraphTestTypesCollectionRequestBuilder *)testTypes;

-(MSGraphEntityType3RequestBuilder *)testTypes:(NSString*)testTypesId;

-(MSGraphSingletonEntity1RequestBuilder *) singletonProperty1;
-(MSGraphSingletonEntity2RequestBuilder *) singletonProperty2;
-(MSGraphSingletonEntity1RequestBuilder *) singletonProperty3;

@end
