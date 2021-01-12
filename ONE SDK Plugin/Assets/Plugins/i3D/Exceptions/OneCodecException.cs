namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a ONE_ERROR_CODEC_* error code.
    /// </summary>
    public class OneCodecException : OneException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneCodecException"/>.
        /// </summary>
        public OneCodecException(OneError error) : base(error)
        {
        }
    }
}