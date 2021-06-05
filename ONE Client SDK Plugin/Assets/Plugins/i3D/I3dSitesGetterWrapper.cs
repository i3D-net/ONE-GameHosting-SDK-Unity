using System;
using System.Collections.Generic;
using i3D.Exceptions;

namespace i3D
{
    /// <summary>
    /// Represents the ONE SitesGetter that handle the retrieval of i3D sites information.
    /// </summary>
    public partial class I3dSitesGetterWrapper : IDisposable
    {
        public IntPtr Ptr {
            get {
                return _ptr;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="I3dSitesGetterWrapper"/> class.
        /// Should be disposed.
        /// </summary>
        /// <param name="logCallback">The logging callback.</param>
        public I3dSitesGetterWrapper()
        {
        }

        /// <summary>
        /// Releases the memory used by the sites information.
        /// </summary>
        public void Dispose()
        {
            i3d_ping_sites_getter_destroy(_ptr);

            lock (SitesGetter)
            {
                SitesGetter.Remove(_ptr);
            }
        }

        /// <summary>
        /// Init an instance of the <see cref="I3dSitesGetterWrapper"/> class.
        /// </summary>
        public void Init(Action<I3dLogLevel, string> logCallback, I3dHttpGetCallback callback, IntPtr userdata)
        {
            _logCallback = logCallback;

            int code = i3d_ping_sites_getter_create(out _ptr, callback, userdata);
            I3dErrorValidator.Validate(code);

            code = i3d_ping_sites_getter_set_logger(_ptr, LogCallback, _ptr);
            I3dErrorValidator.Validate(code);

            lock (SitesGetter)
            {
                SitesGetter.Add(_ptr, this);
            }
        }

        /// <summary>
        /// Updates an instance of the <see cref="I3dSitesGetterWrapper"/> class. This will update the status to ready when the HTTP Get response has been successfully finished.
        /// </summary>
        public void Update()
        {
            int code = i3d_ping_sites_getter_update(_ptr);
            I3dErrorValidator.Validate(code);
        }

        /// <summary>
        /// Returns the status of the pingers.
        /// </summary>
        /// <exception cref="I3dInvalidSitesGetterStatusException">Thrown if the wrapper cannot cast the numeric value
        /// received from the C API to the status enum. </exception>
        public I3dSitesGetterStatus Status
        {
            get
            {
                IntPtr statusPtr;
                int code = i3d_ping_sites_getter_status(_ptr, out statusPtr);
                I3dErrorValidator.Validate(code);

                int status = statusPtr.ToInt32();
                
                if (!Enum.IsDefined(typeof(I3dSitesGetterStatus), status))
                    throw new I3dInvalidSitesGetterStatusException(status);

                return (I3dSitesGetterStatus) status;
            }
        }

        /// <summary>
        /// Gets the size of the Sites that has been retrieved by an instance of the <see cref="I3dSitesGetterWrapper"/> class.
        /// </summary>
        public uint Size()
        {
            uint size = 0;
            int code = i3d_ping_sites_getter_site_list_size(_ptr, out size);
            I3dErrorValidator.Validate(code);
            return size;
        }

        /// <summary>
        /// Retrieves the country name <see cref="string"/> value for the site at the given position.
        /// </summary>
        public string GetCountry(uint position)
        {
            using (var result = new Utf8ByteArray((int)64))
            {
                int code = i3d_ping_sites_getter_list_site_country(_ptr, position, result);
                I3dErrorValidator.Validate(code);

                result.ReadPtr();
                return result.ToString();
            }
        }

        /// <summary>
        /// Retrieves the country name <see cref="string"/> value for the site at the given position.
        /// </summary>
        public string GetDcLocationName(uint position)
        {
            using (var result = new Utf8ByteArray((int)64))
            {
                int code = i3d_ping_sites_getter_list_site_dc_location_name(_ptr, position, result);
                I3dErrorValidator.Validate(code);

                result.ReadPtr();
                return result.ToString();
            }
        }

        /// <summary>
        /// Retrieves the hostname <see cref="string"/> value for the site at the given position.
        /// </summary>
        public string GetHostname(uint position)
        {
            using (var result = new Utf8ByteArray((int)64))
            {
                int code = i3d_ping_sites_getter_list_site_hostname(_ptr, position, result);
                I3dErrorValidator.Validate(code);

                result.ReadPtr();
                return result.ToString();
            }
        }

        /// <summary>
        /// Gets the size of the IPv4 for the site at the given position.
        /// </summary>
        public uint ipv4Size(uint position)
        {
            uint size = 0;
            int code = i3d_ping_sites_getter_list_site_ipv4_size(_ptr, position, out size);
            I3dErrorValidator.Validate(code);
            return size;
        }

        /// <summary>
        /// Retrieves the IPv4 <see cref="string"/> at the ip position in the ip list of the site at the given position.
        /// </summary>
        public string GetIPv4(uint position, uint ipPosition)
        {
            using (var result = new Utf8ByteArray((int)16))
            {
                int code = i3d_ping_sites_getter_list_site_ipv4_ip(_ptr, position, ipPosition, result);
                I3dErrorValidator.Validate(code);

                result.ReadPtr();
                return result.ToString();
            }
        }

        /// <summary>
        /// Gets the size of the IPv6 for the site at the given position.
        /// </summary>
        public uint ipv6Size(uint position)
        {
            uint size = 0;
            int code = i3d_ping_sites_getter_list_site_ipv6_size(_ptr, position, out size);
            I3dErrorValidator.Validate(code);
            return size;
        }

        /// <summary>
        /// Retrieves the IPv6 <see cref="string"/> at the ip position in the ip list of the site at the given position.
        /// </summary>
        public string GetIPv6(uint position, uint ipPosition)
        {
            using (var result = new Utf8ByteArray((int)46))
            {
                int code = i3d_ping_sites_getter_list_site_ipv6_ip(_ptr, position, ipPosition, result);
                I3dErrorValidator.Validate(code);

                result.ReadPtr();
                return result.ToString();
            }
        }

        /// <summary>
        /// Gets IPv4 list for all the sites.
        /// </summary>
        public void ipv4List(I3dIpList list)
        {
            int code = i3d_ping_sites_getter_ipv4_list(_ptr, list.Ptr);
            I3dErrorValidator.Validate(code);
        }

        /// <summary>
        /// Gets IPv6 list for all the sites.
        /// </summary>
        public void ipv6List(I3dIpList list)
        {
            int code = i3d_ping_sites_getter_ipv6_list(_ptr, list.Ptr);
            I3dErrorValidator.Validate(code);
        }

        private static void LogCallback(IntPtr data, int level, IntPtr log)
        {
            I3dSitesGetterWrapper sitesGetter;

            if (!SitesGetter.TryGetValue(data, out sitesGetter))
                throw new InvalidOperationException("Cannot find I3dSitesGetterWrapper instance");

            if (sitesGetter._logCallback == null)
                return;

            if (!Enum.IsDefined(typeof(I3dLogLevel), level))
                throw new I3dInvalidLogLevelException(level);

            var logLevel = (I3dLogLevel) level;

            sitesGetter._logCallback(logLevel, new Utf8ByteArray(log).ToString());
        }

        private IntPtr _ptr;
        private Action<I3dLogLevel, string> _logCallback;
        private static readonly Dictionary<IntPtr, I3dSitesGetterWrapper> SitesGetter = new Dictionary<IntPtr, I3dSitesGetterWrapper>();

    }
}