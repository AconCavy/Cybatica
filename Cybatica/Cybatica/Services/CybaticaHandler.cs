using Cybatica.Empatica;
using Cybatica.Utilities;
using Splat;
using System.Collections.ObjectModel;
using System.Linq;

namespace Cybatica.Services
{
    public class CybaticaHandler : ICybaticaHandler, IEmpaticaHandler
    {
        public ReadOnlyCollection<EmpaticaDevice> Devices => _empaticaDelegate.Devices;
        public bool IsDeviceListEmpty => Devices.Count == 0;
        public EmpaticaSession EmpaticaSession { get; }

        private readonly IEmpaticaApi _empaticaAPI;
        private readonly IEmpaticaDelegate _empaticaDelegate;
        private readonly IEmpaticaDeviceDelegate _deviceDelegate;

        public CybaticaHandler()
        {
            _empaticaAPI = Locator.Current.GetService<IEmpaticaApi>();
            _empaticaDelegate = Locator.Current.GetService<IEmpaticaDelegate>();
            _deviceDelegate = Locator.Current.GetService<IEmpaticaDeviceDelegate>();

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
            RestartDiscovery();
        }

        public void RestartDiscovery()
        {
            if(GetEmpaticaBLEStatus() != EmpaticaBLEStatus.Ready)
            {
                return;
            }

            if (_empaticaDelegate.IsAllDevicesDisconnected)
            {
                _empaticaAPI.Discover();
            }
        }

        public Acceleration GetAcceleration()
        {
            return _deviceDelegate.Acceleration;
        }

        public BatteryLevel GetBatteryLevel()
        {
            return _deviceDelegate.BatteryLevel;
        }

        public Bvp GetBvp()
        {
            return _deviceDelegate.Bvp;
        }

        public Gsr GetGsr()
        {
            return _deviceDelegate.Gsr;
        }

        public Hr GetHr()
        {
            return _deviceDelegate.Hr;
        }

        public Ibi GetIbi()
        {
            return _deviceDelegate.Ibi;
        }

        public Tag GetTag()
        {
            return _deviceDelegate.Tag;
        }

        public Temperature GetTemperature()
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

        public float GetCybersickness()
        {
            return Calculator.CalcCybersickness();
        }

        public float GetNnmean()
        {
            var target = EmpaticaSession.Ibi.Items.ToArray();

            return Calculator.CalcNnmean(target);
        }

        public float GetSdnn()
        {
            var target = EmpaticaSession.Ibi.Items.ToArray();

            return Calculator.CalcSdnn(target);
        }

        public float GetRmssd()
        {
            var target = EmpaticaSession.Ibi.Items.ToArray();

            return Calculator.CalcRmssd(target);
        }

        public float GetPpSd1()
        {
            var target = EmpaticaSession.Ibi.Items.ToArray();

            return Calculator.CalcPpSd1(target);
        }

        public float GetPpSd2()
        {
            var target = EmpaticaSession.Ibi.Items.ToArray();

            return Calculator.CalcPpSd2(target);
        }

        public float GetScr()
        {
            return 0;
        }
    }
}
