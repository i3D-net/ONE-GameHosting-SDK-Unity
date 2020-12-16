#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;

namespace i3D
{
    /// <summary>
    /// Helper class that adds the plugin folder to PATH so Unity Editor can find the DLL.
    /// </summary>
    [InitializeOnLoad]
    public class OneEditorDllHelper
    {
        static OneEditorDllHelper()
        {
            string currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
            string dllPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar +
                             "Assets" + Path.DirectorySeparatorChar +
                             "Plugins" + Path.DirectorySeparatorChar +
                             "i3D" + Path.DirectorySeparatorChar +
                             "Windows";

            if(currentPath == null || !currentPath.Contains(dllPath))
            {
                Environment.SetEnvironmentVariable("PATH",
                                                   (currentPath ?? "") +
                                                   Path.PathSeparator +
                                                   dllPath,
                                                   EnvironmentVariableTarget.Process);
            }
        }
    }
}
#endif