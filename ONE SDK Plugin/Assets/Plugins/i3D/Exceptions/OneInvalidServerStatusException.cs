using System;

namespace i3D.Exceptions
{
    public class OneInvalidServerStatusException : Exception
    {
        public int Status { get { return _status; } }

        private readonly int _status;

        public OneInvalidServerStatusException(int status)
        {
            _status = status;
            
            // TODO: message
        }
    }
}