using System;

namespace i3D.Exceptions
{
    public class OneException : Exception
    {
        public OneError Error { get { return _error; } }

        private readonly OneError _error;

        protected OneException(OneError error)
            : base("The SDK returned error: " + error)
        {
            _error = error;
        }
    }
}