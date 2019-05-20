using CoreFoundation;
using Cybatica.Empatica;
using E4linkBinding;
using Splat;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaAPI : IEmpaticaApi
    {
        public EmpaticaDevice Device => new EmpaticaDevice(
            serialNumber: _device?.SerialNumber,
            name: _device?.Name,
            advertisingName: _device?.AdvertisingName,
            hardwareId: _device?.HardwareId,
            firmwareVersion: _device?.FirmwareVersion);

        private readonly EmpaticaDelegate _empaticaDelegate;
        private readonly EmpaticaDeviceDelegate _deviceDelegate;
        private EmpaticaDeviceManager _device;

        public EmpaticaAPI()
        {
            _empaticaDelegate = Locator.Current.GetService<IEmpaticaDelegate>() as EmpaticaDelegate;
            _deviceDelegate = Locator.Current.GetService<IEmpaticaDeviceDelegate>() as EmpaticaDeviceDelegate;
        }

        public void AuthenticateWithAPIKey(string APIKey)
        {
            DispatchQueue.GetGlobalQueue(DispatchQueuePriority.Background).DispatchAsync(() =>
            {
                E4linkBinding.EmpaticaAPI.AuthenticateWithAPIKey(APIKey,
                    (status, message) =>
                {
                    System.Console.WriteLine($"Authenticate on iOS: {status}");
                    if (status)
                    {
                        Discover();
                    }
                });
            });
        }

        public void Disconnect()
        {
            if(_device == null)
            {
                return;
            }

            if(_device.DeviceStatus == DeviceStatus.Connected)
            {
                _device.Disconnect();
            }
            else if(_device.DeviceStatus == DeviceStatus.Connecting)
            {
                _device.CancelConnection();
            }
            _device = null;
        }

        public void Connect(EmpaticaDevice device)
        {
            _device = _empaticaDelegate.GetDevice(device);
            _device.ConnectWithDeviceDelegate(_deviceDelegate);
        }

        public void Discover()
        {
            E4linkBinding.EmpaticaAPI.DiscoverDevicesWithDelegate(_empaticaDelegate);
        }

        public void PrepareForBackground()
        {
            E4linkBinding.EmpaticaAPI.PrepareForBackground();
        }

        public void PrepareForResume()
        {
            E4linkBinding.EmpaticaAPI.PrepareForResume();
        }
    }
}