namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a ONE_ERROR_VALIDATION_* error code.
    /// </summary>
    public class OneValidationException : OneException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneValidationException"/>.
        /// </summary>
        public OneValidationException(OneError error) : base(error)
        {
        }
    }
}