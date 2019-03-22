namespace Cybatica.Empatica
{
    public interface IEmpaticaDeviceDelegateService
    {
        EmpaticaSession EmpaticaSession { get; }

        EmpaticaSensorStatus SensorStatus { get; }

        EmpaticaDeviceStatus DeviceStatus { get; }
    }
}
