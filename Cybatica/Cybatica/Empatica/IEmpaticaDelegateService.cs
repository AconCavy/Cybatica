using System.Collections.ObjectModel;

namespace Cybatica.Empatica
{
    public interface IEmpaticaDelegateService
    {
        bool IsAllDevicesDisconnected { get; }

        ReadOnlyCollection<EmpaticaDevice> Devices { get; }

        EmpaticaBLEStatus BLEStatus { get; }

    }
}
