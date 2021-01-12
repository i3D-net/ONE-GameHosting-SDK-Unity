using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace i3D
{
    /// <summary>
    /// Helper class to support marshalling strings in UTF-8 encoding.
    /// </summary>
    internal class Utf8ByteArray : IDisposable
    {
        private string _str;
        private IntPtr _ptr;
        private readonly bool _shouldFreeMemory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Utf8ByteArray"/> class with pre-allocated memory.
        /// Should be disposed.
        /// </summary>
        /// <param name="bytes">Number of bytes to pre-allocate.</param>
        public Utf8ByteArray(int bytes)
        {
            _shouldFreeMemory = true;

            _ptr = Marshal.AllocHGlobal(bytes + 1);
            Marshal.WriteByte(_ptr, bytes, 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Utf8ByteArray"/> class to the specified string.
        /// Should be disposed.
        /// </summary>
        /// <param name="str">The string to be marshalled.</param>
        public Utf8ByteArray(string str)
        {
            _str = str;
            _shouldFreeMemory = true;

            byte[] bytes = Encoding.UTF8.GetBytes(str);
            _ptr = Marshal.AllocHGlobal(bytes.Length + 1);
            Marshal.Copy(bytes, 0, _ptr, bytes.Length);
            Marshal.WriteByte(_ptr, bytes.Length, 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Utf8ByteArray"/> class to the specified pointer.
        /// Should NOT be disposed.
        /// </summary>
        /// <param name="ptr">The pointer to read bytes from.</param>
        public Utf8ByteArray(IntPtr ptr)
        {
            _ptr = ptr;
            _shouldFreeMemory = false;

            ReadPtr();
        }

        /// <summary>
        /// Reads bytes between the pointer and the terminating zero-byte.
        /// </summary>
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

        /// <summary>
        /// Releases the memory used by the array.
        /// NOTE: The memory will not be released if it wasn't allocated in scope of this class.
        /// That is, the pointer was passed to the constructor.
        /// </summary>
        public void Dispose()
        {
            if (_shouldFreeMemory && _ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_ptr);
                _ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Defines an implicit conversion of a given <see cref="Utf8ByteArray"/> to a pointer.
        /// </summary>
        public static implicit operator IntPtr(Utf8ByteArray array)
        {
            return array._ptr;
        }

        /// <summary>
        /// Returns the string representation of the array.
        /// </summary>
        public override string ToString()
        {
            return _str;
        }
    }
}