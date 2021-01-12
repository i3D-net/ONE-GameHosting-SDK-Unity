namespace i3D
{
    /// <summary>
    /// Status of a ONE Arcus Server.
    /// </summary>
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