using System;

namespace i3D.Exceptions
{
    /// <summary>
    /// Base exception class that is inherited by all the exceptions thrown in return to ONE_ERROR_* codes returned by the SDK.
    /// </summary>
    public abstract class OneException : Exception
    {
        /// <summary>
        /// The error code returned by the SDK.
        /// </summary>
        public OneError Error { get { return _error; } }

        private readonly OneError _error;

        protected OneException(OneError error)
            : base("The SDK returned error: " + error)
        {
            _error = error;
        }
    }
}