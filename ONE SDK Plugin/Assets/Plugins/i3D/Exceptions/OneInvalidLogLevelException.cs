using System;

namespace i3D.Exceptions
{
    public class OneInvalidLogLevelException : Exception
    {
        public int Level { get; private set; }

        public OneInvalidLogLevelException(int level)
        {
            Level = level;
            
            // TODO: message
        }
    }
}