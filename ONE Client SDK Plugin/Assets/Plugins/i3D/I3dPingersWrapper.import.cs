using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class I3dPingersWrapper
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void I3dLoggerAction(IntPtr data, int level, IntPtr log);

        private const string DllName = "one_ping";

        [DllImport(DllName)]
        private static extern int i3d_ping_pingers_create(out IntPtr pingers);

        [DllImport(DllName)]
        private static extern int i3d_ping_pingers_set_logger(IntPtr pingers, I3dLoggerAction logCb, IntPtr userdata);

        [DllImport(DllName)]
        private static extern void i3d_ping_pingers_destroy(IntPtr pingers);

        [DllImport(DllName)]
        private static extern int i3d_ping_pingers_init(IntPtr pingers, IntPtr ipList);
        
        [DllImport(DllName)]
        private static extern int i3d_ping_pingers_update(IntPtr pingers);

        [DllImport(DllName)]
        private static extern int i3d_ping_pingers_status(IntPtr pingers, out IntPtr status);

        [DllImport(DllName)]
        private static extern int i3d_ping_pingers_size(IntPtr pingers, out uint size);

        [DllImport(DllName)]
        private static extern int i3d_ping_pingers_statistics(IntPtr pingers, uint pos, out int lastTime, out double averageTime, out int minTime, out int maxTime, out double medianTime, out uint pingResponseCount);

        [DllImport(DllName)]
        private static extern int i3d_ping_pingers_at_least_one_site_has_been_pinged(IntPtr pingers, out bool result);
   
        [DllImport(DllName)]
        private static extern int i3d_ping_pingers_all_sites_have_been_pinged(IntPtr pingers, out bool result);
    }
}