using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreFoundation;
using Foundation;
using UIKit;
using E4linkBinding;
using Cybatica.Empatica;
using Xamarin.Forms;
using Splat;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaAPI : IEmpaticaAPIService
    {
        public EmpaticaDevice Device => new EmpaticaDevice(
            serialNumber: _device?.SerialNumber,
            name: _device?.Name,
            advertisingName: _device?.AdvertisingName,
            hardwareId: _device?.HardwareId,
            firmwareVersion: _device?.FirmwareVersion);

        private EmpaticaDelegate _empaticaDelegate;
        private EmpaticaDeviceDelegate _deviceDelegate;
        private EmpaticaDeviceManager _device;

        public EmpaticaAPI()
        {
            _empaticaDelegate = Locator.Current.GetService<IEmpaticaDelegateService>() as EmpaticaDelegate;
            _deviceDelegate = Locator.Current.GetService<IEmpaticaDeviceDelegateService>() as EmpaticaDeviceDelegate;
        }

        public void AuthenticateWithAPIKey(string APIKey)
        {
            DispatchQueue.GetGlobalQueue(DispatchQueuePriority.Background).DispatchAsync(() =>
            {
                E4linkBinding.EmpaticaAPI.AuthenticateWithAPIKey(APIKey,
                    (status, message) =>
                {
                    if (status)
                    {
                        Discover(_empaticaDelegate);
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

        private void Discover(E4linkBinding.EmpaticaDelegate empaticaDelegate)
        {
            E4linkBinding.EmpaticaAPI.DiscoverDevicesWithDelegate(empaticaDelegate);
        }

    }
}