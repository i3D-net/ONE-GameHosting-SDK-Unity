# Integration #

This example shows how to use the ONE Client SDK in a Unity project: how to create a Pingers and a SitesGetter to get information about i3D sites.

It consists of:
- I3dIntegrationExample scene (I3dIntegrationExample.unity file)
- I3dIntegrationExample C# MonoBehaviour Component (I3dIntegrationExample.cs file)

## I3d IntegrationExample scene ##

There are two Game Objects in the scene that need attention.

1. _I3dIntegrationExample_

    A I3dIntegrationExample is a Game Object that has the pingers. This is an object of the SDK that pings the different i3D sites.

2. _SitesGetter_

    A SitesGetter Component is attached to this Game Object and used by the I3dIntegrationExample Game Object. This is an object of the SDK that gets the different i3D sites information.

## OneIntegrationExample Component ##

___Update___ routine uses fetch i3D sites information and then pings them to get update to date ping statistics.
