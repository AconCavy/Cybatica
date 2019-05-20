using System.Collections.ObjectModel;

namespace Cybatica.Empatica
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

        void RestartDiscovery();

    }
}
