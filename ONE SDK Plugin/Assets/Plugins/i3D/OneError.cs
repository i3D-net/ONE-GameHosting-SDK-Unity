﻿namespace i3D
{
    /// <summary>
    /// Enumeration of all the errors that can be returned by the SDK.
    /// </summary>
    public enum OneError
    {
        None = 0,
        ArrayAllocationFailed = 100,
        ArrayPositionOutOfBounds = 101,
        ArrayWrongTypeIsExpectingArray = 102,
        ArrayWrongTypeIsExpectingBool = 103,
        ArrayWrongTypeIsExpectingINT = 104,
        ArrayWrongTypeIsExpectingObject = 105,
        ArrayWrongTypeIsExpectingString = 106,
        ClientNotInitialized = 200,
        CodecHeaderLengthTooSmall = 300,
        CodecHeaderLengthTooBig = 301,
        CodecDataLengthTooSmallForHeader = 302,
        CodecDataLengthTooSmallForPayload = 303,
        CodecExpectedDataLengthTooBig = 304,
        CodecInvalidMessagePayloadSizeTooBig = 305,
        CodecInvalidHeader = 306,
        CodecTryingToEncodeUnsupportedOpcode = 307,
        ConnectionUninitialized = 400,
        ConnectionHandshakeTimeout = 401,
        ConnectionHealthTimeout = 402,
        ConnectionHelloInvalid = 403,
        ConnectionHelloMessageHeaderTooBig = 404,
        ConnectionHelloMessageReceiveFailed = 405,
        ConnectionHelloMessageReplyInvalid = 406,
        ConnectionHelloMessageSendFailed = 407,
        ConnectionHelloReceiveFailed = 408,
        ConnectionHelloSendFailed = 409,
        ConnectionHelloTooBig = 410,
        ConnectionHelloVersionMismatch = 411,
        ConnectionMessageReceiveFailed = 412,
        ConnectionReadTooBigForStream = 413,
        ConnectionOutMessageTooBigForStream = 414,
        ConnectionQueueEmpty = 415,
        ConnectionQueueInsufficientSpace = 416,
        ConnectionOutgoingQueueInsufficientSpace = 417,
        ConnectionIncomingQueueInsufficientSpace = 418,
        ConnectionReceiveBeforeSend = 419,
        ConnectionReceiveFail = 420,
        ConnectionSendFail = 421,
        ConnectionTryAgain = 422,
        ConnectionUnknownStatus = 423,
        ConnectionUpdateAfterError = 424,
        ConnectionUpdateReadyFail = 425,
        MessageAllocationFailed = 500,
        MessageCallbackIsNullptr = 501,
        MessageIsNullptr = 502,
        MessageOpcodeNotMatchingExpectingAllocated = 503,
        MessageOpcodeNotMatchingExpectingLiveState = 504,
        MessageOpcodeNotMatchingExpectingMetadata = 505,
        MessageOpcodeNotMatchingExpectingSoftStop = 506,
        MessageOpcodeNotMatchingExpectingHostInformation = 507,
        MessageOpcodeNotMatchingExpectingApplicationInstanceInformation = 508,
        MessageOpcodeNotMatchingExpectingApplicationInstanceStatus = 509,
        MessageOpcodeNotSupported = 510,
        MessageOpcodePayloadNotEmpty = 511,
        ObjectAllocationFailed = 600,
        ObjectKeyNotFound = 601,
        ObjectKeyIsNullptr = 602,
        ObjectWrongTypeIsExpectingArray = 603,
        ObjectWrongTypeIsExpectingBool = 604,
        ObjectWrongTypeIsExpectingINT = 605,
        ObjectWrongTypeIsExpectingObject = 606,
        ObjectWrongTypeIsExpectingString = 607,
        PayloadKeyNotFound = 700,
        PayloadKeyIsNullptr = 701,
        PayloadParseFailed = 702,
        PayloadValIsNullptr = 703,
        PayloadWrongTypeIsExpectingArray = 704,
        PayloadWrongTypeIsExpectingBool = 705,
        PayloadWrongTypeIsExpectingINT = 706,
        PayloadWrongTypeIsExpectingObject = 707,
        PayloadWrongTypeIsExpectingString = 708,
        ServerAllocationFailed = 800,
        ServerAlreadyInitialized = 801,
        ServerAlreadyListening = 802,
        ServerRetryingListen = 803,
        ServerCallbackIsNullptr = 804,
        ServerConnectionIsNullptr = 805,
        ServerConnectionNotReady = 806,
        ServerMessageIsNullptr = 807,
        ServerSocketAllocationFailed = 808,
        ServerSocketNotInitialized = 809,
        ServerSocketIsNullptr = 810,
        SocketAcceptNonBlockingFailed = 900,
        SocketAcceptUninitialized = 901,
        SocketAddressFailed = 902,
        SocketAvailableFailed = 903,
        SocketBindFailed = 904,
        SocketCloseFailed = 905,
        SocketConnectFailed = 906,
        SocketConnectNonBlockingFailed = 907,
        SocketConnectUninitialized = 908,
        SocketCreateFailed = 909,
        SocketListenFailed = 910,
        SocketListenNonBlockingFailed = 911,
        SocketReceiveFailed = 912,
        SocketSelectFailed = 913,
        SocketSelectUninitialized = 914,
        SocketSendFailed = 915,
        SocketSocketOptionsFailed = 916,
        SocketSystemCleanupFail = 917,
        SocketSystemInitFail = 918,
        ValidationArrayIsNullptr = 1000,
        ValidationCallbackIsNullptr = 1001,
        ValidationCapacityIsNullptr = 1002,
        ValidationCodeIsNullptr = 1003,
        ValidationConnectionIsNullptr = 1004,
        ValidationDataIsNullptr = 1005,
        ValidationDestinationIsNullptr = 1006,
        ValidationEmptyIsNullptr = 1007,
        ValidationKeyIsNullptr = 1008,
        ValidationMapIsNullptr = 1009,
        ValidationMessageIsNullptr = 1010,
        ValidationModeIsNullptr = 1011,
        ValidationNameIsNullptr = 1012,
        ValidationObjectIsNullptr = 1013,
        ValidationResultIsNullptr = 1014,
        ValidationServerIsNullptr = 1015,
        ValidationSocketIsNullptr = 1016,
        ValidationSourceIsNullptr = 1017,
        ValidationStatusIsNullptr = 1018,
        ValidationSizeIsNullptr = 1019,
        ValidationValIsNullptr = 1020,
        ValidationValSizeIsTooSmall = 1021,
        ValidationVersionIsNullptr = 1022
    }
}