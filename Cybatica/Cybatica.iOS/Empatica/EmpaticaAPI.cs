using CoreFoundation;
using Cybatica.Empatica;
using E4linkBinding;
using Splat;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaApi : IEmpaticaApi
    {
        public EmpaticaDevice Device => new EmpaticaDevice(
            serialNumber: _deviceManager?.SerialNumber,
            name: _deviceManager?.Name,
            advertisingName: _deviceManager?.AdvertisingName,
            hardwareId: _deviceManager?.HardwareId,
            firmwareVersion: _deviceManager?.FirmwareVersion);

        private readonly EmpaticaDelegate _empaticaDelegate;
        private readonly EmpaticaDeviceDelegate _deviceDelegate;
        private EmpaticaDeviceManager _deviceManager;

        public EmpaticaApi()
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
            if (_deviceManager == null)
            {
                return;
            }

            if (_deviceManager.DeviceStatus == DeviceStatus.Connected)
            {
                _deviceManager.Disconnect();
            }
            else if (_deviceManager.DeviceStatus == DeviceStatus.Connecting)
            {
                _deviceManager.CancelConnection();
            }
            _deviceManager = null;
        }

        public void Connect(EmpaticaDevice device)
        {
            _deviceManager = _empaticaDelegate.GetDevice(device);
            _deviceManager.ConnectWithDeviceDelegate(_deviceDelegate);
            _deviceDelegate.SetConnectedTime();
        }

        public void Discover()
        {
            E4linkBinding.EmpaticaAPI.DiscoverDevicesWithDelegate(_empaticaDelegate);
        }

        public void PrepareForBackground()
        {
            //E4linkBinding.EmpaticaAPI.PrepareForBackground();
        }

        public void PrepareForResume()
        {
            //E4linkBinding.EmpaticaAPI.PrepareForResume();
        }
    }
}