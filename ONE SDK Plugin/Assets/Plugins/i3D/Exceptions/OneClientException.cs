namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a ONE_ERROR_CLIENT_* error code.
    /// </summary>
    public class OneClientException : OneException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneClientException"/>.
        /// </summary>
        public OneClientException(OneError error) : base(error)
        {
        }
    }
}