namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a I3D_ERROR_DATA_* error code.
    /// </summary>
    public class I3dDataException : I3dException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="I3dDataException"/>.
        /// </summary>
        public I3dDataException(I3dError error) : base(error)
        {
        }
    }
}