# i3D.net ONE Game Hosting SDK Unity Plug-in #

WIP

- Sample game (TODO: add link)

## SDK native libraries ##

The libraries are build using the [SDK source code](https://github.com/i3D-net/ONE-GameHosting-SDK).

_build_dlls.bat_ can be used to build all necessary native assemblies on Windows (located in the _Tools_ directory). [Docker](https://docs.docker.com/docker-for-windows/install/) needs to be installed.

## Package export ##

All the Unity packages are created using Unity Editor of the lowest version the package should be compatible with. 

In order to create a new package, the following actions need to be performed:
1. Close the project in the Editor (if opened). This is needed to unload the SDK libraries.
2. Update the native SDK libraries. The easiest way is to use _build_dlls.bat_ script. Alternatively, they can be rebuilt manually by following the instructions in the [SDK repository](https://github.com/i3D-net/ONE-GameHosting-SDK).
3. Open the project.
4. Click "Assets -> Export Package..." menu item.
5. Make sure to select only "Plugins / i3D" folder and all its contents. All the other folders, if there are any, should be unselected.
6. Click "Export..." and select the output path.