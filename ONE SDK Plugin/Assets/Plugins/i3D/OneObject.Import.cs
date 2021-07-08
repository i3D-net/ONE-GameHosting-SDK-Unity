using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class OneObject
    {
        private const string DllName = "one_arcus";

        [DllImport(DllName)]
        private static extern int one_object_create(out IntPtr obj);

        [DllImport(DllName)]
        private static extern void one_object_destroy(IntPtr obj);

        [DllImport(DllName)]
        private static extern void one_object_copy(IntPtr obj);

        [DllImport(DllName)]
        private static extern void one_object_clear(IntPtr obj);

        [DllImport(DllName)]
        private static extern int one_object_is_val_bool(IntPtr obj, IntPtr key, out bool result);

        [DllImport(DllName)]
        private static extern int one_object_is_val_int(IntPtr obj, IntPtr key, out bool result);

        [DllImport(DllName)]
        private static extern int one_object_is_val_string(IntPtr obj, IntPtr key, out bool result);

        [DllImport(DllName)]
        private static extern int one_object_is_val_array(IntPtr obj, IntPtr key, out bool result);

        [DllImport(DllName)]
        private static extern int one_object_is_val_object(IntPtr obj, IntPtr key, out bool result);

        [DllImport(DllName)]
        private static extern int one_object_val_bool(IntPtr obj, IntPtr key, out bool val);

        [DllImport(DllName)]
        private static extern int one_object_val_int(IntPtr obj, IntPtr key, out int val);

        [DllImport(DllName)]
        private static extern int one_object_val_string_size(IntPtr obj, IntPtr key, out int size);

        [DllImport(DllName)]
        private static extern int one_object_val_string(IntPtr obj, IntPtr key, IntPtr val, int size);

        [DllImport(DllName)]
        private static extern int one_object_val_array(IntPtr obj, IntPtr key, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_object_val_object(IntPtr obj, IntPtr key, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_object_set_val_bool(IntPtr obj, IntPtr key, bool val);

        [DllImport(DllName)]
        private static extern int one_object_set_val_int(IntPtr obj, IntPtr key, int val);

        [DllImport(DllName)]
        private static extern int one_object_set_val_string(IntPtr obj, IntPtr key, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_object_set_val_array(IntPtr obj, IntPtr key, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_object_set_val_object(IntPtr obj, IntPtr key, IntPtr val);
    }
}