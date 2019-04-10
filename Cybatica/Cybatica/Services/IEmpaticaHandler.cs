using System.Collections.ObjectModel;
using Cybatica.Empatica;

namespace Cybatica.Services
{
    public interface IEmpaticaHandler
    {
        ReadOnlyCollection<EmpaticaDevice> Devices { get; }

        IEmpaticaAPI EmpaticaAPI { get; }

        IEmpaticaDelegate EmpaticaDelegate { get; }

        IEmpaticaDeviceDelegate DeviceDelegate { get; }

        void AuthenticateDevice();

        void ConnectDevice(EmpaticaDevice device);

        void DisconnectDevice();

        bool IsDeviceListEmpty { get; }

        Acceleration GetLatestAcceleration();

        BatteryLevel GetLatestBatteryLevel();

        BVP GetLatestBVP();

        GSR GetLatestGSR();

        HR GetLatestHR();

        IBI GetLatestIBI();

        Tag GetLatestTag();

        Temperature GetLatestTemperature();

        EmpaticaDeviceStatus GetDeviceStatus();

        EmpaticaSensorStatus GetEmpaticaSensorStatus();

        EmpaticaBLEStatus GetEmpaticaBLEStatus();
    }
}
