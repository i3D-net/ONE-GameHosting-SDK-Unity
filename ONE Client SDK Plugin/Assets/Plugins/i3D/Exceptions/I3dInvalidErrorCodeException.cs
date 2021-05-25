using System;

namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the ONE Client SDK has returned an error code that cannot be mapped to an enum.
    /// </summary>
    public class I3dInvalidErrorCodeException : Exception
    {
        /// <summary>
        /// The code that cannot be mapped.
        /// </summary>
        public int Code { get { return _code; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="I3dInvalidErrorCodeException"/>.
        /// </summary>
        public I3dInvalidErrorCodeException(int code)
            : base(string.Format("Unexpected error code received: {0}", code))
        {
            _code = code;
        }

        private readonly int _code;
    }
}