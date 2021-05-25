using System;

namespace i3D.Exceptions
{
    /// <summary>
    /// Base exception class that is inherited by all the exceptions thrown in return to I3D_ERROR_* codes returned by the SDK.
    /// </summary>
    public abstract class I3dException : Exception
    {
        /// <summary>
        /// The error code returned by the SDK.
        /// </summary>
        public I3dError Error { get { return _error; } }

        protected I3dException(I3dError error)
            : base("The SDK returned error: " + error)
        {
            _error = error;
        }

        private readonly I3dError _error;
    }
}