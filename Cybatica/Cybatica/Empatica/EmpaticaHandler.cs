using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Splat;

namespace Cybatica.Empatica
{
    public class EmpaticaHandler
    {
        private readonly IEmpaticaAPIService _empaticaAPI;
        private readonly IEmpaticaDelegateService _empaticaDelegate;
        private readonly IEmpaticaDeviceDelegateService _deviceDelegate;

        public EmpaticaHandler()
        {
            _empaticaAPI = Locator.Current.GetService<IEmpaticaAPIService>();
            _empaticaDelegate = Locator.Current.GetService<IEmpaticaDelegateService>();
            _deviceDelegate = Locator.Current.GetService<IEmpaticaDeviceDelegateService>();
        }

        public ReadOnlyCollection<EmpaticaDevice> Devices => _empaticaDelegate.Devices;

        public void AuthenticateDevice()
        {
            _empaticaAPI.AuthenticateWithAPIKey(EmpaticaPrivacyInformation.EmpaticaAPIKey);
        }

        public void ConnectDevice(EmpaticaDevice device)
        {
            _empaticaAPI.Connect(device);
        }

        public void DisconnectDevice()
        {
            _empaticaAPI.Disconnect();
        }

        public bool IsDeviceListEmpty => Devices.Count == 0;

        public Acceleration GetLatestAcceleration()
        {
            return _deviceDelegate.EmpaticaSession.Acceleration.LastOrDefault();
        }

        public BatteryLevel GetLatestBatteryLevel()
        {
            return _deviceDelegate.EmpaticaSession.BatteryLevel.LastOrDefault();
        }

        public BVP GetLatestBVP()
        {
            return _deviceDelegate.EmpaticaSession.BVP.LastOrDefault();
        }

        public GSR GetLatestGSR()
        {
            return _deviceDelegate.EmpaticaSession.GSR.LastOrDefault();
        }

        public HR GetLatestHR()
        {
            return _deviceDelegate.EmpaticaSession.HR.LastOrDefault();
        }

        public IBI GetLatestIBI()
        {
            return _deviceDelegate.EmpaticaSession.IBI.LastOrDefault();
        }

        public Tag GetLatestTag()
        {
            return _deviceDelegate.EmpaticaSession.Tag.LastOrDefault();
        }

        public Temperature GetLatestTemperature()
        {
            return _deviceDelegate.EmpaticaSession.Temperature.LastOrDefault();
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
