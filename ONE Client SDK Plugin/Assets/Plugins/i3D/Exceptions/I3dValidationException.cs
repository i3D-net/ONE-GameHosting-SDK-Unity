namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a I3D_ERROR_VALIDATION_* error code.
    /// </summary>
    public class I3dValidationException : I3dException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="I3dValidationException"/>.
        /// </summary>
        public I3dValidationException(I3dError error) : base(error)
        {
        }
    }
}