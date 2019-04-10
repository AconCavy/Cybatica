using Splat;
using System.Collections.ObjectModel;
using Cybatica.Empatica;

namespace Cybatica.Services
{
    public class EmpaticaHandler : IEmpaticaHandler
    {
        public IEmpaticaAPI EmpaticaAPI { get; }
        public IEmpaticaDelegate EmpaticaDelegate { get; }
        public IEmpaticaDeviceDelegate DeviceDelegate { get; }

        public EmpaticaHandler()
        {
            EmpaticaAPI = Locator.Current.GetService<IEmpaticaAPI>();
            EmpaticaDelegate = Locator.Current.GetService<IEmpaticaDelegate>();
            DeviceDelegate = Locator.Current.GetService<IEmpaticaDeviceDelegate>();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IEmpaticaHandler));

        }

        public ReadOnlyCollection<EmpaticaDevice> Devices => EmpaticaDelegate.Devices;

        public void AuthenticateDevice()
        {
            EmpaticaAPI.AuthenticateWithAPIKey(EmpaticaPrivacyInformation.EmpaticaAPIKey);
        }

        public void ConnectDevice(EmpaticaDevice device)
        {
            EmpaticaAPI.Connect(device);
        }

        public void DisconnectDevice()
        {
            EmpaticaAPI.Disconnect();
        }

        public bool IsDeviceListEmpty => Devices.Count == 0;

        public Acceleration GetLatestAcceleration()
        {
            return DeviceDelegate.Acceleration;
            //return _deviceDelegate.EmpaticaSession.Acceleration.LastOrDefault();
        }

        public BatteryLevel GetLatestBatteryLevel()
        {
            return DeviceDelegate.BatteryLevel;
            //return _deviceDelegate.EmpaticaSession.BatteryLevel.LastOrDefault();
        }

        public BVP GetLatestBVP()
        {
            return DeviceDelegate.BVP;
            //return _deviceDelegate.EmpaticaSession.BVP.LastOrDefault();
        }

        public GSR GetLatestGSR()
        {
            return DeviceDelegate.GSR;
            //return _deviceDelegate.EmpaticaSession.GSR.LastOrDefault();
        }

        public HR GetLatestHR()
        {
            return DeviceDelegate.HR;
            //return _deviceDelegate.EmpaticaSession.HR.LastOrDefault();
        }

        public IBI GetLatestIBI()
        {
            return DeviceDelegate.IBI;
            //return _deviceDelegate.EmpaticaSession.IBI.LastOrDefault();
        }

        public Tag GetLatestTag()
        {
            return DeviceDelegate.Tag;
            //return _deviceDelegate.EmpaticaSession.Tag.LastOrDefault();
        }

        public Temperature GetLatestTemperature()
        {
            return DeviceDelegate.Temperature;
            //return _deviceDelegate.EmpaticaSession.Temperature.LastOrDefault();
        }

        public EmpaticaDeviceStatus GetDeviceStatus()
        {
            return DeviceDelegate.DeviceStatus;
        }

        public EmpaticaSensorStatus GetEmpaticaSensorStatus()
        {
            return DeviceDelegate.SensorStatus;
        }

        public EmpaticaBLEStatus GetEmpaticaBLEStatus()
        {
            return EmpaticaDelegate.BLEStatus;
        }


    }
}
