﻿using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class OneServer
    {
        private const string DllName = "clib.dll";

        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_server_create(Action<int, string> logFn, out IntPtr server);

        [DllImport(DllName)]
        private static extern void one_server_destroy(IntPtr server);

        [DllImport(DllName)]
        private static extern int one_server_shutdown(IntPtr server);

        [DllImport(DllName)]
        private static extern int one_server_listen(IntPtr server, ushort port);

        [DllImport(DllName)]
        private static extern int one_server_update(IntPtr server);

        [DllImport(DllName)]
        private static extern int one_server_status(IntPtr server, out IntPtr status);

        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_server_set_live_state(
            IntPtr server, int players, int max_players, string name,
            string map, string mode, string version, IntPtr additional_data);

        [DllImport(DllName)]
        private static extern int one_server_set_application_instance_status(
            IntPtr server, int status);

        [DllImport(DllName)]
        private static extern int one_server_set_soft_stop_callback(
            IntPtr server, Action<IntPtr, int> callback, IntPtr data);

        [DllImport(DllName)]
        private static extern int one_server_set_allocated_callback(
            IntPtr server, Action<IntPtr, IntPtr> callback, IntPtr data);

        [DllImport(DllName)]
        private static extern int one_server_set_metadata_callback(
            IntPtr server, Action<IntPtr, IntPtr> callback, IntPtr data);

        [DllImport(DllName)]
        private static extern int one_server_set_host_information_callback(
            IntPtr server, Action<IntPtr, IntPtr> callback, IntPtr data);

        [DllImport(DllName)]
        private static extern int one_server_set_application_instance_information_callback(
            IntPtr server, Action<IntPtr, IntPtr> callback, IntPtr data);
    }
}