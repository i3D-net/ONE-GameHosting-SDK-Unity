namespace i3D
{
    public enum OneServerStatus
    {
        OneServerStatusUninitialized = 0,
        OneServerStatusInitialized,
        OneServerStatusWaitingForClient,
        OneServerStatusHandshake,
        OneServerStatusReady,
        OneServerStatusError
    }
}