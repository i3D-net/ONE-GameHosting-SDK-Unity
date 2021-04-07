using System;

namespace i3D
{
    /// <summary>
    /// Represents the ONE Object structure that can be used as a key value in a ONE protocol message.
    /// </summary>
    public partial class OneObject : IDisposable
    {
        /// <summary>
        /// C pointer of the object. For internal use ONLY.
        /// </summary>
        internal IntPtr Ptr { get { return _ptr; } }

        private readonly IntPtr _ptr;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneObject"/>. Should be disposed.
        /// </summary>
        public OneObject()
        {
            int code = one_object_create(out _ptr);
            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneObject"/> for a specific C pointer.
        /// For internal use ONLY. Should NOT be disposed.
        /// </summary>
        /// <param name="ptr">The pointer.</param>
        /// <exception cref="ArgumentNullException"/>
        internal OneObject(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException("ptr");

            _ptr = ptr;
        }

        /// <summary>
        /// Checks whether the given key is <see cref="bool"/>.
        /// </summary>
        public bool IsBool(string key)
        {
            bool result;
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_is_val_bool(_ptr, key8, out result);
            }

            OneErrorValidator.Validate(code);

            return result;
        }

        /// <summary>
        /// Checks whether the given key is <see cref="int"/>.
        /// </summary>
        public bool IsInt(string key)
        {
            bool result;
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_is_val_int(_ptr, key8, out result);
            }

            OneErrorValidator.Validate(code);

            return result;
        }

        /// <summary>
        /// Checks whether the given key is <see cref="string"/>.
        /// </summary>
        public bool IsString(string key)
        {
            bool result;
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_is_val_string(_ptr, key8, out result);
            }

            OneErrorValidator.Validate(code);

            return result;
        }

        /// <summary>
        /// Checks whether the given key is <see cref="OneArray"/>.
        /// </summary>
        public bool IsArray(string key)
        {
            bool result;
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_is_val_array(_ptr, key8, out result);
            }

            OneErrorValidator.Validate(code);

            return result;
        }

        /// <summary>
        /// Checks whether the given key is <see cref="OneObject"/>.
        /// </summary>
        public bool IsObject(string key)
        {
            bool result;
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_is_val_object(_ptr, key8, out result);
            }

            OneErrorValidator.Validate(code);

            return result;
        }

        /// <summary>
        /// Retrieves the <see cref="bool"/> value from the object.
        /// </summary>
        public bool GetBool(string key)
        {
            bool value;
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_val_bool(_ptr, key8, out value);
            }

            OneErrorValidator.Validate(code);

            return value;
        }

        /// <summary>
        /// Retrieves the <see cref="int"/> value from the object.
        /// </summary>
        public int GetInt(string key)
        {
            int value;
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_val_int(_ptr, key8, out value);
            }

            OneErrorValidator.Validate(code);

            return value;
        }

        /// <summary>
        /// Retrieves the <see cref="string"/> value from the object.
        /// </summary>
        public string GetString(string key)
        {
            int size = GetStringSize(key);
            
            var result8 = new Utf8ByteArray(size);
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_val_string(_ptr, key8, result8, size);
            }

            OneErrorValidator.Validate(code);

            result8.ReadPtr();
            var result = result8.ToString();
            result8.Dispose();

            return result;
        }

        /// <summary>
        /// Retrieves the <see cref="OneArray"/> value from the object. Should be disposed.
        /// </summary>
        public OneArray GetArray(string key)
        {
            var array = new OneArray();
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_val_array(_ptr, key8, array.Ptr);
            }

            OneErrorValidator.Validate(code);

            return array;
        }

        /// <summary>
        /// Retrieves the <see cref="OneObject"/> value from the object. Should be disposed.
        /// </summary>
        public OneObject GetObject(string key)
        {
            var obj = new OneObject();
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_val_object(_ptr, key8, obj.Ptr);
            }

            OneErrorValidator.Validate(code);

            return obj;
        }

        /// <summary>
        /// Sets a <see cref="bool"/> key/value pair on the object.
        /// </summary>
        public void SetBool(string key, bool value)
        {
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_set_val_bool(_ptr, key8, value);
            }

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Sets an <see cref="int"/> key/value pair on the object.
        /// </summary>
        public void SetInt(string key, int value)
        {
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_set_val_int(_ptr, key8, value);
            }

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Sets a <see cref="string"/> key/value pair on the object.
        /// </summary>
        public void SetString(string key, string value)
        {
            int code;

            using (var key8 = new Utf8ByteArray(key))
            using (var value8 = new Utf8ByteArray(value))
            {
                code = one_object_set_val_string(_ptr, key8, value8);
            }

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Sets a <see cref="OneArray"/> key/value pair on the object.
        /// </summary>
        public void SetArray(string key, OneArray value)
        {
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_set_val_array(_ptr, key8, value.Ptr);
            }

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Sets a <see cref="OneObject"/> key/value pair on the object.
        /// </summary>
        public void SetObject(string key, OneObject value)
        {
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_set_val_object(_ptr, key8, value._ptr);
            }

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Releases the memory used by the object.
        /// </summary>
        public void Dispose()
        {
            one_object_destroy(_ptr);
        }

        private int GetStringSize(string key)
        {
            int size;
            int code;

            using (var key8 = new Utf8ByteArray(key))
            {
                code = one_object_val_string_size(_ptr, key8, out size);
            }

            OneErrorValidator.Validate(code);

            return size;
        }
    }
}