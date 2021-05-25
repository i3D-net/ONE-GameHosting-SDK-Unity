using System;
using i3D.Exceptions;

namespace i3D
{
    /// <summary>
    /// Helper class for validation of the error codes returned by the i3D SDK.
    /// For internal use ONLY. 
    /// </summary>
    internal static class I3dErrorValidator
    {
        /// <summary>
        /// Validates the error code and throws an exception if needed.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <exception cref="I3dDataException"/>
        /// <exception cref="I3dIpListException"/>
        /// <exception cref="I3dPingerException"/>
        /// <exception cref="I3dPingersException"/>
        /// <exception cref="I3dSitesGetterException"/>
        /// <exception cref="I3dSocketException"/>
        /// <exception cref="I3dSocketException"/>
        /// <exception cref="I3dValidationException"/>
        public static void Validate(int errorCode)
        {
            if (!Enum.IsDefined(typeof(I3dError), errorCode))
                throw new I3dInvalidErrorCodeException(errorCode);

            var error = (I3dError) errorCode;

            switch (error)
            {
                case I3dError.None:
                    return;
                case I3dError.ValidationCallbackIsNullptr:
                case I3dError.ValidationCountryIsNullptr:
                case I3dError.ValidationDcLocationNameIsNullptr:
                case I3dError.ValidationCallIpV4IsNullptr:
                case I3dError.ValidationIpV6IsNullptr:
                case I3dError.ValidationJsonIsNullptr:
                case I3dError.ValidationSitesGetterIsNullptr:
                case I3dError.ValidationPingersIsNullptr:
                case I3dError.ValidationStatusIsNullptr:
                case I3dError.ValidationSizeIsNullptr:
                case I3dError.ValidationContinentIdIsNullptr:
                case I3dError.ValidationDcLocationIdIsNullptr:
                case I3dError.ValidationIpIsNullptr:
                case I3dError.ValidationLastTimeIsNullptr:
                case I3dError.ValidationAverageTimeIsNullptr:
                case I3dError.ValidationMinTimeIsNullptr:
                case I3dError.ValidationMaxTimeIsNullptr:
                case I3dError.ValidationMedianTimeIsNullptr:
                case I3dError.ValidationPingResponseCountIsNullptr:
                case I3dError.ValidationResultIsNullptr:
                case I3dError.ValidationIpListIsNullptr:
                case I3dError.ValidationHttpGetCallbackIsNullptr:
                case I3dError.ValidationSizeIsTooSmall:
                    throw new I3dValidationException(error);
                case I3dError.DataParseFailed:
                case I3dError.DataJsonPayloadIsNullptr:
                case I3dError.DataJsonServerInformationIsInvalid:
                    throw new I3dDataException(error);
                case I3dError.SitesGetterNotInitialized:
                case I3dError.SitesGetterAlreadyInitialized:
                case I3dError.SitesGetterCallbackIsNullptr:
                case I3dError.SitesGetterAllocationFailed:
                case I3dError.SitesGetterUnknownStatus:
                case I3dError.SitesGetterHttpGetCallbackNotSet:
                case I3dError.SitesGetterPosOutOfRange:
                case I3dError.SitesGetterIpPosOutOfRange:
                    throw new I3dSitesGetterException(error);
                case I3dError.IpListPosIsOutOfRange:
                case I3dError.IpListAllocationFailed:
                    throw new I3dIpListException(error);
                case I3dError.SocketSystemInitFail:
                case I3dError.SocketCreationFail:
                case I3dError.SocketTtlSetSocketOptionFail:
                case I3dError.SocketSystemCleanupFail:
                case I3dError.SocketInvalidIpV4:
                case I3dError.SocketSendFail:
                case I3dError.SocketReceiveBufferTooSmall:
                case I3dError.SocketReceiveError:
                case I3dError.SocketTooFewBytes:
                case I3dError.SocketDestinationUnreachable:
                case I3dError.SocketUnknownIcmpPacketType:
                case I3dError.SocketTtlExpired:
                case I3dError.SocketInvalidTime:
                case I3dError.SocketAlreadyInitialized:
                case I3dError.SocketNotInitialized:
                case I3dError.SocketPingNotSent:
                case I3dError.SocketUnknownStatus:
                case I3dError.SocketInvalidSequenceNumber:
                case I3dError.SocketSelectFailed:
                case I3dError.SocketNotReady:
                case I3dError.SocketCannotBeReset:
                    throw new I3dSocketException(error);
                case I3dError.PingerDivisionByZero:
                case I3dError.PingerInvalidTime:
                case I3dError.PingerAlreadyInitialized:
                case I3dError.PingerIsUninitialized:
                    throw new I3dPingerException(error);
                case I3dError.PingersNotInitialized:
                case I3dError.PingersAlreadyInitialized:
                case I3dError.PingersPosIsOutOfRange:
                    throw new I3dPingersException(error);
                default:
                    throw new I3dInvalidErrorCodeException(errorCode);
            }
        }
    }
}