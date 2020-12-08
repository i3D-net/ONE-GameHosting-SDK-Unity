using System;
using i3D.Exceptions;

namespace i3D
{
    public partial class OneServer : IDisposable
    {
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

        public OneServer() : this(null)
        {
        }

        public OneServer(Action<OneLogLevel, string> logCallback)
        {
            _logCallback = logCallback;

            int code = one_server_create(LogCallback, out _ptr);
            OneErrorValidator.Validate(code);

            code = one_server_set_soft_stop_callback(_ptr, SoftStopCallback, IntPtr.Zero);
            OneErrorValidator.Validate(code);

            code = one_server_set_allocated_callback(_ptr, AllocatedCallback, IntPtr.Zero);
            OneErrorValidator.Validate(code);

            code = one_server_set_metadata_callback(_ptr, MetadataCallback, IntPtr.Zero);
            OneErrorValidator.Validate(code);

            code = one_server_set_host_information_callback(_ptr, HostInformationCallback, IntPtr.Zero);
            OneErrorValidator.Validate(code);

            code = one_server_set_application_instance_information_callback(
                _ptr, ApplicationInstanceInformationCallback, IntPtr.Zero);
            OneErrorValidator.Validate(code);
        }

        public void Shutdown()
        {
            int code = one_server_shutdown(_ptr);

            OneErrorValidator.Validate(code);
        }

        public void Listen(ushort port)
        {
            int code = one_server_listen(_ptr, port);

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
                                                 additionalData.Ptr);

            OneErrorValidator.Validate(code);
        }

        public void SetApplicationInstanceStatus(OneApplicationInstanceStatus status)
        {
            int code = one_server_set_application_instance_status(_ptr, (int) status);

            OneErrorValidator.Validate(code);
        }

        private void LogCallback(int level, string log)
        {
            if (_logCallback == null)
                return;
            
            if (!Enum.IsDefined(typeof(OneLogLevel), level))
                throw new OneInvalidLogLevelException(level);

            var logLevel = (OneLogLevel) level;

            _logCallback(logLevel, log);
        }

        private void SoftStopCallback(IntPtr data, int timeout)
        {
            if (SoftStopReceived != null)
                SoftStopReceived(timeout);
        }

        private void AllocatedCallback(IntPtr data, IntPtr array)
        {
            if (AllocatedReceived != null)
                AllocatedReceived(new OneArray(array));
        }

        private void MetadataCallback(IntPtr data, IntPtr array)
        {
            if (MetadataReceived != null)
                MetadataReceived(new OneArray(array));
        }

        private void HostInformationCallback(IntPtr data, IntPtr obj)
        {
            if (HostInformationReceived != null)
                HostInformationReceived(new OneObject(obj));
        }

        private void ApplicationInstanceInformationCallback(IntPtr data, IntPtr obj)
        {
            if (ApplicationInstanceInformationReceived != null)
                ApplicationInstanceInformationReceived(new OneObject(obj));
        }

        public void Dispose()
        {
            one_server_destroy(_ptr);
        }
    }
}