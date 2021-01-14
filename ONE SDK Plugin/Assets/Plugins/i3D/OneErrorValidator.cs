using System;
using i3D.Exceptions;

namespace i3D
{
    /// <summary>
    /// Helper class for validation of the error codes returned by the ONE SDK.
    /// For internal use ONLY. 
    /// </summary>
    internal static class OneErrorValidator
    {
        /// <summary>
        /// Validates the error code and throws an exception if needed.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <exception cref="OneInvalidErrorCodeException"/>
        /// <exception cref="OneArrayException"/>
        /// <exception cref="OneClientException"/>
        /// <exception cref="OneCodecException"/>
        /// <exception cref="OneConnectionException"/>
        /// <exception cref="OneMessageException"/>
        /// <exception cref="OneObjectException"/>
        /// <exception cref="OnePayloadException"/>
        /// <exception cref="OneServerException"/>
        /// <exception cref="OneSocketException"/>
        /// <exception cref="OneValidationException"/>
        public static void Validate(int errorCode)
        {
            if (!Enum.IsDefined(typeof(OneError), errorCode))
                throw new OneInvalidErrorCodeException(errorCode);

            var error = (OneError) errorCode;

            switch (error)
            {
                case OneError.None:
                    return;
                case OneError.ArrayAllocationFailed:
                case OneError.ArrayPositionOutOfBounds:
                case OneError.ArrayWrongTypeIsExpectingArray:
                case OneError.ArrayWrongTypeIsExpectingBool:
                case OneError.ArrayWrongTypeIsExpectingINT:
                case OneError.ArrayWrongTypeIsExpectingObject:
                case OneError.ArrayWrongTypeIsExpectingString:
                    throw new OneArrayException(error);
                case OneError.ClientNotInitialized:
                    throw new OneClientException(error);
                case OneError.CodecHeaderLengthTooSmall:
                case OneError.CodecHeaderLengthTooBig:
                case OneError.CodecDataLengthTooSmallForHeader:
                case OneError.CodecDataLengthTooSmallForPayload:
                case OneError.CodecExpectedDataLengthTooBig:
                case OneError.CodecInvalidMessagePayloadSizeTooBig:
                case OneError.CodecInvalidHeader:
                case OneError.CodecTryingToEncodeUnsupportedOpcode:
                    throw new OneCodecException(error);
                case OneError.ConnectionUninitialized:
                case OneError.ConnectionHandshakeTimeout:
                case OneError.ConnectionHealthTimeout:
                case OneError.ConnectionHelloInvalid:
                case OneError.ConnectionHelloMessageHeaderTooBig:
                case OneError.ConnectionHelloMessageReceiveFailed:
                case OneError.ConnectionHelloMessageReplyInvalid:
                case OneError.ConnectionHelloMessageSendFailed:
                case OneError.ConnectionHelloReceiveFailed:
                case OneError.ConnectionHelloSendFailed:
                case OneError.ConnectionHelloTooBig:
                case OneError.ConnectionHelloVersionMismatch:
                case OneError.ConnectionMessageReceiveFailed:
                case OneError.ConnectionReadTooBigForStream:
                case OneError.ConnectionOutMessageTooBigForStream:
                case OneError.ConnectionQueueEmpty:
                case OneError.ConnectionQueueInsufficientSpace:
                case OneError.ConnectionOutgoingQueueInsufficientSpace:
                case OneError.ConnectionIncomingQueueInsufficientSpace:
                case OneError.ConnectionReceiveBeforeSend:
                case OneError.ConnectionReceiveFail:
                case OneError.ConnectionSendFail:
                case OneError.ConnectionTryAgain:
                case OneError.ConnectionUnknownStatus:
                case OneError.ConnectionUpdateAfterError:
                case OneError.ConnectionUpdateReadyFail:
                    throw new OneConnectionException(error);
                case OneError.MessageAllocationFailed:
                case OneError.MessageCallbackIsNullptr:
                case OneError.MessageIsNullptr:
                case OneError.MessageOpcodeNotMatchingExpectingAllocated:
                case OneError.MessageOpcodeNotMatchingExpectingLiveState:
                case OneError.MessageOpcodeNotMatchingExpectingMetadata:
                case OneError.MessageOpcodeNotMatchingExpectingSoftStop:
                case OneError.MessageOpcodeNotMatchingExpectingHostInformation:
                case OneError.MessageOpcodeNotMatchingExpectingApplicationInstanceInformation:
                case OneError.MessageOpcodeNotMatchingExpectingApplicationInstanceStatus:
                case OneError.MessageOpcodeNotSupported:
                case OneError.MessageOpcodePayloadNotEmpty:
                    throw new OneMessageException(error);
                case OneError.ObjectAllocationFailed:
                case OneError.ObjectKeyNotFound:
                case OneError.ObjectKeyIsNullptr:
                case OneError.ObjectWrongTypeIsExpectingArray:
                case OneError.ObjectWrongTypeIsExpectingBool:
                case OneError.ObjectWrongTypeIsExpectingINT:
                case OneError.ObjectWrongTypeIsExpectingObject:
                case OneError.ObjectWrongTypeIsExpectingString:
                    throw new OneObjectException(error);
                case OneError.PayloadKeyNotFound:
                case OneError.PayloadKeyIsNullptr:
                case OneError.PayloadParseFailed:
                case OneError.PayloadValIsNullptr:
                case OneError.PayloadWrongTypeIsExpectingArray:
                case OneError.PayloadWrongTypeIsExpectingBool:
                case OneError.PayloadWrongTypeIsExpectingINT:
                case OneError.PayloadWrongTypeIsExpectingObject:
                case OneError.PayloadWrongTypeIsExpectingString:
                    throw new OnePayloadException(error);
                case OneError.ServerAllocationFailed:
                case OneError.ServerAlreadyInitialized:
                case OneError.ServerAlreadyListening:
                case OneError.ServerRetryingListen:
                case OneError.ServerCallbackIsNullptr:
                case OneError.ServerConnectionIsNullptr:
                case OneError.ServerConnectionNotReady:
                case OneError.ServerMessageIsNullptr:
                case OneError.ServerSocketAllocationFailed:
                case OneError.ServerSocketNotInitialized:
                case OneError.ServerSocketIsNullptr:
                    throw new OneServerException(error);
                case OneError.SocketAcceptNonBlockingFailed:
                case OneError.SocketAcceptUninitialized:
                case OneError.SocketAddressFailed:
                case OneError.SocketAvailableFailed:
                case OneError.SocketBindFailed:
                case OneError.SocketCloseFailed:
                case OneError.SocketConnectFailed:
                case OneError.SocketConnectNonBlockingFailed:
                case OneError.SocketConnectUninitialized:
                case OneError.SocketCreateFailed:
                case OneError.SocketListenFailed:
                case OneError.SocketListenNonBlockingFailed:
                case OneError.SocketReceiveFailed:
                case OneError.SocketSelectFailed:
                case OneError.SocketSelectUninitialized:
                case OneError.SocketSendFailed:
                case OneError.SocketSocketOptionsFailed:
                case OneError.SocketSystemCleanupFail:
                case OneError.SocketSystemInitFail:
                    throw new OneSocketException(error);
                case OneError.ValidationArrayIsNullptr:
                case OneError.ValidationCallbackIsNullptr:
                case OneError.ValidationCapacityIsNullptr:
                case OneError.ValidationCodeIsNullptr:
                case OneError.ValidationConnectionIsNullptr:
                case OneError.ValidationDataIsNullptr:
                case OneError.ValidationDestinationIsNullptr:
                case OneError.ValidationEmptyIsNullptr:
                case OneError.ValidationKeyIsNullptr:
                case OneError.ValidationMapIsNullptr:
                case OneError.ValidationMessageIsNullptr:
                case OneError.ValidationModeIsNullptr:
                case OneError.ValidationNameIsNullptr:
                case OneError.ValidationObjectIsNullptr:
                case OneError.ValidationResultIsNullptr:
                case OneError.ValidationServerIsNullptr:
                case OneError.ValidationSocketIsNullptr:
                case OneError.ValidationSourceIsNullptr:
                case OneError.ValidationStatusIsNullptr:
                case OneError.ValidationSizeIsNullptr:
                case OneError.ValidationValIsNullptr:
                case OneError.ValidationValSizeIsTooSmall:
                case OneError.ValidationVersionIsNullptr:
                    throw new OneValidationException(error);
                default:
                    throw new OneInvalidErrorCodeException(errorCode);
            }
        }
    }
}