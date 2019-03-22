using CoreFoundation;
using Cybatica.Empatica;
using E4linkBinding;
using Foundation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaDelegate : E4linkBinding.EmpaticaDelegate, IEmpaticaDelegateService
    {
        public bool IsAllDevicesDisconnected
        {
            get => _devices.Aggregate(true, (value, device) => value && (device.DeviceStatus == DeviceStatus.Disconnected));
        }

        public ReadOnlyCollection<EmpaticaDevice> Devices {
            get => new ReadOnlyCollection<EmpaticaDevice>(_devices
                .Select(x => new EmpaticaDevice(
                    serialNumber: x.SerialNumber,
                    name: x.Name,
                    advertisingName: x.AdvertisingName,
                    hardwareId: x.HardwareId,
                    firmwareVersion: x.FirmwareVersion))
                .ToList());
        }

        public EmpaticaBLEStatus BLEStatus { get; private set; }

        private List<EmpaticaDeviceManager> _devices;

        public EmpaticaDelegate()
        {
            _devices = new List<EmpaticaDeviceManager>();

        }

        public override void DidDiscoverDevices(NSObject[] devices)
        {
            
            if (IsAllDevicesDisconnected)
            {
                _devices.Clear();
                _devices.AddRange(devices.OfType<EmpaticaDeviceManager>().ToList());
                DispatchQueue.MainQueue.DispatchAsync(() =>
                {
                    if (IsAllDevicesDisconnected)
                    {
                        E4linkBinding.EmpaticaAPI.DiscoverDevicesWithDelegate(this);
                    }
                });
            }
        }

        public override void DidUpdateBLEStatus(BLEStatus status)
        {
            switch (status)
            {
                case E4linkBinding.BLEStatus.NotAvailable:
                    BLEStatus = EmpaticaBLEStatus.NotAvailable;
                    break;
                case E4linkBinding.BLEStatus.Ready:
                    BLEStatus = EmpaticaBLEStatus.Ready;
                    break;
                case E4linkBinding.BLEStatus.Scanning:
                    BLEStatus = EmpaticaBLEStatus.Scanning;
                    break;
            }
        }

        public EmpaticaDeviceManager GetDevice(EmpaticaDevice device)
        {
            return _devices.Find(x => x.SerialNumber.Equals(device.SerialNumber));
        }
    }
}