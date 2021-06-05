using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class I3dSitesGetterWrapper
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void I3dGlobalAlloc(uint bytes);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void I3dGlobalFree(IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void I3dGlobalRealloc(IntPtr ptr, uint bytes);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void I3dLoggerAction(IntPtr data, int level, IntPtr log);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void I3dHttpParsingCallback(bool success, IntPtr json, IntPtr parsingUserdata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void I3dHttpGetCallback(IntPtr url, I3dHttpParsingCallback parsingCallback, IntPtr parsingUserdata, IntPtr httpGETUserdata);
        private const string DllName = "one_ping";

        // The following functions will set the global allocation callbacks used by the one_ping lib.
        [DllImport(DllName)]
        private static extern int i3d_ping_allocator_set_alloc(I3dGlobalAlloc alloc);

        [DllImport(DllName)]
        private static extern int i3d_ping_allocator_set_free(I3dGlobalFree free);

        [DllImport(DllName)]
        private static extern int i3d_ping_allocator_set_realloc(I3dGlobalRealloc realloc);
        // Global allocation callbacks end.

        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_create(out IntPtr sitesGetter, I3dHttpGetCallback callback, IntPtr userdata);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_set_logger(IntPtr sitesGetter, I3dLoggerAction logCb, IntPtr userdata);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_destroy(IntPtr sitesGetter);

        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_update(IntPtr sitesGetter);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_status(IntPtr sitesGetter, out IntPtr status);

        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_site_list_size(IntPtr sitesGetter, out uint size);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_list_site_continent_id(IntPtr sitesGetter, uint pos, out int continentID);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_list_site_country(IntPtr sitesGetter, uint pos, IntPtr country);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_list_site_dc_location_id(IntPtr sitesGetter, uint pos, out int dcLocationID);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_list_site_dc_location_name(IntPtr sitesGetter, uint pos, IntPtr dcLocationName);

        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_list_site_hostname(IntPtr sitesGetter, uint pos, IntPtr hostname);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_list_site_ipv4_size(IntPtr sitesGetter, uint pos, out uint size);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_list_site_ipv4_ip(IntPtr sitesGetter, uint pos, uint ipPos, IntPtr ip);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_list_site_ipv6_size(IntPtr sitesGetter, uint pos, out uint size);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_list_site_ipv6_ip(IntPtr sitesGetter, uint pos, uint ipPos, IntPtr ip);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_ipv4_list(IntPtr sitesGetter, IntPtr ipList);
        [DllImport(DllName)]
        private static extern int i3d_ping_sites_getter_ipv6_list(IntPtr sitesGetter, IntPtr ipList);
    }
}