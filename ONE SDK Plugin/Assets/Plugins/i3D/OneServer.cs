using System;
using System.Collections.Generic;
using i3D.Exceptions;
using UnityEngine;

namespace i3D
{
    public partial class OneServer : IDisposable
    {
        private static readonly Dictionary<IntPtr, OneServer> Servers = new Dictionary<IntPtr, OneServer>();

        public event Action<int> SoftStopReceived;
        public event Action<OneArray> AllocatedReceived;
        public event Action<OneArray> MetadataReceived;
        public event Action<OneObject> HostInformationReceived;
        public event Action<OneObject> ApplicationInstanceInformationReceived;

        private readonly IntPtr _ptr;
        private readonly Action<OneLogLevel, string> _logCallback;

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

        public OneServer(ushort port) : this(null, port)
        {
        }

        public OneServer(Action<OneLogLevel, string> logCallback, ushort port)
        {
            _logCallback = logCallback;

            int code = one_server_create(LogCallback, out _ptr);
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

            code = one_server_listen(_ptr, port);
            OneErrorValidator.Validate(code);
        }

        public void Shutdown()
        {
            int code = one_server_shutdown(_ptr);

            OneErrorValidator.Validate(code);
        }

        public void Update()
        {
            int code = one_server_update(_ptr);

            OneErrorValidator.Validate(code);
        }

        public void SetLiveState(int players,
                                 int maxPlayers,
                                 string name,
                                 string map,
                                 string mode,
                                 string version,
                                 OneObject additionalData)
        {
            int code = one_server_set_live_state(_ptr, players, maxPlayers, name, map, mode, version,
                                                 additionalData != null ? additionalData.Ptr : IntPtr.Zero);

            OneErrorValidator.Validate(code);
        }

        public void SetApplicationInstanceStatus(OneApplicationInstanceStatus status)
        {
            int code = one_server_set_application_instance_status(_ptr, (int) status);

            OneErrorValidator.Validate(code);
        }

        private int a = 0;

        private static void LogCallback(int level, string log)
        {
            Debug.LogFormat("{0}: {1}", level, log);
            /// Debug.LogFormat("{0}: {1}", a, level);
            // Debug.LogFormat("{0}: {1}", a, log);
            // Debug.LogFormat("{0}: {1}", a, _logCallback);
            // ++a;
            // if (_logCallback == null)
            //     return;
            //
            // if (!Enum.IsDefined(typeof(OneLogLevel), level))
            //     throw new OneInvalidLogLevelException(level);
            //
            // var logLevel = (OneLogLevel) level;
            //
            // _logCallback(logLevel, log);
        }

        private static void SoftStopCallback(IntPtr data, int timeout)
        {
            if (!Servers.ContainsKey(data))
                throw new InvalidOperationException("Cannot find OneServer instance");

            var server = Servers[data];

            if (server.SoftStopReceived != null)
                server.SoftStopReceived(timeout);
        }

        private static void AllocatedCallback(IntPtr data, IntPtr array)
        {
            if (!Servers.ContainsKey(data))
                throw new InvalidOperationException("Cannot find OneServer instance");

            var server = Servers[data];

            if (server.AllocatedReceived != null)
                server.AllocatedReceived(new OneArray(array));
        }

        private static void MetadataCallback(IntPtr data, IntPtr array)
        {
            if (!Servers.ContainsKey(data))
                throw new InvalidOperationException("Cannot find OneServer instance");

            var server = Servers[data];

            if (server.MetadataReceived != null)
                server.MetadataReceived(new OneArray(array));
        }

        private static void HostInformationCallback(IntPtr data, IntPtr obj)
        {
            if (!Servers.ContainsKey(data))
                throw new InvalidOperationException("Cannot find OneServer instance");

            var server = Servers[data];

            if (server.HostInformationReceived != null)
                server.HostInformationReceived(new OneObject(obj));
        }

        private static void ApplicationInstanceInformationCallback(IntPtr data, IntPtr obj)
        {
            if (!Servers.ContainsKey(data))
                throw new InvalidOperationException("Cannot find OneServer instance");

            var server = Servers[data];

            if (server.ApplicationInstanceInformationReceived != null)
                server.ApplicationInstanceInformationReceived(new OneObject(obj));
        }

        public void Dispose()
        {
            one_server_destroy(_ptr);
        }
    }
}