namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a ONE_ERROR_OBJECT_* error code.
    /// </summary>
    public class OneObjectException : OneException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneObjectException"/>.
        /// </summary>
        public OneObjectException(OneError error) : base(error)
        {
        }
    }
}