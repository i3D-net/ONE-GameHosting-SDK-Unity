namespace i3D
{
    /// <summary>
    /// Status of a game server. As defined in:
    /// https://www.i3d.net/docs/one/odp/Game-Integration/Management-Protocol/Arcus-V2/request-response/#applicationinstance-set-status-request
    /// </summary>
    public enum OneApplicationInstanceStatus
    {
        OneServerStarting = 3,
        OneServerOnline = 4,
        OneServerAllocated = 5
    }
}