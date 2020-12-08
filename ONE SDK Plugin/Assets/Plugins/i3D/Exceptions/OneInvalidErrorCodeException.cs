using System;

namespace i3D.Exceptions
{
    public class OneInvalidErrorCodeException : Exception
    {
        public int Code { get { return _code; } }

        private readonly int _code;

        public OneInvalidErrorCodeException(int code)
        {
            _code = code;
            
            // TODO: message
        }
    }
}