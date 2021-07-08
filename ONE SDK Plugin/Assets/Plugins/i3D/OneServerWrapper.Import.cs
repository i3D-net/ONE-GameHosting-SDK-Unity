using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class OneServerWrapper
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OneGlobalAlloc(uint bytes);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OneGlobalFree(IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OneGlobalRealloc(IntPtr ptr, uint bytes);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OneLoggerAction(IntPtr data, int level, IntPtr log);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OneSoftStopAction(IntPtr data, int timeout);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OneArrayAction(IntPtr data, IntPtr array);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OneObjectAction(IntPtr data, IntPtr obj);

        private const string DllName = "one_arcus";

        // The following functions will set the global allocation callbacks used by the one_arcus lib.
        [DllImport(DllName)]
        private static extern int one_allocator_set_alloc(OneGlobalAlloc alloc);

        [DllImport(DllName)]
        private static extern int one_allocator_set_free(OneGlobalFree free);

        [DllImport(DllName)]
        private static extern int one_allocator_set_realloc(OneGlobalRealloc realloc);
        // Global allocation callbacks end.

        [DllImport(DllName)]
        private static extern int one_server_create(ushort port, out IntPtr server);

        [DllImport(DllName)]
        private static extern int one_server_set_logger(IntPtr server, OneLoggerAction logger, IntPtr userdata);

        [DllImport(DllName)]
        private static extern void one_server_destroy(IntPtr server);

        [DllImport(DllName)]
        private static extern int one_server_update(IntPtr server);

        [DllImport(DllName)]
        private static extern int one_server_status(IntPtr server, out IntPtr status);

        [DllImport(DllName)]
        private static extern int one_server_set_live_state(
            IntPtr server, int players, int max_players, IntPtr name,
            IntPtr map, IntPtr mode, IntPtr version, IntPtr additional_data);

        [DllImport(DllName)]
        private static extern int one_server_send_reverse_metadata(
            IntPtr server, IntPtr array);

        [DllImport(DllName)]
        private static extern int one_server_set_application_instance_status(
            IntPtr server, int status);

        [DllImport(DllName)]
        private static extern int one_server_set_soft_stop_callback(
            IntPtr server, OneSoftStopAction callback, IntPtr data);

        [DllImport(DllName)]
        private static extern int one_server_set_allocated_callback(
            IntPtr server, OneArrayAction callback, IntPtr data);

        [DllImport(DllName)]
        private static extern int one_server_set_metadata_callback(
            IntPtr server, OneArrayAction callback, IntPtr data);

        [DllImport(DllName)]
        private static extern int one_server_set_host_information_callback(
            IntPtr server, OneObjectAction callback, IntPtr data);

        [DllImport(DllName)]
        private static extern int one_server_set_application_instance_information_callback(
            IntPtr server, OneObjectAction callback, IntPtr data);

        [DllImport(DllName)]
        private static extern int one_server_set_custom_command_callback(
            IntPtr server, OneArrayAction callback, IntPtr data);
    }
}