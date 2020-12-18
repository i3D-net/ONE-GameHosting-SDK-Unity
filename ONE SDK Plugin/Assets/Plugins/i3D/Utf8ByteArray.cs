using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace i3D
{
    public class Utf8ByteArray : IDisposable
    {
        private string _str;
        private IntPtr _ptr;
        private readonly bool _shouldFreeMemory;

        public IntPtr Ptr { get { return _ptr; } }

        public Utf8ByteArray(int length)
        {
            _ptr = Marshal.AllocHGlobal(length + 1);
            Marshal.WriteByte(_ptr, length, 0);
        }

        public Utf8ByteArray(string str)
        {
            _str = str;
            _shouldFreeMemory = true;

            byte[] bytes = Encoding.UTF8.GetBytes(str);
            _ptr = Marshal.AllocHGlobal(bytes.Length + 1);
            Marshal.Copy(bytes, 0, _ptr, bytes.Length);
            Marshal.WriteByte(_ptr, bytes.Length, 0);
        }

        public Utf8ByteArray(IntPtr ptr)
        {
            _ptr = ptr;
            _shouldFreeMemory = false;

            ReadPtr();
        }

        public void ReadPtr()
        {
            if (_ptr == IntPtr.Zero)
            {
                _str = null;
                return;
            }

            var data = new List<byte>();
            var offset = 0;

            while (true)
            {
                byte b = Marshal.ReadByte(_ptr, offset++);

                if (b == 0)
                    break;

                data.Add(b);
            }

            _str = Encoding.UTF8.GetString(data.ToArray());
        }

        public void Dispose()
        {
            if (_shouldFreeMemory && _ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_ptr);
                _ptr = IntPtr.Zero;
            }
        }

        public static implicit operator IntPtr(Utf8ByteArray ca)
        {
            return ca._ptr;
        }

        public override string ToString()
        {
            return _str;
        }
    }
}