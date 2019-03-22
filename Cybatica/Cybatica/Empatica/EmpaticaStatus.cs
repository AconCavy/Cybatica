namespace Cybatica.Empatica
{
    public enum EmpaticaDeviceStatus
    {
        Disconnected,
        Connecting,
        Connected,
        FailedToConnect,
        Disconnecting
    }

    public enum EmpaticaSensorStatus
    {
        NotOnWrist,
        OnWrist,
        Dead
    }

    public enum EmpaticaBLEStatus
    {
        NotAvailable,
        Ready,
        Scanning
    }
}
