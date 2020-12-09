using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class OneObject : IDisposable
    {
        internal IntPtr Ptr { get { return _ptr; } }

        private readonly IntPtr _ptr;

        public OneObject()
        {
            IntPtr ptr;

            int code = one_object_create(out ptr);

            OneErrorValidator.Validate(code);

            _ptr = Marshal.ReadIntPtr(ptr);
        }

        public OneObject(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException("ptr");

            _ptr = ptr;
        }

        public bool IsBool(string key)
        {
            bool result;
            int code = one_object_is_val_bool(_ptr, key, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsInt(string key)
        {
            bool result;
            int code = one_object_is_val_int(_ptr, key, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsString(string key)
        {
            bool result;
            int code = one_object_is_val_string(_ptr, key, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsArray(string key)
        {
            bool result;
            int code = one_object_is_val_array(_ptr, key, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsObject(string key)
        {
            bool result;
            int code = one_object_is_val_object(_ptr, key, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool GetBool(string key)
        {
            bool value;
            int code = one_object_val_bool(_ptr, key, out value);

            OneErrorValidator.Validate(code);

            return value;
        }

        public int GetInt(string key)
        {
            int value;
            int code = one_object_val_int(_ptr, key, out value);

            OneErrorValidator.Validate(code);

            return value;
        }

        public int GetStringSize(string key)
        {
            int size;
            int code = one_object_val_string_size(_ptr, key, out size);

            OneErrorValidator.Validate(code);

            return size;
        }

        public string GetString(string key)
        {
            int size = GetStringSize(key);
            char[] chars = new char[size];

            int code = one_object_val_string(_ptr, key, chars, size);
            OneErrorValidator.Validate(code);

            return new string(chars);
        }

        public OneArray GetArray(string key)
        {
            IntPtr ptr;
            int code = one_object_val_array(_ptr, key, out ptr);

            OneErrorValidator.Validate(code);

            return new OneArray(ptr);
        }

        public OneObject GetObject(string key)
        {
            IntPtr ptr;
            int code = one_object_val_object(_ptr, key, out ptr);

            OneErrorValidator.Validate(code);

            return new OneObject(ptr);
        }

        public void SetBool(string key, bool value)
        {
            int code = one_object_set_val_bool(_ptr, key, value);

            OneErrorValidator.Validate(code);
        }

        public void SetInt(string key, int value)
        {
            int code = one_object_set_val_int(_ptr, key, value);

            OneErrorValidator.Validate(code);
        }

        public void SetString(string key, string value)
        {
            int code = one_object_set_val_string(_ptr, key, value);

            OneErrorValidator.Validate(code);
        }

        public void SetArray(string key, OneArray value)
        {
            int code = one_object_set_val_array(_ptr, key, value.Ptr);

            OneErrorValidator.Validate(code);
        }

        public void SetObject(string key, OneObject value)
        {
            int code = one_object_set_val_object(_ptr, key, value._ptr);

            OneErrorValidator.Validate(code);
        }

        public void Dispose()
        {
            one_object_destroy(_ptr);
        }
    }
}