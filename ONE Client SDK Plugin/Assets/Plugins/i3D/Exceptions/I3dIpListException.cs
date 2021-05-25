namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a I3D_ERROR_IP_LIST_* error code.
    /// </summary>
    public class I3dIpListException : I3dException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="I3dIpListException"/>.
        /// </summary>
        public I3dIpListException(I3dError error) : base(error)
        {
        }
    }
}