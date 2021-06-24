# i3D.net ONE Game Hosting SDK Unity Plugin

**Version: v0.9 (Beta)**
> All v1.0 features are complete and ready for integration and use. Customer iteration will determine any final changes before labelling as v1.0.
---

## Overview

The Plugin provides multiple components to interact with the i3D ONE Platform.
1. [Arcus](#arcus-component) is the library that provides communication between the Game Server and the scaling environment in the One Platform. It is to be integrated into the Game Server. It contains a C/C++ implementation of the ONE platform's Arcus protocol and messages, and provides a TCP server to communicate with the ONE Platform.
2. [Ping](#ping-component) is a library that provides a way to obtain server addresses of the ONE backend and utilities to ping the servers, for use in a game's player client code's matchmaking features.

## <a name="arcus-component"></a> Arcus Component

The plugin provides Unity game servers with the ability to communicate over TCP with the i3D.net ONE Platform, for easy and efficient scaling of game servers.

- [Integration guide](#arcus-integration-guide) - How to integrate the plugin into a game server.
- [Sample game](/ONE%20SDK%20Plugin/Assets/Plugins/i3D/Example) - An example of how to use the plugin.
- [Package export](#package-export) - How to create a new package.

The i3D.net Game Hosting SDK works on Windows and Linux.
If something doesn’t work, please [file an issue](https://github.com/i3D-net/ONE-GameHosting-SDK-Unity/issues).

The documentation for the entire ONE Platform can be found [here](https://www.i3d.net/docs/one/).

Supported platforms:
    - Windows 10 Pro
    - Ubuntu 18.04

## <a name="arcus-requirements"></a> Requirements

1. Minimum compatible Unity version: 2017.4.40f1.
2. Native SDK libraries require C++ Redistributable 2017 to be installed on Windows. Download the installer and follow its instructions:
    - [x64](https://go.microsoft.com/fwlink/?LinkId=746572)
    - [x86](https://go.microsoft.com/fwlink/?LinkId=746571)
3. [Docker](https://docs.docker.com/docker-for-windows/install/) (Optional for developers building C++ Shared Libraries).

## <a name="arcus-integration-guide"></a> Integration guide

1. Clone this repository or simply download [the Unity package](/ONE-GameHosting-SDK_v0.9.3.unitypackage) from the online repository.
2. In Unity Editor, select _Assets > Import Package > Custom Package..._ menu.
3. Select the package from step 1 and click _Open_.
4. In the opened window, click _Import_.
5. Add [OneServer](/ONE%20SDK%20Plugin/Assets/Plugins/i3D/OneServer.cs) component to a scene that is loaded before online gameplay starts.
    - The component is marked as _DontDestroyOnLoad_, so the object it's attached to will persist on scene transitions, maintaining the Arcus Server from that point forward.
    - For game servers that host multiple gaming sessions on a single process, each session should have its own `OneServer`.
6. Refer to [the example game](/ONE%20SDK%20Plugin/Assets/Plugins/i3D/Example) on how to set up and use the component.
7. [Test](#arcus-how-to-test).

## <a name="arcus-how-to-test"></a> How to test

There are two ways to test a Game Server that is running an Arcus Server:

1. The SDK contains a Fake Agent that can connect and simulate a real deployment. Build and run instructions can be found [here](https://github.com/i3D-net/ONE-GameHosting-SDK/tree/master/one/agent).
2. The Game Server can be uploaded to a live One Development Platform Deployment. See [here](https://www.i3d.net/docs/one/).

> Testing can be performed either in Unity Editor or on a build running in headless mode.

## <a name="ping-component"></a> Ping Component

The plugin provides Unity game servers with the ability ping and gather ping statistics for `i3D` ONE Platform servers.

- [Integration guide](#ping-integration-guide) - How to integrate the plugin into a game server.
- [Sample game](/ONE%20Client%20SDK%20Plugin/Assets/Plugins/i3D/Example) - An example of how to use the plugin.
- [Package export](#package-export) - How to create a new package.

The i3D.net Game Hosting SDK works on Windows and Linux.
If something doesn’t work, please [file an issue](https://github.com/i3D-net/ONE-GameHosting-SDK-Unity/issues).

The documentation for the entire ONE Platform can be found [here](https://www.i3d.net/docs/one/).

Supported platforms:
    - Windows 10 Pro
    - Ubuntu 18.04

## <a name="ping-requirements"></a> Requirements

1. Minimum compatible Unity version: 2017.4.40f1.
2. Native SDK libraries require C++ Redistributable 2017 to be installed on Windows. Download the installer and follow its instructions:
    - [x64](https://go.microsoft.com/fwlink/?LinkId=746572)
    - [x86](https://go.microsoft.com/fwlink/?LinkId=746571)
3. [Docker](https://docs.docker.com/docker-for-windows/install/) (Optional for developers building C++ Shared Libraries).

## <a name="ping-integration-guide"></a> Integration guide

1. Clone this repository or simply download [the Unity package](/ONE-Client-GameHosting-SDK_v0.9.3.unitypackage) from the online repository.
2. In Unity Editor, select _Assets > Import Package > Custom Package..._ menu.
3. Select the package from step 1 and click _Open_.
4. In the opened window, click _Import_.
5. Refer to [the example ping](/ONE%20Client%20SDK%20Plugin/Assets/Plugins/i3D/Example) on how to set up and use the component.
6. [Test](#ping-how-to-test).

## <a name="ping-how-to-test"></a> How to test

The exmple ping can be used standalone to test. The example ping will fetch the `i3D` sites information and start pinging the different sites.

> Testing can be performed in Unity Editor.

# SDK native libraries

Optional - for developers that need to recompile the C++ source used by the plugin.

The libraries are built using the [SDK source code](https://github.com/i3D-net/ONE-GameHosting-SDK).

[_build_sdk_dlls.bat_](/Tools/build_sdk_dlls.bat) can be used to build all necessary native assemblies on Windows. Docker must be installed (see [Requirements](#requirements)).
[_build_client_sdk_dlls.bat_](/Tools/build_client_sdk_dlls.bat) can be used to build all necessary native assemblies on Windows. Docker must be installed (see [Requirements](#requirements)).

For example:
```bat
build_sdk_dlls.bat C:\path_to_sdk
build_client_sdk_dlls.bat C:\path_to_sdk
```

# <a name="package-export"></a> Package export #

Optional - for developers that need to rebuild and repackage the plugin.

Consider potential Unity Version conflicts - ensure the package is built with the lowest Unity Editor Version that your users will be using.

Steps:
1. Close [the project](/ONE%20SDK%20Plugin) in the Editor (if opened). This is needed to unload the SDK libraries.
2. Update the native SDK libraries if needed. The easiest way is to use _build_dlls.bat_ script. Alternatively, they can be rebuilt manually by following the instructions in the [SDK repository](https://github.com/i3D-net/ONE-GameHosting-SDK).
3. Open [the project](/ONE%20SDK%20Plugin). Upgrade the project to match required Unity version, if needed.
4. Click "Assets -> Export Package..." menu item.
5. Make sure to select the "i3D" subfolder from the "Plugins" folder. All the other folders, if there are any, should be unselected.
6. Click "Export..." and select the output path.
