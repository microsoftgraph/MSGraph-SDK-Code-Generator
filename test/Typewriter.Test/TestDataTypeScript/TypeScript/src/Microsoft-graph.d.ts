// Type definitions for non-npm package microsoft-graph <VERSION_STRING>
// Project: https://github.com/microsoftgraph/msgraph-typescript-typings
// Definitions by: Microsoft Graph Team <https://github.com/microsoftgraph>
//                 Michael Mainer <https://github.com/MIchaelMainer>
//                 Peter Ombwa <https://github.com/peombwa>
//                 Mustafa Zengin <https://github.com/zengin>
//                 DeVere Dyett <https://github.com/ddyett>
// Definitions: https://github.com/DefinitelyTyped/DefinitelyTyped
// TypeScript Version: 2.1

export as namespace microsoftgraph;

export type Enum1 = "value0" | "value1";
export interface Entity {
    id?: string;
}
export interface TestType extends Entity {
    propertyAlpha?: DerivedComplexTypeRequest;
}
// tslint:disable-next-line: no-empty-interface
export interface EntityType2 extends Entity {}
// tslint:disable-next-line: no-empty-interface
export interface EntityType3 extends Entity {}
export interface TestEntity extends Entity {
    testNav?: TestType;
    testInvalidNav?: EntityType2;
    testExplicitNav?: EntityType3;
}
export interface Endpoint extends Entity {
    property1?: number;
}
export interface SingletonEntity1 extends Entity {
    testSingleNav?: TestType;
}
export interface SingletonEntity2 extends Entity {
    testSingleNav2?: EntityType3;
}
export interface TimeOffRequest extends Entity {
    name?: string;
}
export interface TimeOff extends Entity {
    name?: string;
}
export interface Schedule extends Entity {
    enabled?: boolean;
    timesOff?: TimeOff[];
    timeOffRequests?: TimeOffRequest[];
}
export interface Call extends Entity {
    subject?: string;
}
export interface CloudCommunications extends Entity {
    calls?: Call[];
    callRecords?: CallRecords.CallRecord[];
}
export interface OnenotePage extends Entity {
    // The OneNotePage content.
///
/// Test token string
    content?: any;
}
// tslint:disable-next-line: no-empty-interface
export interface PlannerGroup extends Entity {}
// tslint:disable-next-line: no-empty-interface
export interface EmptyBaseComplexTypeRequest {}
export interface DerivedComplexTypeRequest extends EmptyBaseComplexTypeRequest {
    property1?: string;
    property2?: string;
    enumProperty?: Enum1;
}
// tslint:disable-next-line: no-empty-interface
export interface ResponseObject {}
export interface Recipient {
    name?: string;
    email?: string;
}
// tslint:disable-next-line: no-empty-interface
export interface EmptyComplexType {}
// tslint:disable-next-line: interface-name
export interface Identity {
    displayName?: string;
    id?: string;
}
// tslint:disable-next-line: interface-name
export interface IdentitySet {
    application?: Identity;
    device?: Identity;
    user?: Identity;
}


export namespace CallRecords {
    export type CallType = "unknown" | "groupCall";
    export type ClientPlatform = "unknown" | "windows";
    export type FailureStage = "unknown" | "callSetup";
    export type MediaStreamDirection = "callerToCallee" | "calleeToCaller";
    export type NetworkConnectionType = "unknown" | "wired";
    export type ProductFamily = "unknown" | "teams";
    export type ServiceRole = "unknown" | "customBot";
    export type UserFeedbackRating = "notRated" | "bad";
    export type WifiBand = "unknown" | "frequency24GHz";
    export type WifiRadioType = "unknown" | "wifi80211a";
    export type Modality = "audio" | "video";
    export interface SingletonEntity1 extends microsoftgraph.Entity {
        testSingleNav?: microsoftgraph.TestType;
    }
    export interface CallRecord extends microsoftgraph.Entity {
        version?: number;
        type?: CallType;
        modalities?: Modality[];
        lastModifiedDateTime?: string;
        startDateTime?: string;
        endDateTime?: string;
        organizer?: microsoftgraph.IdentitySet;
        participants?: microsoftgraph.IdentitySet[];
        joinWebUrl?: string;
        sessions?: Session[];
        recipients?: microsoftgraph.EntityType2[];
    }
    export interface Session extends microsoftgraph.Entity {
        modalities?: Modality[];
        startDateTime?: string;
        endDateTime?: string;
        caller?: Endpoint;
        callee?: Endpoint;
        failureInfo?: FailureInfo;
        segments?: Segment[];
    }
    export interface Segment extends microsoftgraph.Entity {
        startDateTime?: string;
        endDateTime?: string;
        caller?: Endpoint;
        callee?: Endpoint;
        failureInfo?: FailureInfo;
        media?: Media[];
        refTypes?: microsoftgraph.EntityType3[];
        refType?: microsoftgraph.Call;
        sessionRef?: Session;
        photo?: Photo;
    }
// tslint:disable-next-line: no-empty-interface
    export interface Option extends microsoftgraph.Entity {}
    export interface Photo extends microsoftgraph.Entity {
        failureInfo?: FailureInfo;
        option?: Option;
    }
    export interface Endpoint {
        userAgent?: UserAgent;
    }
    export interface UserAgent {
        headerValue?: string;
        applicationVersion?: string;
    }
    export interface FailureInfo {
        stage?: FailureStage;
        reason?: string;
    }
    export interface Media {
        label?: string;
        callerNetwork?: NetworkInfo;
        callerDevice?: DeviceInfo;
        streams?: MediaStream[];
    }
    export interface NetworkInfo {
        connectionType?: NetworkConnectionType;
        wifiBand?: WifiBand;
        basicServiceSetIdentifier?: string;
        wifiRadioType?: WifiRadioType;
        wifiSignalStrength?: number;
        bandwidthLowEventRatio?: number;
    }
    export interface DeviceInfo {
        captureDeviceName?: string;
        sentSignalLevel?: number;
        speakerGlitchRate?: number;
    }
    export interface MediaStream {
        streamId?: string;
        startDateTime?: string;
        streamDirection?: MediaStreamDirection;
        packetUtilization?: number;
        wasMediaBypassed?: boolean;
        lowVideoProcessingCapabilityRatio?: number;
        averageAudioNetworkJitter?: string;
    }
    export interface ParticipantEndpoint extends Endpoint {
        identity?: microsoftgraph.IdentitySet;
        feedback?: UserFeedback;
    }
    export interface UserFeedback {
        text?: string;
        rating?: UserFeedbackRating;
        tokens?: FeedbackTokenSet;
    }
// tslint:disable-next-line: no-empty-interface
    export interface FeedbackTokenSet {}
// tslint:disable-next-line: no-empty-interface
    export interface ServiceEndpoint extends Endpoint {}
    export interface ClientUserAgent extends UserAgent {
        platform?: ClientPlatform;
        productFamily?: ProductFamily;
    }
    export interface ServiceUserAgent extends UserAgent {
        role?: ServiceRole;
    }
}

