using System.Collections.ObjectModel;

namespace Cybatica.Empatica
{
    public interface IEmpaticaHandler
    {
        DeviceStatus DeviceStatus { get; }

        SensorStatus SensorStatus { get; }

        BLEStatus BLEStatus { get; }

        ReadOnlyCollection<EmpaticaDevice> Devices { get; }

        EmpaticaSession EmpaticaSession { get; }

        void InitializeSession();

        void Authenticate(string key);

        void Connect(EmpaticaDevice device);

        void Disconnect();

        void Discover();

        void StartSession();

        void StopSession();

    }
}
