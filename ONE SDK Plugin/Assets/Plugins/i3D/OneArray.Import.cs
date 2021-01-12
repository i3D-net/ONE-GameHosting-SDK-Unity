using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class OneArray
    {
        private const string DllName = "one_arcus";

        [DllImport(DllName)]
        private static extern int one_array_create(out IntPtr array);

        [DllImport(DllName)]
        private static extern void one_array_destroy(IntPtr array);

        [DllImport(DllName)]
        private static extern int one_array_copy(IntPtr source, IntPtr destination);

        [DllImport(DllName)]
        private static extern int one_array_clear(IntPtr array);

        [DllImport(DllName)]
        private static extern int one_array_reserve(IntPtr array, int size);

        [DllImport(DllName)]
        private static extern int one_array_is_empty(IntPtr array, out bool empty);

        [DllImport(DllName)]
        private static extern int one_array_size(IntPtr array, out int size);

        [DllImport(DllName)]
        private static extern int one_array_capacity(IntPtr array, out int capacity);

        [DllImport(DllName)]
        private static extern int one_array_push_back_bool(IntPtr array, bool val);

        [DllImport(DllName)]
        private static extern int one_array_push_back_int(IntPtr array, int val);

        [DllImport(DllName)]
        private static extern int one_array_push_back_string(IntPtr array, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_array_push_back_array(IntPtr array, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_array_push_back_object(IntPtr array, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_array_pop_back(IntPtr array);

        [DllImport(DllName)]
        private static extern int one_array_is_val_bool(IntPtr array, uint pos, out bool result);

        [DllImport(DllName)]
        private static extern int one_array_is_val_int(IntPtr array, uint pos, out bool result);

        [DllImport(DllName)]
        private static extern int one_array_is_val_string(IntPtr array, uint pos, out bool result);

        [DllImport(DllName)]
        private static extern int one_array_is_val_array(IntPtr array, uint pos, out bool result);

        [DllImport(DllName)]
        private static extern int one_array_is_val_object(IntPtr array, uint pos, out bool result);

        [DllImport(DllName)]
        private static extern int one_array_val_bool(IntPtr array, uint pos, out bool val);

        [DllImport(DllName)]
        private static extern int one_array_val_int(IntPtr array, uint pos, out int val);

        [DllImport(DllName)]
        private static extern int one_array_val_string_size(IntPtr array, uint pos, out int size);

        [DllImport(DllName)]
        private static extern int one_array_val_string(IntPtr array, uint pos, IntPtr val, int size);

        [DllImport(DllName)]
        private static extern int one_array_val_array(IntPtr array, uint pos, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_array_val_object(IntPtr array, uint pos, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_array_set_val_bool(IntPtr array, uint pos, bool val);

        [DllImport(DllName)]
        private static extern int one_array_set_val_int(IntPtr array, uint pos, int val);

        [DllImport(DllName)]
        private static extern int one_array_set_val_string(IntPtr array, uint pos, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_array_set_val_array(IntPtr array, uint pos, IntPtr val);

        [DllImport(DllName)]
        private static extern int one_array_set_val_object(IntPtr array, uint pos, IntPtr val);
    }
}