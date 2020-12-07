using System;

namespace i3D
{
    public class OneArray
    {
        internal IntPtr Ptr { get { return _ptr; } }

        private readonly IntPtr _ptr;

        public OneArray()
        {
            // TODO: _ptr initialization
        }

        public OneArray(IntPtr ptr)
        {
            _ptr = ptr;
        }
    }
}