namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a I3D_ERROR_PINGER_* error code.
    /// </summary>
    public class I3dPingerException : I3dException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="I3dPingerException"/>.
        /// </summary>
        public I3dPingerException(I3dError error) : base(error)
        {
        }
    }
}