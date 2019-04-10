namespace Cybatica.Empatica
{
    public interface IEmpaticaDeviceDelegate
    {
        EmpaticaSession EmpaticaSession { get; }

        BatteryLevel BatteryLevel { get; }

        Acceleration Acceleration { get; }

        GSR GSR { get; }

        BVP BVP { get; }

        IBI IBI { get; }

        Temperature Temperature { get; }

        HR HR { get; }

        Tag Tag { get; }

        EmpaticaSensorStatus SensorStatus { get; }

        EmpaticaDeviceStatus DeviceStatus { get; }
    }
}
