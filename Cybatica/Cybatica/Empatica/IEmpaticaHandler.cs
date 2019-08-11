using System;
using System.Collections.ObjectModel;

namespace Cybatica.Empatica
{
    public interface IEmpaticaHandler
    {
        DeviceStatus DeviceStatus { get; }

        SensorStatus SensorStatus { get; }

        BLEStatus BLEStatus { get; }

        ReadOnlyCollection<EmpaticaDevice> Devices { get; }

        Action<BatteryLevel> BatteryLevelAction { get; set; }

        Action<Bvp> BvpAction { get; set; }

        Action<Ibi> IbiAction { get; set; }

        Action<Hr> HrAction { get; set; }

        Action<Gsr> GsrAction { get; set; }

        Action<Temperature> TemperatureAction { get; set; }

        Action<Acceleration> AccelerationAction { get; set; }

        Action<Tag> TagAction { get; set; }

        void Authenticate(string key);

        void Connect(EmpaticaDevice device);

        void Disconnect();

        void Discover();

        void StartSession(double startedTime);

        void StopSession();

    }
}
