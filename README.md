# i3D.net ONE Game Hosting SDK Unity Plug-in #

**Version: v0.9 (Beta)**

> All v1.0 features are complete and ready for integration and use. Customer iteration will determine any final changes before labelling as v1.0.

---

## Overview ##

The plug-in provides Unity game servers with the ability to communicate over TCP with the i3D.net ONE Platform, for easy and efficient scaling of game servers.

- [Integration guide](#integration-guide) - How to integrate the plug-in into a game server.
- [Sample game](/ONE%20SDK%20Plugin/Assets/Plugins/i3D/Example) - An example of how to use the plug-in.
- [Package export](#package-export) - How to create a new package.

The i3D.net Game Hosting SDK works on Windows and Linux.
If something doesn’t work, please [file an issue](https://github.com/i3D-net/ONE-GameHosting-SDK/issues).

The documentation for the entire ONE Platform can be found [here](https://www.i3d.net/docs/one/).

Supported platforms:
    - Windows 10 Pro
    - Ubuntu 18.04
## <a name="requirements"></a> Requirements ##

1. Minimum compatible Unity version: 2017.4.40f1.
2. Native SDK libraries require C++ Redistributable 2017 to be installed on Windows. Download the installer and follow its instructions:
    - [x64](https://go.microsoft.com/fwlink/?LinkId=746572)
    - [x86](https://go.microsoft.com/fwlink/?LinkId=746571)
3. [Docker](https://docs.docker.com/docker-for-windows/install/) (Optional for developers building C++ Shared Libraries).

## <a name="integration-guide"></a> Integration guide ##

1. Clone this repository or simply download [the Unity package](/ONE-GameHosting-SDK_v0.9.unitypackage) from the online repository.
2. In Unity Editor, select _Assets > Import Package > Custom Package..._ menu.
3. Select the package from step 1 and click _Open_.
4. In the opened window, click _Import_.
5. Add [OneServer](/ONE%20SDK%20Plugin/Assets/Plugins/i3D/OneServer.cs) component to a scene that is loaded before online gameplay starts.
    - The component is marked as _DontDestroyOnLoad_, so the object it's attached to will persist on scene transitions, maintaining the Arcus Server from that point forward.
    - For game servers that host multiple gaming sessions on a single process, each session should have its own `OneServer`.
6. Refer to [the example game](/ONE%20SDK%20Plugin/Assets/Plugins/i3D/Example) on how to set up and use the component.
7. [Test](#how-to-test).

## <a name="how-to-test"></a> How to test ##

There are two ways to test a Game Server that is running an Arcus Server:

1. The SDK contains a Fake Agent that can connect and simulate a real deployment. Build and run instructions can be found [here](https://github.com/i3D-net/ONE-GameHosting-SDK/tree/master/one/agent).
2. The Game Server can be uploaded to a live One Development Platform Deployment. See [here](https://www.i3d.net/docs/one/).

> Testing can be performed either in Unity Editor or on a build running in headless mode.

## SDK native libraries ##

Optional - for developers that need to recompile the C++ source used by the plugin.

The libraries are built using the [SDK source code](https://github.com/i3D-net/ONE-GameHosting-SDK).

[_build_dlls.bat_](/Tools/build_dlls.bat) can be used to build all necessary native assemblies on Windows. Docker must be installed (see [Requirements](#requirements)).

For example:
```bat
build_dlls.bat C:\path_to_sdk
```

## <a name="package-export"></a> Package export ##

Optional - for developers that need to rebuild and repackage the plugin.

Consider potential Unity Version conflicts - ensure the package is built with the lowest Unity Editor Version that your users will be using.

Steps:
1. Close [the project](/ONE%20SDK%20Plugin) in the Editor (if opened). This is needed to unload the SDK libraries.
2. Update the native SDK libraries if needed. The easiest way is to use _build_dlls.bat_ script. Alternatively, they can be rebuilt manually by following the instructions in the [SDK repository](https://github.com/i3D-net/ONE-GameHosting-SDK).
3. Open [the project](/ONE%20SDK%20Plugin). Upgrade the project to match required Unity version, if needed.
4. Click "Assets -> Export Package..." menu item.
5. Make sure to select the "i3D" subfolder from the "Plugins" folder. All the other folders, if there are any, should be unselected.
6. Click "Export..." and select the output path.
