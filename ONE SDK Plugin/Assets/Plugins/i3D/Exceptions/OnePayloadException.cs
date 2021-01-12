namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a ONE_ERROR_PAYLOAD_* error code.
    /// </summary>
    public class OnePayloadException : OneException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnePayloadException"/>.
        /// </summary>
        public OnePayloadException(OneError error) : base(error)
        {
        }
    }
}