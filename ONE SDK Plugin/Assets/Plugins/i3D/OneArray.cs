using System;

namespace i3D
{
    /// <summary>
    /// Represents the ONE Array structure that can be used in a ONE protocol message.
    /// </summary>
    public partial class OneArray : IDisposable
    {
        /// <summary>
        /// Returns the number of elements pushed to the array.
        /// </summary>
        public int Size
        {
            get
            {
                int size;
                int code = one_array_size(_ptr, out size);

                OneErrorValidator.Validate(code);

                return size;
            }
        }

        /// <summary>
        /// Returns the total size, allocated via Reserve, of the array.
        /// </summary>
        public int Capacity
        {
            get
            {
                int capacity;
                int code = one_array_capacity(_ptr, out capacity);

                OneErrorValidator.Validate(code);

                return capacity;
            }
        }

        /// <summary>
        /// Returns true if the array is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                bool empty;
                int code = one_array_is_empty(_ptr, out empty);

                OneErrorValidator.Validate(code);

                return empty;
            }
        }

        /// <summary>
        /// C pointer of the array. For internal use ONLY.
        /// </summary>
        internal IntPtr Ptr { get { return _ptr; } }

        private readonly IntPtr _ptr;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneArray"/>. Should be disposed.
        /// </summary>
        public OneArray()
        {
            int code = one_array_create(out _ptr);
            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneArray"/> and reserves space.
        /// Should be disposed.
        /// </summary>
        public OneArray(int size) : this()
        {
            Reserve(size);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneArray"/> for a specific C pointer.
        /// For internal use ONLY. Should NOT be disposed.
        /// </summary>
        /// <param name="ptr">The pointer.</param>
        /// <exception cref="ArgumentNullException"/>
        internal OneArray(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException("ptr");

            _ptr = ptr;
        }

        /// <summary>
        /// Makes a copy of the array.
        /// </summary>
        public static void Copy(OneArray source, OneArray destination)
        {
            int code = one_array_copy(source._ptr, destination._ptr);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Clears the array to an empty initialized state.
        /// </summary>
        public void Clear()
        {
            int code = one_array_clear(_ptr);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Reserves array space.
        /// </summary>
        public void Reserve(int size)
        {
            int code = one_array_reserve(_ptr, size);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Adds a <see cref="bool"/> element value to the back of the array. The array
        /// must have sufficient free space, that is the capacity must be greater than
        /// the size.
        /// </summary>
        public void PushBool(bool value)
        {
            int code = one_array_push_back_bool(_ptr, value);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Adds an <see cref="int"/> element value to the back of the array. The array
        /// must have sufficient free space, that is the capacity must be greater than
        /// the size.
        /// </summary>
        public void PushInt(int value)
        {
            int code = one_array_push_back_int(_ptr, value);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Adds a <see cref="string"/> element value to the back of the array. The array
        /// must have sufficient free space, that is the capacity must be greater than
        /// the size.
        /// </summary>
        public void PushString(string value)
        {
            int code;

            using (var value8 = new Utf8ByteArray(value))
            {
                code = one_array_push_back_string(_ptr, value8);
            }

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Adds a <see cref="OneArray"/> element value to the back of the array. The array
        /// must have sufficient free space, that is the capacity must be greater than
        /// the size.
        /// </summary>
        public void PushArray(OneArray value)
        {
            int code = one_array_push_back_array(_ptr, value._ptr);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Adds a <see cref="OneObject"/> element value to the back of the array. The array
        /// must have sufficient free space, that is the capacity must be greater than
        /// the size.
        /// </summary>
        public void PushObject(OneObject value)
        {
            int code = one_array_push_back_object(_ptr, value.Ptr);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Removes last element of the array, if any.
        /// </summary>
        public void Pop()
        {
            int code = one_array_pop_back(_ptr);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Checks whether the given key is <see cref="bool"/>.
        /// </summary>
        public bool IsBool(uint position)
        {
            bool result;
            int code = one_array_is_val_bool(_ptr, position, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        /// <summary>
        /// Checks whether the given key is <see cref="int"/>.
        /// </summary>
        public bool IsInt(uint position)
        {
            bool result;
            int code = one_array_is_val_int(_ptr, position, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        /// <summary>
        /// Checks whether the given key is <see cref="string"/>.
        /// </summary>
        public bool IsString(uint position)
        {
            bool result;
            int code = one_array_is_val_string(_ptr, position, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        /// <summary>
        /// Checks whether the given key is <see cref="OneArray"/>.
        /// </summary>
        public bool IsArray(uint position)
        {
            bool result;
            int code = one_array_is_val_array(_ptr, position, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        /// <summary>
        /// Checks whether the given key is <see cref="OneObject"/>.
        /// </summary>
        public bool IsObject(uint position)
        {
            bool result;
            int code = one_array_is_val_object(_ptr, position, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        /// <summary>
        /// Retrieves the <see cref="bool"/> value from the array.
        /// </summary>
        public bool GetBool(uint position)
        {
            bool value;
            int code = one_array_val_bool(_ptr, position, out value);

            OneErrorValidator.Validate(code);

            return value;
        }

        /// <summary>
        /// Retrieves the <see cref="int"/> value from the array.
        /// </summary>
        public int GetInt(uint position)
        {
            int value;
            int code = one_array_val_int(_ptr, position, out value);

            OneErrorValidator.Validate(code);

            return value;
        }

        /// <summary>
        /// Retrieves the <see cref="string"/> value from the array.
        /// </summary>
        public string GetString(uint position)
        {
            int size = GetStringSize(position);

            using (var result8 = new Utf8ByteArray(size))
            {
                int code = one_array_val_string(_ptr, position, result8, size);
                OneErrorValidator.Validate(code);

                result8.ReadPtr();
                return result8.ToString();
            }
        }

        /// <summary>
        /// Retrieves the <see cref="OneArray"/> value from the array. Should be disposed.
        /// </summary>
        public OneArray GetArray(uint position)
        {
            var array = new OneArray();
            int code = one_array_val_array(_ptr, position, array.Ptr);

            OneErrorValidator.Validate(code);

            return array;
        }

        /// <summary>
        /// Retrieves the <see cref="OneObject"/> value from the array. Should be disposed.
        /// </summary>
        public OneObject GetObject(uint position)
        {
            var obj = new OneObject();
            int code = one_array_val_object(_ptr, position, obj.Ptr);

            OneErrorValidator.Validate(code);

            return obj;
        }

        /// <summary>
        /// Sets a <see cref="bool"/> value into a position of the array.
        /// </summary>
        public void SetBool(uint position, bool value)
        {
            int code = one_array_set_val_bool(_ptr, position, value);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Sets an <see cref="int"/> value into a position of the array.
        /// </summary>
        public void SetInt(uint position, int value)
        {
            int code = one_array_set_val_int(_ptr, position, value);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Sets a <see cref="string"/> value into a position of the array.
        /// </summary>
        public void SetString(uint position, string value)
        {
            int code;

            using (var value8 = new Utf8ByteArray(value))
            {
                code = one_array_set_val_string(_ptr, position, value8);
            }

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Sets a <see cref="OneArray"/> value into a position of the array.
        /// </summary>
        public void SetArray(uint position, OneArray value)
        {
            int code = one_array_set_val_array(_ptr, position, value._ptr);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Sets a <see cref="OneObject"/> value into a position of the array.
        /// </summary>
        public void SetObject(uint position, OneObject value)
        {
            int code = one_array_set_val_object(_ptr, position, value.Ptr);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Releases the memory used by the array.
        /// </summary>
        public void Dispose()
        {
            one_array_destroy(_ptr);
        }

        private int GetStringSize(uint position)
        {
            int size;

            int code = one_array_val_string_size(_ptr, position, out size);
            OneErrorValidator.Validate(code);

            return size;
        }
    }
}