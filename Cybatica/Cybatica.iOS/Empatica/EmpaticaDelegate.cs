using System;
using System.Collections.Generic;
using System.Linq;
using Cybatica.Empatica;
using E4linkBinding;
using Foundation;
using DeviceStatus = E4linkBinding.DeviceStatus;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaDelegate : E4linkBinding.EmpaticaDelegate
    {
        private readonly Action<BleStatus> _bleStatusAction;
        private readonly List<EmpaticaDeviceManager> _devices;

        public EmpaticaDelegate(List<EmpaticaDeviceManager> devices, Action<BleStatus> bleStatusAction)
        {
            _devices = devices;
            _bleStatusAction = bleStatusAction;
        }

        public override void DidDiscoverDevices(NSObject[] devices)
        {
            if (!IsAllDevicesDisconnected()) return;
            _devices.Clear();
            _devices.AddRange(devices.OfType<EmpaticaDeviceManager>().ToList());
        }

        public override void DidUpdateBLEStatus(BLEStatus status)
        {
            switch (status)
            {
                case BLEStatus.NotAvailable:
                    _bleStatusAction(BleStatus.NotAvailable);
                    break;
                case BLEStatus.Ready:
                    _bleStatusAction(BleStatus.Ready);
                    break;
                case BLEStatus.Scanning:
                    _bleStatusAction(BleStatus.Scanning);
                    break;
            }
        }

        private bool IsAllDevicesDisconnected()
        {
            return _devices.Aggregate(true,
                (value, device) => value && device.DeviceStatus == DeviceStatus.Disconnected);
        }
    }
}