using System;
using UnityEngine;

namespace i3D
{
    /// <summary>
    /// The main object of the ONE SDK. A single server should be created per game server.
    /// The server needs to be updated often to send and receive messages from an Arcus Client.
    /// </summary>
    public class OneServer : MonoBehaviour
    {
        /// <summary>
        /// The port to bind to.
        /// </summary>
        [SerializeField, Tooltip("The port to bind to")]
        private ushort port = 19001;

        /// <summary>
        /// Whether the server should automatically run on Awake.
        /// This flag can be used when the port is static and can be set using the <see cref="port"/> field.
        /// </summary>
        [SerializeField, Tooltip("Whether the server should automatically run on Awake")]
        private bool runOnAwake = false;

        /// <summary>
        /// The minimum log level.
        /// </summary>
        [SerializeField, Tooltip("The minimum log level")]
        private OneLogLevel minimumLogLevel = OneLogLevel.OneLogLevelInfo;

        /// <summary>
        /// Whether the logs should be displayed.
        /// </summary>
        [SerializeField, Tooltip("Whether the logs should be displayed")]
        private bool enableLogs = true;

        /// <summary>
        /// Occurs when the server receives a Soft Stop packet.
        /// </summary>
        public event Action<int> SoftStopReceived;

        /// <summary>
        /// Occurs when the server receives an Allocated packet.
        /// </summary>
        public event Action<OneArray> AllocatedReceived;

        /// <summary>
        /// Occurs when the server receives a Metadata packet.
        /// </summary>
        public event Action<OneArray> MetadataReceived;

        /// <summary>
        /// Occurs when the server receives a Host Information packet.
        /// </summary>
        public event Action<OneObject> HostInformationReceived;

        /// <summary>
        /// Occurs when the server receives an Application Instance Information packet.
        /// </summary>
        public event Action<OneObject> ApplicationInstanceInformationReceived;

        /// <summary>
        /// Returns the status of the server.
        /// </summary>
        public OneServerStatus Status
        {
            get
            {
                if (!IsHeadless())
                    return OneServerStatus.OneServerStatusUninitialized;

                return _wrapper.Status;
            }
        }

        private OneServerWrapper _wrapper;
        private bool _isRunning;

        private void Awake()
        {
            if (!IsHeadless())
            {
                enabled = false;
                return;
            }

            DontDestroyOnLoad(gameObject);

            if (runOnAwake)
                Run();
        }

        private void Update()
        {
            if (!IsHeadless())
            {
                enabled = false;
                return;
            }

            if (_wrapper == null)
                throw new InvalidOperationException("Server wrapper is null");

            _wrapper.Update();
        }

        private void OnEnable()
        {
            if (!IsHeadless())
            {
                enabled = false;
                return;
            }

            if (!_isRunning)
                return;

            InitWrapper();
        }

        private void OnDisable()
        {
            if (!IsHeadless())
                return;

            if (!_isRunning)
                return;

            ShutdownWrapper();
        }

        /// <summary>
        /// Run the server and start listening on the port. Needs to be called once.
        /// Alternatively, the <see cref="runOnAwake"/> flag can be set to <code>true</code>.
        /// </summary>
        public void Run()
        {
            if (!_isRunning && enabled)
                InitWrapper();

            _isRunning = true;
        }

        /// <summary>
        /// Set a different port to bind to and listen on for incoming Client connections.
        /// </summary>
        public void SetPort(ushort newPort)
        {
            if (!IsHeadless())
                return;

            port = newPort;

            if (_wrapper != null)
            {
                ShutdownWrapper();
                InitWrapper();
            }
        }

        /// <summary>
        /// Set the live game state information about the game server. This should be called at the least when
        /// the state changes, but it is safe to call more often if it is more convenient to do so -
        /// data is only sent out if there are changes from the previous call. Thread-safe.
        /// </summary>
        /// <param name="players">Current player count.</param>
        /// <param name="maxPlayers">Max player count allowed in current match.</param>
        /// <param name="serverName">Friendly server name.</param>
        /// <param name="map">Actively hosted map.</param>
        /// <param name="gameMode">Actively hosted game mode.</param>
        /// <param name="version">The version of the game software.</param>
        /// <param name="additionalData">Any key/value pairs set on this object will be added.</param>
        public void SetLiveState(int players,
                                 int maxPlayers,
                                 string serverName,
                                 string map,
                                 string gameMode,
                                 string version,
                                 OneObject additionalData)
        {
            if (!IsHeadless())
                return;

            if (_wrapper == null)
                throw new InvalidOperationException("SDK wrapper is null. This component should be enabled in order to make this call.");

            _wrapper.SetLiveState(players, maxPlayers, serverName, map, gameMode, version, additionalData);
        }

        /// <summary>
        /// This should be called at the least when the state changes, but it is safe to call more often if it is
        /// more convenient to do so - data is only sent out if there are changes from the previous call. Thread-safe.
        /// </summary>
        /// <param name="status">The current status of the game server application instance.</param>
        public void SetApplicationInstanceStatus(OneApplicationInstanceStatus status)
        {
            if (!IsHeadless())
                return;

            if (_wrapper == null)
                throw new InvalidOperationException("SDK wrapper is null. This component should be enabled in order to make this call.");

            _wrapper.SetApplicationInstanceStatus(status);
        }

        private void WrapperOnLogReceived(OneLogLevel logLevel, string log)
        {
            if (!enableLogs)
                return;

            if ((int) logLevel >= (int) minimumLogLevel)
                Debug.LogFormat("ONE Server [{0}]: {1}", LogLevelToString(logLevel), log);
        }

        private void WrapperOnAllocatedReceived(OneArray metadata)
        {
            if (AllocatedReceived != null)
                AllocatedReceived(metadata);
        }

        private void WrapperOnMetadataReceived(OneArray metadata)
        {
            if (MetadataReceived != null)
                MetadataReceived(metadata);
        }

        private void WrapperOnHostInformationReceived(OneObject payload)
        {
            if (HostInformationReceived != null)
                HostInformationReceived(payload);
        }

        private void WrapperOnSoftStopReceived(int timeout)
        {
            if (SoftStopReceived != null)
                SoftStopReceived(timeout);
        }

        private void WrapperOnApplicationInstanceInformationReceived(OneObject payload)
        {
            if (ApplicationInstanceInformationReceived != null)
                ApplicationInstanceInformationReceived(payload);
        }

        private static string LogLevelToString(OneLogLevel level)
        {
            switch (level)
            {
                case OneLogLevel.OneLogLevelInfo: return "Info";
                case OneLogLevel.OneLogLevelError: return "Error";
                default: return null;
            }
        }

        private void InitWrapper()
        {
            if (_wrapper != null)
            {
                if (_wrapper.Port != port)
                    ShutdownWrapper();
                else
                    return;
            }

            _wrapper = new OneServerWrapper(WrapperOnLogReceived, port);

            _wrapper.AllocatedReceived += WrapperOnAllocatedReceived;
            _wrapper.MetadataReceived += WrapperOnMetadataReceived;
            _wrapper.HostInformationReceived += WrapperOnHostInformationReceived;
            _wrapper.SoftStopReceived += WrapperOnSoftStopReceived;
            _wrapper.ApplicationInstanceInformationReceived += WrapperOnApplicationInstanceInformationReceived;
        }

        private void ShutdownWrapper()
        {
            if (_wrapper != null)
            {
                _wrapper.Dispose();
                _wrapper = null;
            }
        }

        private static bool IsHeadless()
        {
#if UNITY_EDITOR
            return true;
#else
            return SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null;
#endif
        }
    }
}