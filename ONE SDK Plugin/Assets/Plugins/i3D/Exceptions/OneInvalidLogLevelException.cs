using System;

namespace i3D.Exceptions
{
    public class OneInvalidLogLevelException : Exception
    {
        public int Level { get { return _level; } }

        private readonly int _level;

        public OneInvalidLogLevelException(int level)
        {
            _level = level;
            
            // TODO: message
        }
    }
}