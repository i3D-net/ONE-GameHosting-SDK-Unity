using System;
using System.Collections.Generic;
using i3D.Exceptions;

namespace i3D
{
    /// <summary>
    /// Represents the ONE Pingers that handle the pinging of different IPs.
    /// </summary>
    public partial class I3dPingersWrapper : IDisposable
    {
        /// <summary>
        /// Creates a new instance of the <see cref="I3dPingersWrapper"/> class.
        /// Should be disposed.
        /// </summary>
        /// <param name="logCallback">The logging callback.</param>
        public I3dPingersWrapper(Action<I3dLogLevel, string> logCallback, I3dIpList ipList)
        {
            _logCallback = logCallback;

            int code = i3d_ping_pingers_create(out _ptr, ipList.Ptr);
            I3dErrorValidator.Validate(code);

            code = i3d_ping_pingers_set_logger(_ptr, LogCallback, _ptr);
            I3dErrorValidator.Validate(code);

            lock (Pingers)
            {
                Pingers.Add(_ptr, this);
            }
        }

        /// <summary>
        /// Updates an instance of the <see cref="I3dPingersWrapper"/> class. This will update the ping statistics.
        /// </summary>
        public void Update()
        {
            int code = i3d_ping_pingers_update(_ptr);
            I3dErrorValidator.Validate(code);
        }

        /// <summary>
        /// Returns the status of the pingers.
        /// </summary>
        /// <exception cref="I3dInvalidPingersStatusException">Thrown if the wrapper cannot cast the numeric value
        /// received from the C API to the status enum. </exception>
        public I3dPingersStatus Status
        {
            get
            {
                IntPtr statusPtr;
                int code = i3d_ping_pingers_status(_ptr, out statusPtr);
                I3dErrorValidator.Validate(code);

                int status = statusPtr.ToInt32();
                
                if (!Enum.IsDefined(typeof(I3dPingersStatus), status))
                    throw new I3dInvalidPingersStatusException(status);

                return (I3dPingersStatus) status;
            }
        }

        /// <summary>
        /// Gets the size of the IpList being pinged by an instance of the <see cref="I3dPingersWrapper"/> class.
        /// </summary>
        public uint Size()
        {
            uint size = 0;
            int code = i3d_ping_pingers_size(_ptr, out size);
            I3dErrorValidator.Validate(code);
            return size;
        }

        /// <summary>
        /// Gets the ping statistics for the pinger at position pos.
        /// </summary>
        public void Statistics(uint pos, out int lastTime, out double averageTime, out int minTime, out int maxTime, out double medianTime, out uint pingResponseCount)
        {
            int code = i3d_ping_pingers_statistics(_ptr, pos, out lastTime, out averageTime, out minTime, out maxTime, out medianTime, out pingResponseCount);
            I3dErrorValidator.Validate(code);
        }

        /// <summary>
        /// Returns true if at least one site has been pinged successfully.
        /// </summary>
        public bool AtLeastOneSiteHasBeenPinged()
        {
            bool atLeastOne = false;
            int code = i3d_ping_pingers_at_least_one_site_has_been_pinged(_ptr, out atLeastOne);
            I3dErrorValidator.Validate(code);
            return atLeastOne;
        }

        /// <summary>
        /// Returns true if all sites have been pinged successfully.
        /// </summary>
        public bool AllSitesHaveBeenPigned()
        {
            bool allPinged = false;
            int code = i3d_ping_pingers_all_sites_have_been_pinged(_ptr, out allPinged);
            I3dErrorValidator.Validate(code);
            return allPinged;
        }

        /// <summary>
        /// Releases the memory used by the pingers.
        /// </summary>
        public void Dispose()
        {
            i3d_ping_pingers_destroy(_ptr);

            lock (Pingers)
            {
                Pingers.Remove(_ptr);
            }
        }

        private readonly IntPtr _ptr;
        private readonly Action<I3dLogLevel, string> _logCallback;
        private static readonly Dictionary<IntPtr, I3dPingersWrapper> Pingers = new Dictionary<IntPtr, I3dPingersWrapper>();

        private static void LogCallback(IntPtr data, int level, IntPtr log)
        {
            I3dPingersWrapper pingers;

            if (!Pingers.TryGetValue(data, out pingers))
                throw new InvalidOperationException("Cannot find I3dPingersWrapper instance");

            if (pingers._logCallback == null)
                return;

            if (!Enum.IsDefined(typeof(I3dLogLevel), level))
                throw new I3dInvalidLogLevelException(level);

            var logLevel = (I3dLogLevel) level;

            pingers._logCallback(logLevel, new Utf8ByteArray(log).ToString());
        }
    }
}