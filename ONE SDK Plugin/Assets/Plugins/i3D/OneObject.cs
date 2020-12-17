using System;

namespace i3D
{
    public partial class OneObject : IDisposable
    {
        internal IntPtr Ptr { get { return _ptr; } }

        private readonly IntPtr _ptr;

        public OneObject()
        {
            int code = one_object_create(out _ptr);
            OneErrorValidator.Validate(code);
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
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_is_val_bool(_ptr, key8, out result);
            }

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsInt(string key)
        {
            bool result;
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_is_val_int(_ptr, key8, out result);
            }

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsString(string key)
        {
            bool result;
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_is_val_string(_ptr, key8, out result);
            }

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsArray(string key)
        {
            bool result;
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_is_val_array(_ptr, key8, out result);
            }

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsObject(string key)
        {
            bool result;
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_is_val_object(_ptr, key8, out result);
            }

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool GetBool(string key)
        {
            bool value;
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_val_bool(_ptr, key8, out value);
            }

            OneErrorValidator.Validate(code);

            return value;
        }

        public int GetInt(string key)
        {
            int value;
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_val_int(_ptr, key8, out value);
            }

            OneErrorValidator.Validate(code);

            return value;
        }

        public int GetStringSize(string key)
        {
            int size;
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_val_string_size(_ptr, key8, out size);
            }

            OneErrorValidator.Validate(code);

            return size;
        }

        public string GetString(string key)
        {
            int size = GetStringSize(key);
            
            var result8 = new Utf8CharArray(size);
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_val_string(_ptr, key8, result8, size);
            }

            OneErrorValidator.Validate(code);

            result8.ReadPtr();
            var result = result8.ToString();
            result8.Dispose();

            return result;
        }

        public OneArray GetArray(string key)
        {
            var array = new OneArray();
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_val_array(_ptr, key8, array.Ptr);
            }

            OneErrorValidator.Validate(code);

            return array;
        }

        public OneObject GetObject(string key)
        {
            var obj = new OneObject();
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_val_object(_ptr, key8, obj.Ptr);
            }

            OneErrorValidator.Validate(code);

            return obj;
        }

        public void SetBool(string key, bool value)
        {
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_set_val_bool(_ptr, key8, value);
            }

            OneErrorValidator.Validate(code);
        }

        public void SetInt(string key, int value)
        {
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_set_val_int(_ptr, key8, value);
            }

            OneErrorValidator.Validate(code);
        }

        public void SetString(string key, string value)
        {
            int code;

            using (var key8 = new Utf8CharArray(key))
            using (var value8 = new Utf8CharArray(value))
            {
                code = one_object_set_val_string(_ptr, key8, value8);
            }

            OneErrorValidator.Validate(code);
        }

        public void SetArray(string key, OneArray value)
        {
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_set_val_array(_ptr, key8, value.Ptr);
            }

            OneErrorValidator.Validate(code);
        }

        public void SetObject(string key, OneObject value)
        {
            int code;

            using (var key8 = new Utf8CharArray(key))
            {
                code = one_object_set_val_object(_ptr, key8, value._ptr);
            }

            OneErrorValidator.Validate(code);
        }

        public void Dispose()
        {
            one_object_destroy(_ptr);
        }
    }
}