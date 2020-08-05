// Type definitions for non-npm package microsoft-graph <VERSION_STRING>
// Project: https://github.com/microsoftgraph/msgraph-typescript-typings
// Definitions by: Microsoft Graph Team <https://github.com/microsoftgraph>
//                 Michael Mainer <https://github.com/MIchaelMainer>
//                 Peter Ombwa <https://github.com/peombwa>
//                 Mustafa Zengin <https://github.com/zengin>
//                 DeVere Dyett <https://github.com/ddyett>
//                 Nikitha Udaykumar Chettiar <https://github.com/nikithauc>
// Definitions: https://github.com/DefinitelyTyped/DefinitelyTyped
// TypeScript Version: 2.1

export as namespace microsoftgraph;

export type NullableOption<T> = T | null;

export type Enum1 = "value0" | "value1";
export interface Entity {
    id?: string;
}
export interface TestType extends Entity {
    propertyAlpha?: NullableOption<DerivedComplexTypeRequest>;
}
// tslint:disable-next-line: no-empty-interface
export interface EntityType2 extends Entity {}
// tslint:disable-next-line: no-empty-interface
export interface EntityType3 extends Entity {}
export interface TestEntity extends Entity {
    testNav?: NullableOption<TestType>;
    testInvalidNav?: NullableOption<EntityType2>;
    testExplicitNav?: NullableOption<EntityType3>;
}
export interface Endpoint extends Entity {
    property1?: NullableOption<number>;
}
export interface SingletonEntity1 extends Entity {
    testSingleNav?: NullableOption<TestType>;
}
export interface SingletonEntity2 extends Entity {
    testSingleNav2?: NullableOption<EntityType3>;
}
export interface TimeOffRequest extends Entity {
    name?: NullableOption<string>;
}
export interface TimeOff extends Entity {
    name?: NullableOption<string>;
}
export interface Schedule extends Entity {
    enabled?: NullableOption<boolean>;
    timesOff?: NullableOption<TimeOff[]>;
    timeOffRequests?: NullableOption<TimeOffRequest[]>;
}
export interface Call extends Entity {
    subject?: NullableOption<string>;
}
export interface CloudCommunications extends Entity {
    calls?: NullableOption<Call[]>;
    callRecords?: NullableOption<CallRecords.CallRecord[]>;
}
export interface OnenotePage extends Entity {
    // The OneNotePage content.
///
/// Test token string
    content?: NullableOption<any>;
}
// tslint:disable-next-line: no-empty-interface
export interface PlannerGroup extends Entity {}
// tslint:disable-next-line: no-empty-interface
export interface EmptyBaseComplexTypeRequest {}
export interface DerivedComplexTypeRequest extends EmptyBaseComplexTypeRequest {
    property1?: NullableOption<string>;
    property2?: NullableOption<string>;
    enumProperty?: NullableOption<Enum1>;
}
// tslint:disable-next-line: no-empty-interface
export interface ResponseObject {}
export interface Recipient {
    name?: NullableOption<string>;
    email?: NullableOption<string>;
}
// tslint:disable-next-line: no-empty-interface
export interface EmptyComplexType {}
// tslint:disable-next-line: interface-name
export interface Identity {
    displayName?: NullableOption<string>;
    id?: NullableOption<string>;
}
// tslint:disable-next-line: interface-name
export interface IdentitySet {
    application?: NullableOption<Identity>;
    device?: NullableOption<Identity>;
    user?: NullableOption<Identity>;
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
        testSingleNav?: NullableOption<microsoftgraph.TestType>;
    }
    export interface CallRecord extends microsoftgraph.Entity {
        version?: number;
        type?: CallType;
        modalities?: Modality[];
        lastModifiedDateTime?: string;
        startDateTime?: string;
        endDateTime?: string;
        organizer?: NullableOption<microsoftgraph.IdentitySet>;
        participants?: NullableOption<microsoftgraph.IdentitySet[]>;
        joinWebUrl?: NullableOption<string>;
        sessions?: NullableOption<Session[]>;
        recipients?: NullableOption<microsoftgraph.EntityType2[]>;
    }
    export interface Session extends microsoftgraph.Entity {
        modalities?: Modality[];
        startDateTime?: string;
        endDateTime?: string;
        caller?: NullableOption<Endpoint>;
        callee?: NullableOption<Endpoint>;
        failureInfo?: NullableOption<FailureInfo>;
        segments?: NullableOption<Segment[]>;
    }
    export interface Segment extends microsoftgraph.Entity {
        startDateTime?: string;
        endDateTime?: string;
        caller?: NullableOption<Endpoint>;
        callee?: NullableOption<Endpoint>;
        failureInfo?: NullableOption<FailureInfo>;
        media?: NullableOption<Media[]>;
        refTypes?: NullableOption<microsoftgraph.EntityType3[]>;
        refType?: NullableOption<microsoftgraph.Call>;
        sessionRef?: NullableOption<Session>;
        photo?: NullableOption<Photo>;
    }
// tslint:disable-next-line: no-empty-interface
    export interface Option extends microsoftgraph.Entity {}
    export interface Photo extends microsoftgraph.Entity {
        failureInfo?: NullableOption<FailureInfo>;
        option?: NullableOption<Option>;
    }
    export interface Endpoint {
        userAgent?: NullableOption<UserAgent>;
    }
    export interface UserAgent {
        headerValue?: NullableOption<string>;
        applicationVersion?: NullableOption<string>;
    }
    export interface FailureInfo {
        stage?: FailureStage;
        reason?: NullableOption<string>;
    }
    export interface Media {
        label?: NullableOption<string>;
        callerNetwork?: NullableOption<NetworkInfo>;
        callerDevice?: NullableOption<DeviceInfo>;
        streams?: NullableOption<MediaStream[]>;
    }
    export interface NetworkInfo {
        connectionType?: NetworkConnectionType;
        wifiBand?: WifiBand;
        basicServiceSetIdentifier?: NullableOption<string>;
        wifiRadioType?: WifiRadioType;
        wifiSignalStrength?: NullableOption<number>;
        bandwidthLowEventRatio?: NullableOption<number>;
    }
    export interface DeviceInfo {
        captureDeviceName?: NullableOption<string>;
        sentSignalLevel?: NullableOption<number>;
        speakerGlitchRate?: NullableOption<number>;
    }
    export interface MediaStream {
        streamId?: NullableOption<string>;
        startDateTime?: NullableOption<string>;
        streamDirection?: MediaStreamDirection;
        packetUtilization?: NullableOption<number>;
        wasMediaBypassed?: NullableOption<boolean>;
        lowVideoProcessingCapabilityRatio?: NullableOption<number>;
        averageAudioNetworkJitter?: NullableOption<string>;
    }
    export interface ParticipantEndpoint extends Endpoint {
        identity?: NullableOption<microsoftgraph.IdentitySet>;
        feedback?: NullableOption<UserFeedback>;
    }
    export interface UserFeedback {
        text?: NullableOption<string>;
        rating?: UserFeedbackRating;
        tokens?: NullableOption<FeedbackTokenSet>;
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

