using System;

namespace i3D
{
    /// <summary>
    /// Represents the ONE IP List that represents a list of IPv4.
    /// </summary>
    public partial class I3dIpList : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="I3dIpList"/>. Should be disposed.
        /// </summary>
        public I3dIpList()
        {
            int code = i3d_ping_ip_list_create(out _ptr);
            I3dErrorValidator.Validate(code);
        }

        /// <summary>
        /// Releases the memory used by the array.
        /// </summary>
        public void Dispose()
        {
            i3d_ping_ip_list_destroy(_ptr);
        }

        /// <summary>
        /// Clears the list to an empty initialized state.
        /// </summary>
        public void Clear()
        {
            int code = i3d_ping_ip_list_clear(_ptr);
            I3dErrorValidator.Validate(code);
        }

        /// <summary>
        /// Adds an Ip into the list.
        /// </summary>
        public void PushIp(string value)
        {
            int code;

            using (var value1 = new Utf8ByteArray(value))
            {
                code = i3d_ping_ip_list_push_back(_ptr, value1);
            }

            I3dErrorValidator.Validate(code);
        }

        /// <summary>
        /// Returns the number of elements in the list.
        /// </summary>
        public uint Size
        {
            get
            {
                uint size;
                int code = i3d_ping_ip_list_size(_ptr, out size);
                I3dErrorValidator.Validate(code);

                return size;
            }
        }

        /// <summary>
        /// Retrieves the IP <see cref="string"/> value from the ip list.
        /// </summary>
        public string GetIp(uint position)
        {
            uint size = GetStringSize(position);

            using (var result = new Utf8ByteArray((int)size))
            {
                int code = i3d_ping_ip_list_ip(_ptr, position, result, size);
                I3dErrorValidator.Validate(code);

                result.ReadPtr();
                return result.ToString();
            }
        }

        /// <summary>
        /// C pointer of the array. For internal use ONLY.
        /// </summary>
        internal IntPtr Ptr { get { return _ptr; } }
        private readonly IntPtr _ptr;

        /// <summary>
        /// Initializes a new instance of the <see cref="I3dIpList"/> for a specific C pointer.
        /// For internal use ONLY. Should NOT be disposed.
        /// </summary>
        /// <param name="ptr">The pointer.</param>
        /// <exception cref="ArgumentNullException"/>
        internal I3dIpList(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException("ptr");

            _ptr = ptr;
        }

        private uint GetStringSize(uint position)
        {
            uint size;
            int code = i3d_ping_ip_list_ip_size(_ptr, position, out size);
            I3dErrorValidator.Validate(code);
            return size;
        }
    }
}