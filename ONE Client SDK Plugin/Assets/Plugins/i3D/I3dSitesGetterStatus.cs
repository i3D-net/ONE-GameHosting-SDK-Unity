namespace i3D
{
    /// <summary>
    /// Status of a i3D SitesGetter.
    /// </summary>
    public enum I3dSitesGetterStatus
    {
        I3dSitesGetterUninitialized = 0,
        I3dSitesGetterInitialized = 1,
        I3dSitesGetterWaiting = 2,
        I3dSitesGetterError = 3,
        I3dSitesGetterReady = 4
    }
}