using System;
using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace i3D.Example
{
    /// <summary>
    /// Simulates a fake game server instance and shows how to integrate and interact with the i3D ONE Arcus Server.
    /// </summary>
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

            // Start the Arcus Server. Note, if the Component's RunOnAwake flag is set to true then this is
            // not needed. The example has the flag set to false to illustrate explicit running of the
            // Server so that the port can be set first (above).
            // The Server will listen for an Arcus Client.
            server.Run();
        }

        private void Start()
        {
            // Start simulating game server behavior.
            // Here all the calls to the SDK are done within a coroutine just to give a clear example in one place.
            // In a real project, these calls can be done from any suitable place, including Update.
            StartCoroutine(Simulation());

            LogWithTimestamp(string.Format("Server status: {0}", server.Status));

            // Initialize the cached status of the server to check if it needs to be logged later.
            _lastStatus = server.Status;
        }

        private void Update()
        {
            // If the status has changed, log it and save the new one.
            if (_lastStatus != server.Status)
            {
                LogWithTimestamp(string.Format("Server status: {0}", server.Status));
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
            // Wait for the agent to connect. A real game server does not need to wait for a connection.
            // The wait here is done for testing purposes so that the state changes in the code below will be
            // logged on a connected Arcus Agent (e.g. a connected Fake Agent). Without waiting, only the final
            // state would reach the Agent upon connection.
            yield return new WaitUntil(() => server.Status == OneServerStatus.OneServerStatusReady);

            // Simulate the server starting up by sending the OneServerStarting status.
            LogWithTimestamp(string.Format("Sending <b>Application Instance Status</b>: {0} ({1})",
                                           OneApplicationInstanceStatus.OneServerStarting,
                                           (int) OneApplicationInstanceStatus.OneServerStarting));
            server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerStarting);

            // Simulate the startup taking 1 second.
            yield return new WaitForSeconds(1f);

            // Simulate the server started by sending OneServerOnline status.
            LogWithTimestamp(string.Format("Sending <b>Application Instance Status</b>: {0} ({1})",
                                           OneApplicationInstanceStatus.OneServerOnline,
                                           (int) OneApplicationInstanceStatus.OneServerOnline));
            server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerOnline);

            // Send the test Live State data to the agent.
            SendLiveState(1, 5, "Example", "Example map", "Example mode", "v0.1");
            SendReverseMetadata("Example map", "Example mode", "Example type");

            // Regularly send Live State updates with a random number of players.
            while (true)
            {
                yield return new WaitForSeconds(2f);

                // Send random values simulating changing active player count.
                SendLiveState(UnityEngine.Random.Range(1, 6), 5, "Example", "Example map", "Example mode", "v0.1");
                SendReverseMetadata("Example map", "Example mode", "Example type");
            }
        }

        private void OnEnable()
        {
            // Subscribe to notifications from the One Server.
            server.SoftStopReceived += OnServerSoftStopReceived;
            server.AllocatedReceived += OnServerAllocatedReceived;
            server.MetadataReceived += OnServerMetadataReceived;
            server.HostInformationReceived += OnServerHostInformationReceived;
            server.ApplicationInstanceInformationReceived += OnServerApplicationInstanceInformationReceived;
            server.CustomCommandReceived += OnCustomCommandReceived;
        }

        private void OnDisable()
        {
            // Unsubscribe.
            server.SoftStopReceived -= OnServerSoftStopReceived;
            server.AllocatedReceived -= OnServerAllocatedReceived;
            server.MetadataReceived -= OnServerMetadataReceived;
            server.HostInformationReceived -= OnServerHostInformationReceived;
            server.ApplicationInstanceInformationReceived -= OnServerApplicationInstanceInformationReceived;
            server.CustomCommandReceived -= OnCustomCommandReceived;
        }

        /// <summary>
        /// SoftStopReceived event handler.
        /// </summary>
        private static void OnServerSoftStopReceived(int timeout)
        {
            LogWithTimestamp(string.Format("Received <b>Soft Stop</b> packet with timeout {0}. Shutting down.", timeout));

            // A real game should gracefully quit as needed by design. For example disallowing new
            // players to join and waiting until the match ends.
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
            // Extracting keys from the metadata is optional. These are example values sent from the Fake Agent.
            // A real game may or may not have custom key values, as needed.
            using (var obj0 = metadata.GetObject(0))
            using (var obj1 = metadata.GetObject(1))
            {
                LogWithTimestamp(string.Format("Received <b>Allocated</b> packet with metadata:\n{0} : {1}\n{2} : {3}",
                                               obj0.GetString("key"),
                                               obj0.GetString("value"),
                                               obj1.GetString("key"),
                                               obj1.GetString("value")));
            }

            // Confirm that the server has acknowledged by sending OneServerAllocated status.
            server.SetApplicationInstanceStatus(OneApplicationInstanceStatus.OneServerAllocated);
        }

        /// <summary>
        /// MetadataReceived event handler.
        /// </summary>
        private static void OnServerMetadataReceived(OneArray metadata)
        {
            // Extracting keys from the metadata is optional. These are example values sent from the Fake Agent.
            // A real game may or may not have custom key values, as needed.
            using (var obj0 = metadata.GetObject(0))
            using (var obj1 = metadata.GetObject(1))
            using (var obj2 = metadata.GetObject(2))
            {
                LogWithTimestamp(string.Format("Received <b>Metadata</b> packet:\n" +
                                               "(key : \"{0}\", value : \"{1}\")\n" +
                                               "(key : \"{2}\", value : \"{3}\")\n" +
                                               "(key : \"{4}\", value : \"{5}\")\n",
                                               obj0.GetString("key"),
                                               obj0.GetString("value"),
                                               obj1.GetString("key"),
                                               obj1.GetString("value"),
                                               obj2.GetString("key"),
                                               obj2.GetString("value")));
            }
        }

        /// <summary>
        /// HostInformationReceived event handler.
        /// </summary>
        private static void OnServerHostInformationReceived(OneObject payload)
        {
            // Extracting keys from the payload is optional. A real game may or may not be interested in host
            // information key values.
            LogWithTimestamp(string.Format("Received <b>Host Information</b> packet with payload:\n" +
                                           "id : {0}, serverId : {1}",
                                           payload.GetInt("id"),
                                           payload.GetInt("serverId")));
        }

        /// <summary>
        /// ApplicationInstanceInformationReceived event handler.
        /// </summary>
        private static void OnServerApplicationInstanceInformationReceived(OneObject payload)
        {
            // Extracting keys from the payload is optional. A real game may or may not be interested in application
            // instance information key values.
            LogWithTimestamp(string.Format("Received <b>Application Instance Information</b> packet with payload:\n" +
                                           "fleetId: \"{0}\", hostId: {1}",
                                           payload.GetString("fleetId"),
                                           payload.GetInt("hostId")));
        }

        /// <summary>
        /// CustomCommandReceived event handler.
        /// </summary>
        private static void OnCustomCommandReceived(OneArray data)
        {
            // Extracting keys from the custom command is optional. These are example values sent from the Fake Agent.
            // A real game may or may not have custom key values, as needed.
            using (var obj0 = data.GetObject(0))
            using (var obj1 = data.GetObject(1))
            {
                LogWithTimestamp(string.Format("Received <b>custom command</b> packet:\n" +
                                               "(key : \"{0}\", value : \"{1}\")\n" +
                                               "(key : \"{2}\", value : \"{3}\")\n",
                                               obj0.GetString("key"),
                                               obj0.GetString("value"),
                                               obj1.GetString("key"),
                                               obj1.GetString("value")));
            }
        }

        /// <summary>
        /// Helper method that wraps sending of the Live State update and logging of this action.
        /// </summary>
        private void SendLiveState(int players, int maxPlayers, string serverName, string map, string gameMode, string version)
        {
            LogWithTimestamp(string.Format("Sending <b>Live State</b>: players = {0}, maxPlayers = {1}, " +
                                           "serverName = {2}, map = {3}, gameMode = {4}, version = {5}",
                                           players, maxPlayers, serverName, map, gameMode, version));

            // It is safe to call this every frame if it is easier to do so - the values are only
            // sent to the Agent if they have changed.
            // The final parameter can be used to send additional custom key/value pairs.
            server.SetLiveState(players, maxPlayers, serverName, map, gameMode, version, null);
        }

        /// <summary>
        /// Helper method that wraps sending of the Live State update and logging of this action.
        /// </summary>
        private void SendReverseMetadata(string map, string mode, string type)
        {
            LogWithTimestamp(string.Format("Sending <b>Rreverse metadata</b>: map = {0}, mode = {1}, " +
                                           "type = {2}",
                                           map, mode, type));

            // It is safe to call this every frame if it is easier to do so - the values are only
            // sent to the Agent if they have changed.
            // The final parameter can be used to send additional custom key/value pairs.
            server.SendReverseMetadata(map, mode, type);
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