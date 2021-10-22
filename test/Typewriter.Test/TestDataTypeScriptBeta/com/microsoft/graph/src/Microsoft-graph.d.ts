// Project: https://github.com/microsoftgraph/msgraph-typescript-typings
// Definitions by: Microsoft Graph Team <https://github.com/microsoftgraph>
//                 Michael Mainer <https://github.com/MIchaelMainer>
//                 Peter Ombwa <https://github.com/peombwa>
//                 Mustafa Zengin <https://github.com/zengin>
//                 DeVere Dyett <https://github.com/ddyett>
//                 Nikitha Udaykumar Chettiar <https://github.com/nikithauc>
// Definitions: https://github.com/DefinitelyTyped/DefinitelyTyped
// TypeScript Version: 2.1

export as namespace microsoftgraphbeta;

export type NullableOption<T> = T | null;

export type Enum1 = "value0" | "value1";
export interface Entity {
    id?: string;
}
export interface TestType extends Entity {
    propertyAlpha?: NullableOption<DerivedComplexTypeRequest>;
}
export interface Print {
    settings?: NullableOption<string>;
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
    type CallType = "unknown" | "groupCall";
    type ClientPlatform = "unknown" | "windows";
    type FailureStage = "unknown" | "callSetup";
    type MediaStreamDirection = "callerToCallee" | "calleeToCaller";
    type NetworkConnectionType = "unknown" | "wired";
    type ProductFamily = "unknown" | "teams";
    type ServiceRole = "unknown" | "customBot";
    type UserFeedbackRating = "notRated" | "bad";
    type WifiBand = "unknown" | "frequency24GHz";
    type WifiRadioType = "unknown" | "wifi80211a";
    type Modality = "audio" | "video";
    interface SingletonEntity1 extends microsoftgraphbeta.Entity {
        testSingleNav?: NullableOption<microsoftgraphbeta.TestType>;
    }
    interface CallRecord extends microsoftgraphbeta.Entity {
        version?: number;
        type?: CallType;
        modalities?: Modality[];
        lastModifiedDateTime?: string;
        startDateTime?: string;
        endDateTime?: string;
        organizer?: NullableOption<microsoftgraphbeta.IdentitySet>;
        participants?: NullableOption<microsoftgraphbeta.IdentitySet[]>;
        joinWebUrl?: NullableOption<string>;
        sessions?: NullableOption<Session[]>;
        recipients?: NullableOption<microsoftgraphbeta.EntityType2[]>;
    }
    interface Session extends microsoftgraphbeta.Entity {
        modalities?: Modality[];
        startDateTime?: string;
        endDateTime?: string;
        caller?: NullableOption<Endpoint>;
        callee?: NullableOption<Endpoint>;
        failureInfo?: NullableOption<FailureInfo>;
        segments?: NullableOption<Segment[]>;
    }
    interface Segment extends microsoftgraphbeta.Entity {
        startDateTime?: string;
        endDateTime?: string;
        caller?: NullableOption<Endpoint>;
        callee?: NullableOption<Endpoint>;
        failureInfo?: NullableOption<FailureInfo>;
        media?: NullableOption<Media[]>;
        refTypes?: NullableOption<microsoftgraphbeta.EntityType3[]>;
        refType?: NullableOption<microsoftgraphbeta.Call>;
        sessionRef?: NullableOption<Session>;
        photo?: NullableOption<Photo>;
    }
// tslint:disable-next-line: no-empty-interface
    interface Option extends microsoftgraphbeta.Entity {}
    interface Photo extends microsoftgraphbeta.Entity {
        failureInfo?: NullableOption<FailureInfo>;
        option?: NullableOption<Option>;
    }
    interface Endpoint {
        userAgent?: NullableOption<UserAgent>;
    }
    interface UserAgent {
        headerValue?: NullableOption<string>;
        applicationVersion?: NullableOption<string>;
    }
    interface FailureInfo {
        stage?: FailureStage;
        reason?: NullableOption<string>;
    }
    interface Media {
        label?: NullableOption<string>;
        callerNetwork?: NullableOption<NetworkInfo>;
        callerDevice?: NullableOption<DeviceInfo>;
        streams?: NullableOption<MediaStream[]>;
    }
    interface NetworkInfo {
        connectionType?: NetworkConnectionType;
        wifiBand?: WifiBand;
        basicServiceSetIdentifier?: NullableOption<string>;
        wifiRadioType?: WifiRadioType;
        wifiSignalStrength?: NullableOption<number>;
        bandwidthLowEventRatio?: NullableOption<number>;
    }
    interface DeviceInfo {
        captureDeviceName?: NullableOption<string>;
        sentSignalLevel?: NullableOption<number>;
        speakerGlitchRate?: NullableOption<number>;
    }
    interface MediaStream {
        streamId?: NullableOption<string>;
        startDateTime?: NullableOption<string>;
        streamDirection?: MediaStreamDirection;
        packetUtilization?: NullableOption<number>;
        wasMediaBypassed?: NullableOption<boolean>;
        lowVideoProcessingCapabilityRatio?: NullableOption<number>;
        averageAudioNetworkJitter?: NullableOption<string>;
    }
    interface ParticipantEndpoint extends Endpoint {
        identity?: NullableOption<microsoftgraphbeta.IdentitySet>;
        feedback?: NullableOption<UserFeedback>;
    }
    interface UserFeedback {
        text?: NullableOption<string>;
        rating?: UserFeedbackRating;
        tokens?: NullableOption<FeedbackTokenSet>;
    }
// tslint:disable-next-line: no-empty-interface
    interface FeedbackTokenSet {}
// tslint:disable-next-line: no-empty-interface
    interface ServiceEndpoint extends Endpoint {}
    interface ClientUserAgent extends UserAgent {
        platform?: ClientPlatform;
        productFamily?: ProductFamily;
    }
    interface ServiceUserAgent extends UserAgent {
        role?: ServiceRole;
    }
    interface DisplayTemplate {
        id?: string;
        layout?: any;
        priority?: number;
    }
}
