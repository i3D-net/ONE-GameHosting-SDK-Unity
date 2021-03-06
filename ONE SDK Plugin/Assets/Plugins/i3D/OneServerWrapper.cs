﻿using System;
using System.Collections.Generic;
using i3D.Exceptions;

namespace i3D
{
    /// <summary>
    /// The C# wrapper of the Server which is the main object of the C API. A single server should be created
    /// per "game server". The server needs to be updated often to send and receive messages from an Arcus Client.
    /// </summary>
    public partial class OneServerWrapper : IDisposable
    {
        private static readonly Dictionary<IntPtr, OneServerWrapper> Servers = new Dictionary<IntPtr, OneServerWrapper>();

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
        /// Occurs when the server receives a Custom Command packet.
        /// </summary>
        public event Action<OneArray> CustomCommandReceived;

        private readonly IntPtr _ptr;
        private readonly Action<OneLogLevel, string> _logCallback;
        private readonly ushort _port;

        /// <summary>
        /// Returns the status of the server.
        /// </summary>
        /// <exception cref="OneInvalidServerStatusException">Thrown if the wrapper cannot cast the numeric value
        /// received from the C API to the status enum. </exception>
        public OneServerStatus Status
        {
            get
            {
                IntPtr statusPtr;

                int code = one_server_status(_ptr, out statusPtr);

                OneErrorValidator.Validate(code);

                int status = statusPtr.ToInt32();
                
                if (!Enum.IsDefined(typeof(OneServerStatus), status))
                    throw new OneInvalidServerStatusException(status);

                return (OneServerStatus) status;
            }
        }

