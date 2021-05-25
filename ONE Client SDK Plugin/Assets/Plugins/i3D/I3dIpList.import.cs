using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class I3dIpList
    {
        private const string DllName = "one_ping";

        [DllImport(DllName)]
        private static extern int i3d_ping_ip_list_create(out IntPtr ipList);

        [DllImport(DllName)]
        private static extern void i3d_ping_ip_list_destroy(IntPtr ipList);
        
        [DllImport(DllName)]
        private static extern int i3d_ping_ip_list_clear(IntPtr ipList);

        [DllImport(DllName)]
        private static extern int i3d_ping_ip_list_push_back(IntPtr ipList, IntPtr ip);

        [DllImport(DllName)]
        private static extern int i3d_ping_ip_list_size(IntPtr ipList, out uint size);
        [DllImport(DllName)]
        private static extern int i3d_ping_ip_list_ip_size(IntPtr ipList, uint pos, out uint size);
        [DllImport(DllName)]
        private static extern int i3d_ping_ip_list_ip(IntPtr ipList, uint pos, IntPtr ip, uint size);
    }
}