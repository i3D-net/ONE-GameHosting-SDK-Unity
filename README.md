# i3D.net ONE Game Hosting SDK Unity Plug-in #

**Version: v0.9 (Beta)**

> All v1.0 features are complete and ready for integration and use. Customer iteration will determine any final changes before labelling as v1.0.

**Minimum compatible Unity version: 2017.4.40f1**

---

## Overview

The plug-in provides Unity game servers with the ability to communicate over TCP with the i3D.net ONE Platform, for easy and efficient scaling of game servers.

- [Integration guide](#integration-guide) - How to integrate the plug-in into a game server.
- [Sample game](/ONE%20SDK%20Plugin/Assets/Plugins/i3D/Example) - An example of how to use the plug-in.
- [Package export](#package-export) - How to create a new package.

The i3D.net Game Hosting SDK works on Windows and Linux.
If something doesn’t work, please [file an issue](https://github.com/i3D-net/ONE-GameHosting-SDK/issues).

The documentation for the entire ONE Platform can be found [here](https://www.i3d.net/docs/one/).

## <a name="integration-guide"></a> Integration guide ##

1. Download [the Unity package](/ONE-GameHosting-SDK_v0.9.unitypackage).
2. In Unity Editor, select _Assets > Import Package > Custom Package..._ menu.
3. Select the downloaded package and click _Open_.
4. In the opened window, click _Import_.
5. Add [OneServer](/ONE%20SDK%20Plugin/Assets/Plugins/i3D/OneServer.cs) component to the scene. The component is marked as _DontDestroyOnLoad_, so the object it's attached to will persist on scenes switch.
6. Refer to [the sample game](/ONE%20SDK%20Plugin/Assets/Plugins/i3D/Example) on how to set up and use the component.

## SDK native libraries ##

The libraries are build using the [SDK source code](https://github.com/i3D-net/ONE-GameHosting-SDK).

_build_dlls.bat_ can be used to build all necessary native assemblies on Windows (located in the _Tools_ directory). [Docker](https://docs.docker.com/docker-for-windows/install/) needs to be installed.

## <a name="package-export"></a> Package export ##

All the Unity packages are created using Unity Editor of the lowest version the package should be compatible with.

In order to create a new package, the following actions need to be performed:
1. Close [the project](/ONE%20SDK%20Plugin) in the Editor (if opened). This is needed to unload the SDK libraries.
2. Update the native SDK libraries. The easiest way is to use _build_dlls.bat_ script. Alternatively, they can be rebuilt manually by following the instructions in the [SDK repository](https://github.com/i3D-net/ONE-GameHosting-SDK).
3. Open [the project](/ONE%20SDK%20Plugin).
4. Click "Assets -> Export Package..." menu item.
5. Make sure to select only "Plugins / i3D" folder and all its contents. All the other folders, if there are any, should be unselected.
6. Click "Export..." and select the output path.