        /// <summary>
        /// The port the server listens on.
        /// </summary>
        public ushort Port { get { return _port; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneServerWrapper"/> class and starts listening to the specified port.
        /// Should be disposed.
        /// </summary>
        /// <param name="port">The port to listen to.</param>
        public OneServerWrapper(ushort port) : this(null, port)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneServerWrapper"/> class and starts listening to the specified port.
        /// Should be disposed.
        /// </summary>
        /// <param name="logCallback">The logging callback.</param>
        /// <param name="port">The port to listen to.</param>
        public OneServerWrapper(Action<OneLogLevel, string> logCallback, ushort port)
        {
            _logCallback = logCallback;
            _port = port;

            int code = one_server_create(port, out _ptr);
            OneErrorValidator.Validate(code);

            code = one_server_set_logger(_ptr, LogCallback, _ptr);
            OneErrorValidator.Validate(code);

            lock (Servers)
            {
                Servers.Add(_ptr, this);
            }

            code = one_server_set_soft_stop_callback(_ptr, SoftStopCallback, _ptr);
            OneErrorValidator.Validate(code);

            code = one_server_set_allocated_callback(_ptr, AllocatedCallback, _ptr);
            OneErrorValidator.Validate(code);

            code = one_server_set_metadata_callback(_ptr, MetadataCallback, _ptr);
            OneErrorValidator.Validate(code);
            
            code = one_server_set_host_information_callback(_ptr, HostInformationCallback, _ptr);
            OneErrorValidator.Validate(code);
            
            code = one_server_set_application_instance_information_callback(
                _ptr, ApplicationInstanceInformationCallback, _ptr);
            OneErrorValidator.Validate(code);

            code = one_server_set_custom_command_callback(_ptr, CustomCommandCallback, _ptr);
            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Update the server. This must be called frequently (e.g. each frame) to process incoming and outgoing
        /// communications. Incoming messages trigger their respective incoming callbacks during the call to update.
        /// If the callback for a message is not set then the message is ignored.
        /// </summary>
        public void Update()
        {
            int code = one_server_update(_ptr);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Set the live game state information about the game server. This should be called at the least when
        /// the state changes, but it is safe to call more often if it is more convenient to do so -
        /// data is only sent out if there are changes from the previous call. Thread-safe.
        /// </summary>
        /// <param name="players">Current player count.</param>
        /// <param name="maxPlayers">Max player count allowed in current match.</param>
        /// <param name="name">Friendly server name.</param>
        /// <param name="map">Actively hosted map.</param>
        /// <param name="mode">Actively hosted game mode.</param>
        /// <param name="version">The version of the game software.</param>
        /// <param name="additionalData">Any key/value pairs set on this object will be added.</param>
        public void SetLiveState(int players,
                                 int maxPlayers,
                                 string name,
                                 string map,
                                 string mode,
                                 string version,
                                 OneObject additionalData)
        {
            int code;

            using (var name8 = new Utf8ByteArray(name))
            using (var map8 = new Utf8ByteArray(map))
            using (var mode8 = new Utf8ByteArray(mode))
            using (var version8 = new Utf8ByteArray(version))
            {
                code = one_server_set_live_state(_ptr, players, maxPlayers, name8, map8, mode8, version8,
                                                 additionalData != null ? additionalData.Ptr : IntPtr.Zero);
            }

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// This should be called at the least when the state changes, but it is safe to call more often if it is
        /// more convenient to do so - data is only sent out if there are changes from the previous call. Thread-safe.
        /// </summary>
        /// <param name="status">The current status of the game server application instance.</param>
        public void SetApplicationInstanceStatus(OneApplicationInstanceStatus status)
        {
            int code = one_server_set_application_instance_status(_ptr, (int) status);

            OneErrorValidator.Validate(code);
        }

        /// <summary>
        /// Send the reverse metadata from game server to the agent. This should be called when the game wants to update the metadata. Thread-safe.
        /// </summary>
        /// <param name="map">Current map.</param>
        /// <param name="mode">Current mode.</param>
        /// <param name="type">Current type.</param>
        public void SendReverseMetadata(string map,
                                        string mode,
                                        string type)
        {
            int code;

            using (var data = new OneArray())
            using (var mapObject = new OneObject())
            using (var modeObject = new OneObject())
            using (var typeObject = new OneObject())
            using (var map1 = new Utf8ByteArray(map))
            using (var mode1 = new Utf8ByteArray(mode))
            using (var type1 = new Utf8ByteArray(type))
            {
                mapObject.SetString("key", "map");
                mapObject.SetString("value", map1.ToString());
                modeObject.SetString("key", "mode");
                modeObject.SetString("value", mode1.ToString());
                typeObject.SetString("key", "type");
                typeObject.SetString("value", type1.ToString());
                data.PushObject(mapObject);
                data.PushObject(modeObject);
                data.PushObject(typeObject);
                code = one_server_send_reverse_metadata(_ptr, data.Ptr);
            }

            OneErrorValidator.Validate(code);
        }

        private static void LogCallback(IntPtr data, int level, IntPtr log)
        {
            OneServerWrapper server;

            if (!Servers.TryGetValue(data, out server))
                throw new InvalidOperationException("Cannot find OneServer instance");

            if (server._logCallback == null)
                return;

            if (!Enum.IsDefined(typeof(OneLogLevel), level))
                throw new OneInvalidLogLevelException(level);

            var logLevel = (OneLogLevel) level;

            server._logCallback(logLevel, new Utf8ByteArray(log).ToString());
        }

        private static void SoftStopCallback(IntPtr data, int timeout)
        {
            OneServerWrapper server;

            if (!Servers.TryGetValue(data, out server))
                throw new InvalidOperationException("Cannot find OneServer instance");

            if (server.SoftStopReceived != null)
                server.SoftStopReceived(timeout);
        }

        private static void AllocatedCallback(IntPtr data, IntPtr array)
        {
            OneServerWrapper server;

            if (!Servers.TryGetValue(data, out server))
                throw new InvalidOperationException("Cannot find OneServer instance");

            if (server.AllocatedReceived != null)
                server.AllocatedReceived(new OneArray(array));
        }

        private static void MetadataCallback(IntPtr data, IntPtr array)
        {
            OneServerWrapper server;

            if (!Servers.TryGetValue(data, out server))
                throw new InvalidOperationException("Cannot find OneServer instance");

            if (server.MetadataReceived != null)
                server.MetadataReceived(new OneArray(array));
        }

        private static void HostInformationCallback(IntPtr data, IntPtr obj)
        {
            OneServerWrapper server;

            if (!Servers.TryGetValue(data, out server))
                throw new InvalidOperationException("Cannot find OneServer instance");

            if (server.HostInformationReceived != null)
                server.HostInformationReceived(new OneObject(obj));
        }

        private static void ApplicationInstanceInformationCallback(IntPtr data, IntPtr obj)
        {
            OneServerWrapper server;

            if (!Servers.TryGetValue(data, out server))
                throw new InvalidOperationException("Cannot find OneServer instance");

            if (server.ApplicationInstanceInformationReceived != null)
                server.ApplicationInstanceInformationReceived(new OneObject(obj));
        }

        private static void CustomCommandCallback(IntPtr data, IntPtr array)
        {
            OneServerWrapper server;

            if (!Servers.TryGetValue(data, out server))
                throw new InvalidOperationException("Cannot find OneServer instance");

            if (server.CustomCommandReceived != null)
                server.CustomCommandReceived(new OneArray(array));
        }

        /// <summary>
        /// Releases the memory used by the server.
        /// </summary>
        public void Dispose()
        {
            one_server_destroy(_ptr);

            lock (Servers)
            {
                Servers.Remove(_ptr);
            }
        }
    }
}