namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a ONE_ERROR_SOCKET_* error code.
    /// </summary>
    public class OneSocketException : OneException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneSocketException"/>.
        /// </summary>
        public OneSocketException(OneError error) : base(error)
        {
        }
    }
}