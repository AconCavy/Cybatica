namespace Cybatica.Empatica
{
    public class EmpaticaDevice
    {
        public EmpaticaDevice()
        {
        }

        public EmpaticaDevice(string serialNumber, string name, string advertisingName, string hardwareId,
            string firmwareVersion)
        {
            SerialNumber = serialNumber;
            Name = name;
            AdvertisingName = advertisingName;
            HardwareId = hardwareId;
            FirmwareVersion = firmwareVersion;
        }

        public string SerialNumber { get; }
        public string Name { get; }
        public string AdvertisingName { get; }
        public string HardwareId { get; }
        public string FirmwareVersion { get; }
    }
}