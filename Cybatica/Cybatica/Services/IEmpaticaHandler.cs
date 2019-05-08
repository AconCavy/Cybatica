using System.Collections.ObjectModel;
using Cybatica.Empatica;

namespace Cybatica.Services
{
    public interface IEmpaticaHandler
    {
        ReadOnlyCollection<EmpaticaDevice> Devices { get; }

        bool IsDeviceListEmpty { get; }

        EmpaticaSession EmpaticaSession { get; }

        void InitializeSession();

        void AuthenticateWithApiKey(string key);

        void ConnectDevice(EmpaticaDevice device);

        void DisconnectDevice();

        Acceleration GetLatestAcceleration();

        BatteryLevel GetLatestBatteryLevel();

        Bvp GetLatestBvp();

        Gsr GetLatestGsr();

        Hr GetLatestHr();

        Ibi GetLatestIbi();

        Tag GetLatestTag();

        Temperature GetLatestTemperature();

        EmpaticaDeviceStatus GetDeviceStatus();

        EmpaticaSensorStatus GetEmpaticaSensorStatus();

        EmpaticaBLEStatus GetEmpaticaBLEStatus();
    }
}
