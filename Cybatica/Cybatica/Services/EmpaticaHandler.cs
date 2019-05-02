using Splat;
using System.Collections.ObjectModel;
using Cybatica.Empatica;
using System.Threading.Tasks;

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

        }

        public ReadOnlyCollection<EmpaticaDevice> Devices => EmpaticaDelegate.Devices;

        public void AuthenticateDevice()
        {
            EmpaticaAPI.AuthenticateWithAPIKey(AppPrivateInformations.EmpaticaAPIKey);
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
        }

        public BatteryLevel GetLatestBatteryLevel()
        {
            return DeviceDelegate.BatteryLevel;
        }

        public BVP GetLatestBVP()
        {
            return DeviceDelegate.BVP;
        }

        public GSR GetLatestGSR()
        {
            return DeviceDelegate.GSR;
        }

        public HR GetLatestHR()
        {
            return DeviceDelegate.HR;
        }

        public IBI GetLatestIBI()
        {
            return DeviceDelegate.IBI;
        }

        public Tag GetLatestTag()
        {
            return DeviceDelegate.Tag;
        }

        public Temperature GetLatestTemperature()
        {
            return DeviceDelegate.Temperature;
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
