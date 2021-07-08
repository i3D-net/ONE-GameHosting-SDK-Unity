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
            server.SendReverseMetadata("Example map", "Example mode", "example type");

            if (++_stateSentCount % 10000 == 0)
                Debug.LogFormat("State sent messages: {0}", _stateSentCount);

            yield return null;
        }
    }

    private void OnEnable()
    {
        server.MetadataReceived += OnServerMetadataReceived;
        server.CustomCommandReceived += OnCustomCommandReceived;
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
    private static void OnCustomCommandReceived(OneArray data)
    {
        // Extracting keys from the custom command is optional. These are example values sent from the Fake Agent.
        // A real game may or may not have custom key values, as needed.
        using (var obj0 = data.GetObject(0))
        using (var obj1 = data.GetObject(1))
        {
            Debug.LogFormat("Received <b>custom command</b> packet:\n" +
                            "(key : \"{0}\", value : \"{1}\")\n" +
                            "(key : \"{2}\", value : \"{3}\")\n",
                            obj0.GetString("key"),
                            obj0.GetString("value"),
                            obj1.GetString("key"),
                            obj1.GetString("value"));
        }
    }
}