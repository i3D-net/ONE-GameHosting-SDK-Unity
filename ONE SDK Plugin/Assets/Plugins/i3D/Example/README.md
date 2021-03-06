# Integration #

This example shows how to use the ONE SDK in a Unity project: how to create a server and communicate with an Arcus Client.

It consists of:
- OneIntegrationExample scene (OneIntegrationExample.unity file)
- OneIntegrationExample C# MonoBehaviour Component (OneIntegrationExample.cs file)

## OneIntegrationExample scene ##

There are two Game Objects in the scene that need attention.

1. _Server_

    A OneServer Component is attached to this Game Object. This is the main object of the SDK. It communicates with an Arcus Client.

    The Component has the following settings:
    - _Port_ - The port to bind to and listen on for incoming Client connections.
    - _Minimum Log Level_ - If set to Info, the Component logs both info and error messages. If set to Error, only errors are logged.
    - _Enable Logs_ - Enables or disables logging ONE Server messages to console.

2. _Example_

    This Game Object has a __OneIntegrationExample__ Component and runs the test sequence of communication with an Arcus Client.

## OneIntegrationExample Component ##

___Start___ triggers the _Simulation_ routine.

___Simulation___ routine imitates server behavior.

___OnEnable___ and ___OnDisable___ handle the subscription/unsubscription logic to/from the message-received events.

___OnServerSoftStopReceived___, ___OnServerAllocatedReceived___, ___OnServerMetadataReceived___, ___OnServerHostInformationReceived___, ___OnServerApplicationInstanceInformationReceived___
are the handles of the message-received events.

___SendLiveState___ is a helper method that wraps sending of the Live State update and logging of this action.

___BecomeAllocatedAfter___ routine to wait and send the Allocated status update.
