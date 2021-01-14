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
        [SerializeField]
        private OneServer server;

        /// <summary>
        /// Holds the server status from the previous frame. For tracking the status change.
        /// </summary>
        private OneServerStatus _lastStatus;

        private void Awake()
        {
            ushort port = GetCommandLinePort();

            // Set the port passed from the command line.
            if (port != 0)
            {
                Debug.LogFormat("Setting port to {0}", port);
                server.SetPort(port);
            }

            // Start the server.
            server.Run();
        }

        private void Start()
        {
            // Start simulating server behavior.
            StartCoroutine(Simulation());

            Debug.LogFormat("Server status: {0}", server.Status);
            _lastStatus = server.Status;
        }

        private void Update()
        {
            // If the status has changed, log it and save the new one.
            if (_lastStatus != server.Status)
            {
                Debug.LogFormat("Server status: {0}", server.Status);
                _lastStatus = server.Status;
            }
        }

        /// <summary>
        /// Parse the command line arguments and return the port if it's specified.
        /// Example of usage:
        /// > Game.exe -p 19001
        /// </summary>
        private static ushort GetCommandLinePort()
        {
            // Get command line arguments and find if the port is specified.
            var args = Environment.GetCommandLineArgs();
            int portIndex = Array.FindIndex(args, s => string.Equals(s, "-p", StringComparison.Ordinal));

            // Stop if "-p" entry doesn't exist among the arguments.
            if (portIndex == -1)
                return 0;

            // Log error and stop if there's nothing specified after "-p".
            if (portIndex >= args.Length - 1)
            {
                Debug.LogError("Cannot find the port value");
                return 0;
            }

            ushort port;

            // Parse the port value after "-p". Log error and stop if the value cannot be parsed.
            if (!ushort.TryParse(args[portIndex + 1], out port))
            {
                Debug.LogErrorFormat("Couldn't parse port {0}", args[portIndex + 1]);
                return 0;
            }

            return port;
        }

        /// <summary>
        /// Routine to simulate server behavior.
        /// </summary>
        private IEnumerator Simulation()
        {
            // Wait for the agent to connect.
            yield return new WaitUntil(() => server.Status == OneServerStatus.OneServerStatusReady);

            // Simulate the server starting up by sending the OneServerStarting status.
            Debug.LogFormat("Sending <b>Application Instance Status</b>: {0} ({1})",
                            OneApplicationInstanceStatus.OneServerStarting,
                            (int) OneApplicationInstanceStatus.OneServerStarting);
            server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerStarting);

            // Simulate the startup taking 1 second.
            yield return new WaitForSeconds(1f);

            // Simulate the server started by sending OneServerOnline status.
            Debug.LogFormat("Sending <b>Application Instance Status</b>: {0} ({1})",
                            OneApplicationInstanceStatus.OneServerOnline,
                            (int) OneApplicationInstanceStatus.OneServerOnline);
            server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerOnline);

            // Send the test live state data to the agent.
            SendLiveState(1, 5, "Example", "Example map", "Example mode", "v0.1");

            // Regularly send live state updates with random number of players.
            while (true)
            {
                yield return new WaitForSeconds(2f);

                // Send random values simulating changing active player count.
                SendLiveState(UnityEngine.Random.Range(1, 6), 5, "Example", "Example map", "Example mode", "v0.1");
            }
        }

        private void OnEnable()
        {
            server.SoftStopReceived += OnServerSoftStopReceived;
            server.AllocatedReceived += OnServerAllocatedReceived;
            server.MetadataReceived += OnServerMetadataReceived;
            server.HostInformationReceived += OnServerHostInformationReceived;
            server.ApplicationInstanceInformationReceived += OnServerApplicationInstanceInformationReceived;
        }

        private void OnDisable()
        {
            server.SoftStopReceived -= OnServerSoftStopReceived;
            server.AllocatedReceived -= OnServerAllocatedReceived;
            server.MetadataReceived -= OnServerMetadataReceived;
            server.HostInformationReceived -= OnServerHostInformationReceived;
            server.ApplicationInstanceInformationReceived -= OnServerApplicationInstanceInformationReceived;
        }

        /// <summary>
        /// SoftStopReceived event handler.
        /// </summary>
        private static void OnServerSoftStopReceived(int timeout)
        {
            Debug.LogFormat("Received <b>Soft Stop</b> packet with timeout {0}. Shutting down.", timeout);
            
#if UNITY_EDITOR
            // In the Editor, when a soft stop packet received, stop playing the game.
            EditorApplication.isPlaying = false;
#else
            // In the build, when a soft stop packet received, quit the application.
            Application.Quit();
#endif
        }

        /// <summary>
        /// AllocatedReceived event handler.
        /// </summary>
        private void OnServerAllocatedReceived(OneArray metadata)
        {
            Debug.LogFormat("Received <b>Allocated</b> packet with metadata:\n{0} : {1}\n{2} : {3}",
                            metadata.GetObject(0).GetString("key"),
                            metadata.GetObject(0).GetString("value"),
                            metadata.GetObject(1).GetString("key"),
                            metadata.GetObject(1).GetString("value"));

            StartCoroutine(BecomeAllocatedAfter(3));
        }

        /// <summary>
        /// MetadataReceived event handler.
        /// </summary>
        private static void OnServerMetadataReceived(OneArray metadata)
        {
            Debug.LogFormat("Received <b>Metadata</b> packet:\nkey : \"{0}\", valid : {1}, message_number : {2}\n" +
                            "Data: {3}, {4}, \"{5}\"",
                            metadata.GetObject(0).GetString("key"),
                            metadata.GetObject(0).GetBool("valid"),
                            metadata.GetObject(0).GetInt("message_number"),
                            metadata.GetObject(0).GetArray("data").GetBool(0),
                            metadata.GetObject(0).GetArray("data").GetInt(1),
                            metadata.GetObject(0).GetArray("data").GetString(2));
        }

        /// <summary>
        /// HostInformationReceived event handler.
        /// </summary>
        private static void OnServerHostInformationReceived(OneObject payload)
        {
            Debug.LogFormat("Received <b>Host Information</b> packet with payload:\nid : {0}, serverId : {1}",
                            payload.GetInt("id"),
                            payload.GetInt("serverId"));
        }

        /// <summary>
        /// ApplicationInstanceInformationReceived event handler.
        /// </summary>
        private static void OnServerApplicationInstanceInformationReceived(OneObject payload)
        {
            Debug.LogFormat("Received <b>Application Instance Information</b> packet with payload:\n" +
                            "fleetId: \"{0}\", hostId: {1}",
                            payload.GetString("fleetId"),
                            payload.GetInt("hostId"));
        }

        /// <summary>
        /// Helper method that wraps sending of the Live State update and logging of this action.
        /// </summary>
        private void SendLiveState(int players, int maxPlayers, string serverName, string map, string gameMode, string version)
        {
            Debug.LogFormat("Sending <b>Live State</b>: players = {0}, maxPlayers = {1}, " +
                            "serverName = {2}, map = {3}, gameMode = {4}, version = {5}",
                            players, maxPlayers, serverName, map, gameMode, version);

            server.SetLiveState(players, maxPlayers, serverName, map, gameMode, version, null);
        }

        /// <summary>
        /// Routine to wait and send the Allocated status update.
        /// </summary>
        private IEnumerator BecomeAllocatedAfter(float seconds)
        {
            // Simulate the server working.
            yield return new WaitForSeconds(seconds);

            // Simulate the server becoming allocated by a matchmaker by sending OneServerAllocated status.
            server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerAllocated);
        }
    }
}