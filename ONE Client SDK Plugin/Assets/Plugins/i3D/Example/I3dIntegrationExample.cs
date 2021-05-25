using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace i3D.Example
{
    /// <summary>
    /// Show typical usage of a I3dSitesGetterWrapper and I3dPingersWrapper to get ping information about i3D sites.
    /// </summary>
    public class I3dIntegrationExample : MonoBehaviour
    {
        private I3dIpList _ipList;

        private I3dPingersWrapper _pingers;

        [SerializeField]
        private I3dSitesGetter sitesGetter;

        private void Update()
        {
            I3dPingersStatus status = _pingers.Status;
            
            LogWithTimestamp(string.Format("Pingers status: {0}", status));

            // When the pinger is initialized it is ready to ping the sites.
            if (status == I3dPingersStatus.I3dPingersStatusInitialized) {
                LogWithTimestamp("Updating pingers");
                _pingers.Update();

                if (_pingers.AtLeastOneSiteHasBeenPinged()) {
                    LogWithTimestamp("pingers at least one pinged");
                }

                if (_pingers.AllSitesHaveBeenPigned()) {
                    LogWithTimestamp("pingers all pinged");

                    uint size = _pingers.Size();
                    int lastTime = 0;
                    double averageTime = 0.0;
                    int minTime = 0;
                    int maxTime = 0;
                    double medianTime = 0.0;
                    uint pingResponseCount = 0;

                    for (uint i = 0; i < size; ++i) {
                        _pingers.Statistics(i, out lastTime, out averageTime, out minTime, out maxTime, out medianTime, out pingResponseCount);
                        var hostname = sitesGetter.Wrapper.GetHostname(i);
                        LogWithTimestamp(string.Format("Site statistics: {0}, {1}, {2}, {3}, {4}, {5}, {6}", hostname, lastTime, averageTime, minTime, maxTime, medianTime, pingResponseCount));
                    }

                    LogWithTimestamp("------------------");
                }

                return;
            }

            // When the SitesGetter is Ready it has fetch the sites information
            // and the Pingers can be initialized.
            if (sitesGetter.Status == I3dSitesGetterStatus.I3dSitesGetterReady) {
                _ipList.Clear();
                sitesGetter.ipv4List(_ipList);
                _pingers.Init(_ipList);
                LogWithTimestamp("Pingers initialized");
                return;
            }

            // Until the SitesGetter is ready it needs to be updated. It'll restart
            // the HTTP Get query if it has failed.
            sitesGetter.Update();
        }

        private void OnEnable()
        {
            _ipList = new I3dIpList();
            _pingers = new I3dPingersWrapper();
        }

        private void OnDisable()
        {
            _ipList.Dispose();
            _pingers.Dispose();
        }

        /// <summary>
        /// Logs a string prefixed with a UTC timestamp.
        /// </summary>
        private static void LogWithTimestamp(string log)
        {
            Debug.LogFormat("{0:yyyy-MM-ddTHH:mm:ssZ} {1}", DateTime.UtcNow, log);
        }
    }
}