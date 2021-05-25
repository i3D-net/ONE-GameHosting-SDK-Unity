namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a I3D_ERROR_SITESGETTER_* error code.
    /// </summary>
    public class I3dSitesGetterException : I3dException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="I3dSitesGetterException"/>.
        /// </summary>
        public I3dSitesGetterException(I3dError error) : base(error)
        {
        }
    }
}