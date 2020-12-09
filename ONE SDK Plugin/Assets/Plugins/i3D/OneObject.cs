using System;

namespace i3D
{
    public partial class OneObject
    {
        internal IntPtr Ptr { get { return _ptr; } }

        private readonly IntPtr _ptr;

        public OneObject()
        {
            // TODO: _ptr initialization
        }

        public OneObject(IntPtr ptr)
        {
            _ptr = ptr;
        }
    }
}