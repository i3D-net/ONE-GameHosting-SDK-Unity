# Intergation example #

This example shows how to use the ONE SDK in a Unity project: how to create a server and communicate with an Arcus Client.
It consists of:
- OneIntegrationExample scene (OneIntegrationExample.unity file) and
- OneIntegrationExample C# MonoBehaviour component (OneIntegrationExample.cs file)

## OneIntegrationExample scene ##

There are two objects on the scene that need attention.

1. _Server_

    OneServer component is attached to this game object. This is the main object of the SDK. It provides means of communication with an Arcus Client.

    The component has the following settings:
    - _Port_ - The port to bind to and listen on for incoming Client connections.
    - _Minimum Log Level_ - If set to Info, the component logs both info and error messages. If set to Error, only errors are logged.
    - _Enable Logs_ - Set whether ONE SDK info and error messages should be logged.

2. _Example_

    This game object has the __OneIntegrationExample__ component and runs the test sequence of communication with the Arcus Client (see [Fake Agent](https://github.com/i3D-net/ONE-GameHosting-SDK/tree/master/one/agent)).

## OneIntegrationExample component ##

___Start___ triggers the _Simulation_ routine.

___Simulation___ routine imitates server behavior.

___OnEnable___ and ___OnDisable___ handle the subscription/unsubscription logic to/from the message-received events.

___OnServerSoftStopReceived___, ___OnServerAllocatedReceived___, ___OnServerMetadataReceived___, ___OnServerHostInformationReceived___, ___OnServerApplicationInstanceInformationReceived___
are the handles of the message-received events.

___SendLiveState___ is a helper method that wraps sending of the Live State update and logging of this action.

___BecomeAllocatedAfter___ routine to wait and send the Allocated status update.
