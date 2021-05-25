namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a I3D_ERROR_SOCKET_* error code.
    /// </summary>
    public class I3dSocketException : I3dException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="I3dSocketException"/>.
        /// </summary>
        public I3dSocketException(I3dError error) : base(error)
        {
        }
    }
}