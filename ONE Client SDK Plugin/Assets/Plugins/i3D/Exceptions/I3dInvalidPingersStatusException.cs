﻿using System;

namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a pingers status that cannot be mapped to an enum.
    /// </summary>
    public class I3dInvalidPingersStatusException : Exception
    {
        /// <summary>
        /// The server status that cannot be mapped.
        /// </summary>
        public int Status { get { return _status; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="I3dInvalidPingersStatusException"/>.
        /// </summary>
        public I3dInvalidPingersStatusException(int status)
            : base(string.Format("Unexpected server status received: {0}", status))
        {
            _status = status;
        }

        private readonly int _status;
    }
}