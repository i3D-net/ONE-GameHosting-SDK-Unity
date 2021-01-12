namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a ONE_ERROR_CONNECTION_* error code.
    /// </summary>
    public class OneConnectionException : OneException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneConnectionException"/>.
        /// </summary>
        public OneConnectionException(OneError error) : base(error)
        {
        }
    }
}