namespace Cybatica.Empatica
{
    public interface IEmpaticaDeviceDelegate
    {
        EmpaticaSession EmpaticaSession { get; }

        BatteryLevel BatteryLevel { get; }

        Acceleration Acceleration { get; }

        Gsr Gsr { get; }

        Bvp Bvp { get; }

        Ibi Ibi { get; }

        Temperature Temperature { get; }

        Hr Hr { get; }

        Tag Tag { get; }

        EmpaticaSensorStatus SensorStatus { get; }

        EmpaticaDeviceStatus DeviceStatus { get; }

        void InitializeSession();
    }
}
