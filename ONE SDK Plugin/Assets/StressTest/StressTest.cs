using i3D;
using System;
using System.Collections;
using UnityEngine;

public class StressTest : MonoBehaviour
{
    [SerializeField]
    private OneServer server;

    private OneServerStatus _lastStatus;

    private int _stateSentCount;
    private int _metadataReceivedCount;

    private void Awake()
    {
        ushort port = GetCommandLinePort();

        if (port != 0)
        {
            Debug.LogFormat("Setting port to {0}", port);
            server.SetPort(port);
        }

        server.Run();
    }

    private void Start()
    {
        StartCoroutine(Simulation());

        Debug.LogFormat("Server status: {0}", server.Status);
        _lastStatus = server.Status;
    }

    private void Update()
    {
        if (_lastStatus != server.Status)
        {
            Debug.LogFormat("Server status: {0}", server.Status);
            _lastStatus = server.Status;
        }
    }

    private static ushort GetCommandLinePort()
    {
        var args = Environment.GetCommandLineArgs();
        int portIndex = Array.FindIndex(args, s => string.Equals(s, "-p", StringComparison.Ordinal));

        if (portIndex == -1)
            return 0;

        if (portIndex >= args.Length - 1)
        {
            Debug.LogError("Cannot find the port value");
            return 0;
        }

        ushort port;

        if (!ushort.TryParse(args[portIndex + 1], out port))
        {
            Debug.LogErrorFormat("Couldn't parse port {0}", args[portIndex + 1]);
            return 0;
        }

        return port;
    }

    private IEnumerator Simulation()
    {
        yield return new WaitUntil(() => server.Status == OneServerStatus.OneServerStatusReady);

        server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerOnline);

        while (true)
        {
            server.SetLiveState(UnityEngine.Random.Range(0, 101), 100, "Test", "Test", "Test", "v0.1", null);

            if (++_stateSentCount % 10000 == 0)
                Debug.LogFormat("State sent messages: {0}", _stateSentCount);

            yield return null;
        }
    }

    private void OnEnable()
    {
        server.MetadataReceived += OnServerMetadataReceived;
    }

    private void OnDisable()
    {
        server.MetadataReceived -= OnServerMetadataReceived;
    }

    private void OnServerMetadataReceived(OneArray obj)
    {
        if (++_metadataReceivedCount % 10000 == 0)
            Debug.LogFormat("Received metadata messages: {0}", _metadataReceivedCount);
    }
}