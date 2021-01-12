namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a ONE_ERROR_SERVER_* error code.
    /// </summary>
    public class OneServerException : OneException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneServerException"/>.
        /// </summary>
        public OneServerException(OneError error) : base(error)
        {
        }
    }
}