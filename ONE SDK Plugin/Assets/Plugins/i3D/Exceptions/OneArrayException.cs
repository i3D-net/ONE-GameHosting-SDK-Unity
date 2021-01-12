namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a ONE_ERROR_ARRAY_* error code.
    /// </summary>
    public class OneArrayException : OneException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneArrayException"/>.
        /// </summary>
        public OneArrayException(OneError error) : base(error)
        {
        }
    }
}