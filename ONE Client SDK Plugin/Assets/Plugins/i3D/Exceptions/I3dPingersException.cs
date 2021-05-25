namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a I3D_ERROR_PINGERS_* error code.
    /// </summary>
    public class I3dPingersException : I3dException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="I3dPingersException"/>.
        /// </summary>
        public I3dPingersException(I3dError error) : base(error)
        {
        }
    }
}