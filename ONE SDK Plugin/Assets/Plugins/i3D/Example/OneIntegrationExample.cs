using System;
using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace i3D.Example
{
    public class OneIntegrationExample : MonoBehaviour
    {
        private OneServer _server;

        private void Awake()
        {
            _server = FindObjectOfType<OneServer>();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => _server.Status == OneServerStatus.OneServerStatusReady);

            _server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerStarting);

            yield return new WaitForSeconds(0.5f);

            _server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerOnline);

            _server.SetLiveState(1,
                                5,
                                "Example",
                                "Example map",
                                "Example mode",
                                "v0.1",
                                null);

            yield return new WaitForSeconds(2);

            _server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerOnline);
        }

        private void OnEnable()
        {
            _server.SoftStopReceived += OnServerSoftStopReceived;
            _server.AllocatedReceived += OnServerAllocatedReceived;
            _server.MetadataReceived += OnServerMetadataReceived;
            _server.HostInformationReceived += OnServerHostInformationReceived;
            _server.ApplicationInstanceInformationReceived += OnServerApplicationInstanceInformationReceived;
        }

        private void OnDisable()
        {
            _server.SoftStopReceived -= OnServerSoftStopReceived;
            _server.AllocatedReceived -= OnServerAllocatedReceived;
            _server.MetadataReceived -= OnServerMetadataReceived;
            _server.HostInformationReceived -= OnServerHostInformationReceived;
            _server.ApplicationInstanceInformationReceived -= OnServerApplicationInstanceInformationReceived;
        }

        private static void OnServerSoftStopReceived(int timeout)
        {
            Debug.LogFormat("Received <b>Soft Stop</b> packet with timeout {0}. Shutting down.", timeout);
            
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private static void OnServerAllocatedReceived(OneArray metadata)
        {
            Debug.LogFormat("Received <b>Allocated</b> packet with metadata:\n{0} : {1}\n{2} : {3}",
                            metadata.GetObject(0).GetString("key"),
                            metadata.GetObject(0).GetString("value"),
                            metadata.GetObject(1).GetString("key"),
                            metadata.GetObject(1).GetString("value"));
        }

        private static void OnServerMetadataReceived(OneArray metadata)
        {
            throw new NotImplementedException();
        }

        private static void OnServerHostInformationReceived(OneObject payload)
        {
             Debug.Log("Received <b>Host Information</b> packet");
        }

        private static void OnServerApplicationInstanceInformationReceived(OneObject payload)
        {
            Debug.Log("Received <b>Application Instance Information</b> packet");
        }
    }
}