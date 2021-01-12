using System;

namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK has returned an error code that cannot be mapped to an enum.
    /// </summary>
    public class OneInvalidErrorCodeException : Exception
    {
        /// <summary>
        /// The code that cannot be mapped.
        /// </summary>
        public int Code { get { return _code; } }

        private readonly int _code;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneInvalidErrorCodeException"/>.
        /// </summary>
        public OneInvalidErrorCodeException(int code)
            : base(string.Format("Unexpected error code received: {0}", code))
        {
            _code = code;
        }
    }
}