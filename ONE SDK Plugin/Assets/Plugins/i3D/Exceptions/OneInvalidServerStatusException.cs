using System;

namespace i3D.Exceptions
{
    public class OneInvalidServerStatusException : Exception
    {
        public int Status { get; private set; }

        public OneInvalidServerStatusException(int status)
        {
            Status = status;
            
            // TODO: message
        }
    }
}