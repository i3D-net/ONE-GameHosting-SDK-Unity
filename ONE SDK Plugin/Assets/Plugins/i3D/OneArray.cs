﻿using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class OneArray : IDisposable
    {
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

        internal IntPtr Ptr { get { return _ptr; } }

        private readonly IntPtr _ptr;

        public OneArray()
        {
            IntPtr ptr;

            int code = one_array_create(out ptr);

            OneErrorValidator.Validate(code);

            _ptr = Marshal.ReadIntPtr(ptr);
        }

        public OneArray(int size) : this()
        {
            Reserve(size);
        }

        internal OneArray(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentNullException("ptr");

            _ptr = ptr;
        }

        public static void Copy(OneArray source, OneArray destination)
        {
            int code = one_array_copy(source._ptr, destination._ptr);

            OneErrorValidator.Validate(code);
        }

        public void Clear()
        {
            int code = one_array_clear(_ptr);

            OneErrorValidator.Validate(code);
        }

        // TODO: rename to Resize?
        public void Reserve(int size)
        {
            int code = one_array_reserve(_ptr, size);

            OneErrorValidator.Validate(code);
        }

        public void PushBool(bool value)
        {
            int code = one_array_push_back_bool(_ptr, value);

            OneErrorValidator.Validate(code);
        }

        public void PushInt(int value)
        {
            int code = one_array_push_back_int(_ptr, value);

            OneErrorValidator.Validate(code);
        }

        public void PushString(string value)
        {
            int code = one_array_push_back_string(_ptr, value);

            OneErrorValidator.Validate(code);
        }

        public void PushArray(OneArray value)
        {
            int code = one_array_push_back_array(_ptr, value._ptr);

            OneErrorValidator.Validate(code);
        }

        public void PushObject(OneObject value)
        {
            int code = one_array_push_back_object(_ptr, value.Ptr);

            OneErrorValidator.Validate(code);
        }

        public void Pop()
        {
            int code = one_array_pop_back(_ptr);

            OneErrorValidator.Validate(code);
        }

        public bool IsBool(uint position)
        {
            bool result;
            int code = one_array_is_val_bool(_ptr, position, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsInt(uint position)
        {
            bool result;
            int code = one_array_is_val_int(_ptr, position, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsString(uint position)
        {
            bool result;
            int code = one_array_is_val_string(_ptr, position, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsArray(uint position)
        {
            bool result;
            int code = one_array_is_val_array(_ptr, position, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool IsObject(uint position)
        {
            bool result;
            int code = one_array_is_val_object(_ptr, position, out result);

            OneErrorValidator.Validate(code);

            return result;
        }

        public bool GetBool(uint position)
        {
            bool value;
            int code = one_array_val_bool(_ptr, position, out value);

            OneErrorValidator.Validate(code);

            return value;
        }

        public int GetInt(uint position)
        {
            int value;
            int code = one_array_val_int(_ptr, position, out value);

            OneErrorValidator.Validate(code);

            return value;
        }

        public int GetStringSize(uint position)
        {
            int size;

            int code = one_array_val_string_size(_ptr, position, out size);
            OneErrorValidator.Validate(code);

            return size;
        }

        public string GetString(uint position)
        {
            int size = GetStringSize(position);
            char[] chars = new char[size];

            int code = one_array_val_string(_ptr, position, ref chars, size);
            OneErrorValidator.Validate(code);

            return new string(chars);
        }

        public OneArray GetArray(uint position)
        {
            IntPtr ptr;
            int code = one_array_val_array(_ptr, position, out ptr);

            OneErrorValidator.Validate(code);

            return new OneArray(ptr);
        }

        public OneObject GetObject(uint position)
        {
            IntPtr ptr;
            int code = one_array_val_object(_ptr, position, out ptr);

            OneErrorValidator.Validate(code);

            return new OneObject(ptr);
        }

        public void SetBool(uint position, bool value)
        {
            int code = one_array_set_val_bool(_ptr, position, value);

            OneErrorValidator.Validate(code);
        }

        public void SetInt(uint position, int value)
        {
            int code = one_array_set_val_int(_ptr, position, value);

            OneErrorValidator.Validate(code);
        }

        public void SetString(uint position, string value)
        {
            int code = one_array_set_val_string(_ptr, position, value);

            OneErrorValidator.Validate(code);
        }

        public void SetArray(uint position, OneArray value)
        {
            int code = one_array_set_val_array(_ptr, position, value._ptr);

            OneErrorValidator.Validate(code);
        }

        public void SetObject(uint position, OneObject value)
        {
            int code = one_array_set_val_object(_ptr, position, value.Ptr);

            OneErrorValidator.Validate(code);
        }

        public void Dispose()
        {
            one_array_destroy(_ptr);
        }
    }
}