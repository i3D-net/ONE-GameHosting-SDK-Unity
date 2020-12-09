using System;
using System.Runtime.InteropServices;

namespace i3D
{
    public partial class OneObject
    {
        private const string DllName = "clib.dll";

        /// <summary>
        /// Create a new object that can be used as a key value in a One protocol message.
        /// one_object_destroy must be called to free the object. Thread-safe.
        /// </summary>
        /// <param name="obj">A pointer that will be set to point to the new OneObjectPtr.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName)]
        private static extern int one_object_create(out IntPtr obj);

        /// <summary>
        /// Must be called to free an object created by one_object_create.
        /// </summary>
        /// <param name="obj">A non-null object pointer created via one_object_create.</param>
        [DllImport(DllName)]
        private static extern void one_object_destroy(IntPtr obj);

        /// <summary>
        /// Checks whether the given key is <see cref="bool"/>.
        /// </summary>
        /// <param name="obj">A pointer that will be set to point to the new OneObjectPtr.</param>
        /// <param name="key">The key to lookup.</param>
        /// <param name="result">A non-null bool pointer to set the result to.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_is_val_bool(IntPtr obj, string key, out bool result);

        /// <summary>
        /// Checks whether the given key is <see cref="int"/>.
        /// </summary>
        /// <param name="obj">A pointer that will be set to point to the new OneObjectPtr.</param>
        /// <param name="key">The key to lookup.</param>
        /// <param name="result">A non-null bool pointer to set the result to.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_is_val_int(IntPtr obj, string key, out bool result);

        /// <summary>
        /// Checks whether the given key is <see cref="string"/>.
        /// </summary>
        /// <param name="obj">A pointer that will be set to point to the new OneObjectPtr.</param>
        /// <param name="key">The key to lookup.</param>
        /// <param name="result">A non-null bool pointer to set the result to.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_is_val_string(IntPtr obj, string key, out bool result);

        /// <summary>
        /// Checks whether the given key is <see cref="OneObject"/>.
        /// </summary>
        /// <param name="obj">A pointer that will be set to point to the new OneObjectPtr.</param>
        /// <param name="key">The key to lookup.</param>
        /// <param name="result">A non-null bool pointer to set the result to.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_is_val_array(IntPtr obj, string key, out bool result);

        /// <summary>
        /// Checks whether the given key is <see cref="OneArray"/>.
        /// </summary>
        /// <param name="obj">A pointer that will be set to point to the new OneObjectPtr.</param>
        /// <param name="key">The key to lookup.</param>
        /// <param name="result">A non-null bool pointer to set the result to.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_is_val_object(IntPtr obj, string key, out bool result);

        /// <summary>
        /// Retrieves and sets the <see cref="bool"/> value from the object. The given value
        /// pointer must be non-null and will have the return value set on it.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="val">Non-null pointer to set the value on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_val_bool(IntPtr obj, string key, out bool val);

        /// <summary>
        /// Retrieves and sets the <see cref="int"/> value from the object. The given value
        /// pointer must be non-null and will have the return value set on it.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="val">Non-null pointer to set the value on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_val_int(IntPtr obj, string key, out int val);

        /// <summary>
        /// Returns the number of characters in the string. This does not include a trailing null character.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="size">A non-null int pointer to set the size on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_val_string_size(IntPtr obj, string key, out int size);

        /// <summary>
        /// Writes the key value to the given character buffer.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="val">Non-null pointer to set the value on.</param>
        /// <param name="size">Size of the value buffer that can be written to. Must be equal
        /// to size obtained via one_object_val_string_size.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_val_string(IntPtr obj, string key, char[] val, int size);

        /// <summary>
        /// Retrieves and sets the <see cref="OneArray"/> value from the object. The given value
        /// pointer must be non-null and will have the return value set on it.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="val">Non-null pointer to set the value on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_val_array(IntPtr obj, string key, out IntPtr val);

        /// <summary>
        /// Retrieves and sets the <see cref="OneObject"/> value from the object. The given value
        /// pointer must be non-null and will have the return value set on it.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="val">Non-null pointer to set the value on.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_val_object(IntPtr obj, string key, out IntPtr val);

        /// <summary>
        /// Allows setting a <see cref="bool"/> sub key/value pair on the object.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="val">The value to set.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_set_val_bool(IntPtr obj, string key, bool val);

        /// <summary>
        /// Allows setting an <see cref="int"/> sub key/value pair on the object.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="val">The value to set.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_set_val_int(IntPtr obj, string key, int val);

        /// <summary>
        /// Allows setting a <see cref="string"/> sub key/value pair on the object.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="val">The value to set.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_set_val_string(IntPtr obj, string key, string val);

        /// <summary>
        /// Allows setting a <see cref="OneArray"/> sub key/value pair on the object.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="val">The value to set.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_set_val_array(IntPtr obj, string key, IntPtr val);

        /// <summary>
        /// Allows setting a <see cref="OneObject"/> sub key/value pair on the object.
        /// </summary>
        /// <param name="obj">A valid object created via one_object_create.</param>
        /// <param name="key">The key of the value to return.</param>
        /// <param name="val">The value to set.</param>
        /// <returns>Error code.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        private static extern int one_object_set_val_object(IntPtr obj, string key, IntPtr val);
    }
}