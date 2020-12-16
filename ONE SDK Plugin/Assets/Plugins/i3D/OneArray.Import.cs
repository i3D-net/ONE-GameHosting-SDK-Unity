using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class OneArray
    {
        private const string DllName = "one_arcus.dll";

        /// <summary>
        /// Creates a new array. Must be freed with one_array_destroy. Thread-safe.
        /// </summary>
        /// <param name="array">A null pointer to a OneArrayPtr, to be set with new array.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_create(out IntPtr array);

        /// <summary>
        /// Destroys array.
        /// </summary>
        /// <param name="array">A non-null OneArrayPtr, to be deleted.</param>
        [DllImport(DllName)]
        private static extern void one_array_destroy(IntPtr array);

        /// <summary>
        /// Makes a copy of the array. The destination must have been created via one_array_create.
        /// </summary>
        /// <param name="source">Source array.</param>
        /// <param name="destination">Destination array.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_copy(IntPtr source, IntPtr destination);

        /// <summary>
        /// Clears the array to an empty initialized state.
        /// </summary>
        /// <param name="array">A non-null OneArrayPtr.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_clear(IntPtr array);

        /// <summary>
        /// Reserves array space.
        /// </summary>
        /// <param name="array">A non-null OneArrayPtr.</param>
        /// <param name="size">Number of total elements the array should contain.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_reserve(IntPtr array, int size);

        /// <summary>
        /// Sets the given value to true if the array is empty.
        /// </summary>
        /// <param name="array">A non-null OneArrayPtr.</param>
        /// <param name="empty">A non-null bool pointer to set the result on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_is_empty(IntPtr array, out bool empty);

        /// <summary>
        /// Returns the number of elements pushed to the array.
        /// </summary>
        /// <param name="array">A non-null OneArrayPtr.</param>
        /// <param name="size">A non-null int pointer to set the result on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_size(IntPtr array, out int size);

        /// <summary>
        /// Returns the total size, allocated via one_array_reserve, of the array.
        /// </summary>
        /// <param name="array">A non-null OneArrayPtr.</param>
        /// <param name="capacity">A non-null int pointer to set the result on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_capacity(IntPtr array, out int capacity);

        /// <summary>
        /// Adds a <see cref="bool"/> element value to the back of the array. The array
        /// must have sufficient free space, that is the capacity must be greater than
        /// the size.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <param name="val">The value to add as a new element.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_push_back_bool(IntPtr array, bool val);

        /// <summary>
        /// Adds an <see cref="int"/> element value to the back of the array. The array
        /// must have sufficient free space, that is the capacity must be greater than
        /// the size.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <param name="val">The value to add as a new element.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_push_back_int(IntPtr array, int val);

        /// <summary>
        /// Adds a <see cref="string"/> element value to the back of the array. The array
        /// must have sufficient free space, that is the capacity must be greater than
        /// the size.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <param name="val">The value to add as a new element.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_push_back_string(IntPtr array, IntPtr val);

        /// <summary>
        /// Adds a <see cref="OneArray"/> element value to the back of the array. The array
        /// must have sufficient free space, that is the capacity must be greater than
        /// the size.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <param name="val">The value to add as a new element.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_push_back_array(IntPtr array, IntPtr val);

        /// <summary>
        /// Adds a <see cref="OneObject"/> element value to the back of the array. The array
        /// must have sufficient free space, that is the capacity must be greater than
        /// the size.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <param name="val">The value to add as a new element.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_push_back_object(IntPtr array, IntPtr val);

        /// <summary>
        /// Removes last element of the array, if any.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_pop_back(IntPtr array);

        /// <summary>
        /// Checks whether the given key is <see cref="bool"/>.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <param name="pos">The element index to inspect.</param>
        /// <param name="result">A non-null bool pointer to set the result to.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_is_val_bool(IntPtr array, uint pos, out bool result);

        /// <summary>
        /// Checks whether the given key is <see cref="int"/>.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <param name="pos">The element index to inspect.</param>
        /// <param name="result">A non-null bool pointer to set the result to.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_is_val_int(IntPtr array, uint pos, out bool result);

        /// <summary>
        /// Checks whether the given key is <see cref="string"/>.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <param name="pos">The element index to inspect.</param>
        /// <param name="result">A non-null bool pointer to set the result to.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_is_val_string(IntPtr array, uint pos, out bool result);

        /// <summary>
        /// Checks whether the given key is <see cref="OneArray"/>.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <param name="pos">The element index to inspect.</param>
        /// <param name="result">A non-null bool pointer to set the result to.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_is_val_array(IntPtr array, uint pos, out bool result);

        /// <summary>
        /// Checks whether the given key is <see cref="OneObject"/>.
        /// </summary>
        /// <param name="array">A pointer that will be set to point to the new OneArrayPtr.</param>
        /// <param name="pos">The element index to inspect.</param>
        /// <param name="result">A non-null bool pointer to set the result to.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_is_val_object(IntPtr array, uint pos, out bool result);

        /// <summary>
        /// Retrieves and sets the <see cref="bool"/> value from the array. The given value
        /// pointer must be non-null and will have the return value set on it.
        /// </summary>
        /// <param name="array">A valid array created via one_array_create.</param>
        /// <param name="pos">The index of the value to retrieve. Must be less than one_array_size.</param>
        /// <param name="val">A non-null pointer to set the value on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_val_bool(IntPtr array, uint pos, out bool val);

        /// <summary>
        /// Retrieves and sets the value <see cref="int"/> from the array. The given value
        /// pointer must be non-null and will have the return value set on it.
        /// </summary>
        /// <param name="array">A valid array created via one_array_create.</param>
        /// <param name="pos">The index of the value to retrieve. Must be less than one_array_size.</param>
        /// <param name="val">A non-null pointer to set the value on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_val_int(IntPtr array, uint pos, out int val);

        /// <summary>
        /// Returns the number of characters in the string. This does not include a trailing null character.
        /// </summary>
        /// <param name="array">A valid array created via one_array_create.</param>
        /// <param name="pos">The index of the value to retrieve. Must be less than one_array_size.</param>
        /// <param name="size">A non-null int pointer to set the size on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_val_string_size(IntPtr array, uint pos, out int size);

        /// <summary>
        /// Writes the key value to the given character buffer.
        /// </summary>
        /// <param name="array">A valid array created via one_array_create.</param>
        /// <param name="pos">The index of the value to retrieve. Must be less than one_array_size.</param>
        /// <param name="val">A non-null pointer to set the value on.</param>
        /// <param name="size">Size of the value buffer that can be written to.
        /// Must be equal to size obtained via one_array_val_string_size.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_val_string(IntPtr array, uint pos, IntPtr val, int size);

        /// <summary>
        /// Writes the key value to the given character buffer.
        /// </summary>
        /// <param name="array">A valid array created via one_array_create.</param>
        /// <param name="pos">The index of the value to retrieve. Must be less than one_array_size.</param>
        /// <param name="val">A non-null pointer to set the value on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_val_array(IntPtr array, uint pos, out IntPtr val);

        /// <summary>
        /// Writes the key value to the given character buffer.
        /// </summary>
        /// <param name="array">A valid array created via one_array_create.</param>
        /// <param name="pos">The index of the value to retrieve. Must be less than one_array_size.</param>
        /// <param name="val">A non-null pointer to set the value on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_val_object(IntPtr array, uint pos, out IntPtr val);

        /// <summary>
        /// Allows setting a <see cref="bool"/> sub key/value pair on the array.
        /// </summary>
        /// <param name="array">A valid object created via one_array_create.</param>
        /// <param name="pos">The position in the array to set the value in. Must be less than one_array_size.</param>
        /// <param name="val">The value to set.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_set_val_bool(IntPtr array, uint pos, bool val);

        /// <summary>
        /// Allows setting a <see cref="int"/> sub key/value pair on the array.
        /// </summary>
        /// <param name="array">A valid object created via one_array_create.</param>
        /// <param name="pos">The position in the array to set the value in. Must be less than one_array_size.</param>
        /// <param name="val">The value to set.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_set_val_int(IntPtr array, uint pos, int val);

        /// <summary>
        /// Allows setting a <see cref="string"/> sub key/value pair on the array.
        /// </summary>
        /// <param name="array">A valid object created via one_array_create.</param>
        /// <param name="pos">The position in the array to set the value in. Must be less than one_array_size.</param>
        /// <param name="val">The value to set.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_set_val_string(IntPtr array, uint pos, IntPtr val);

        /// <summary>
        /// Allows setting a <see cref="OneArray"/> sub key/value pair on the array.
        /// </summary>
        /// <param name="array">A valid object created via one_array_create.</param>
        /// <param name="pos">The position in the array to set the value in. Must be less than one_array_size.</param>
        /// <param name="val">The value to set.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_set_val_array(IntPtr array, uint pos, IntPtr val);

        /// <summary>
        /// Allows setting a <see cref="OneObject"/> sub key/value pair on the array.
        /// </summary>
        /// <param name="array">A valid object created via one_array_create.</param>
        /// <param name="pos">The position in the array to set the value in. Must be less than one_array_size.</param>
        /// <param name="val">The value to set.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_array_set_val_object(IntPtr array, uint pos, IntPtr val);
    }
}