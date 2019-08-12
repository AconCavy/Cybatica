namespace Cybatica.Empatica
{
    public enum DeviceStatus
    {
        Disconnected,
        Connecting,
        Connected,
        FailedToConnect,
        Disconnecting
    }

    public enum SensorStatus
    {
        NotOnWrist,
        OnWrist,
        Dead
    }

    public enum BleStatus
    {
        NotAvailable,
        Ready,
        Scanning
    }
}