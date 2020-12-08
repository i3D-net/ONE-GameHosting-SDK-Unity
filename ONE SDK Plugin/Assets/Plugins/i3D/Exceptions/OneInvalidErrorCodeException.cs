using System;

namespace i3D.Exceptions
{
    public class OneInvalidErrorCodeException : Exception
    {
        public int Code { get; private set; }

        public OneInvalidErrorCodeException(int code)
        {
            Code = code;
            
            // TODO: message
        }
    }
}