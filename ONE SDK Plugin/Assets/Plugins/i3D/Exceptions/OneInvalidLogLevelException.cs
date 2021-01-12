using System;

namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a log level that cannot be mapped to an enum.
    /// </summary>
    public class OneInvalidLogLevelException : Exception
    {
        /// <summary>
        /// The log level that cannot be mapped.
        /// </summary>
        public int Level { get { return _level; } }

        private readonly int _level;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneInvalidLogLevelException"/>.
        /// </summary>
        public OneInvalidLogLevelException(int level)
            : base(string.Format("Unexpected log level received: {0}", level))
        {
            _level = level;
        }
    }
}