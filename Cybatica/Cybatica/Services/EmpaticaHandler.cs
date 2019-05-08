using Cybatica.Empatica;
using Splat;
using System.Collections.ObjectModel;

namespace Cybatica.Services
{
    public class EmpaticaHandler : IEmpaticaHandler
    {
        public ReadOnlyCollection<EmpaticaDevice> Devices { get; }
        public bool IsDeviceListEmpty => Devices.Count == 0;
        public EmpaticaSession EmpaticaSession { get; }

        private readonly IEmpaticaApi _empaticaAPI;
        private readonly IEmpaticaDelegate _empaticaDelegate;
        private readonly IEmpaticaDeviceDelegate _deviceDelegate;

        public EmpaticaHandler()
        {
            _empaticaAPI = Locator.Current.GetService<IEmpaticaApi>();
            _empaticaDelegate = Locator.Current.GetService<IEmpaticaDelegate>();
            _deviceDelegate = Locator.Current.GetService<IEmpaticaDeviceDelegate>();

            Devices = _empaticaDelegate.Devices;
            EmpaticaSession = _deviceDelegate.EmpaticaSession;

        }

        public void InitializeSession()
        {
            _deviceDelegate.InitializeSession();
        }

        public void AuthenticateWithApiKey(string key)
        {
            _empaticaAPI.AuthenticateWithAPIKey(key);
        }

        public void ConnectDevice(EmpaticaDevice device)
        {
            _empaticaAPI.Connect(device);
        }

        public void DisconnectDevice()
        {
            _empaticaAPI.Disconnect();
        }

        public Acceleration GetLatestAcceleration()
        {
            return _deviceDelegate.Acceleration;
        }

        public BatteryLevel GetLatestBatteryLevel()
        {
            return _deviceDelegate.BatteryLevel;
        }

        public Bvp GetLatestBvp()
        {
            return _deviceDelegate.Bvp;
        }

        public Gsr GetLatestGsr()
        {
            return _deviceDelegate.Gsr;
        }

        public Hr GetLatestHr()
        {
            return _deviceDelegate.Hr;
        }

        public Ibi GetLatestIbi()
        {
            return _deviceDelegate.Ibi;
        }

        public Tag GetLatestTag()
        {
            return _deviceDelegate.Tag;
        }

        public Temperature GetLatestTemperature()
        {
            return _deviceDelegate.Temperature;
        }

        public EmpaticaDeviceStatus GetDeviceStatus()
        {
            return _deviceDelegate.DeviceStatus;
        }

        public EmpaticaSensorStatus GetEmpaticaSensorStatus()
        {
            return _deviceDelegate.SensorStatus;
        }

        public EmpaticaBLEStatus GetEmpaticaBLEStatus()
        {
            return _empaticaDelegate.BLEStatus;
        }

        
    }
}
