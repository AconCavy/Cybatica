using System;
using System.Collections.Generic;
using System.Linq;
using CoreFoundation;
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

            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                if (IsAllDevicesDisconnected()) EmpaticaAPI.DiscoverDevicesWithDelegate(this);
            });
        }

        public override void DidUpdateBLEStatus(E4linkBinding.BLEStatus status)
        {
            switch (status)
            {
                case E4linkBinding.BLEStatus.NotAvailable:
                    _bleStatusAction(BleStatus.NotAvailable);
                    break;
                case E4linkBinding.BLEStatus.Ready:
                    _bleStatusAction(BleStatus.Ready);
                    break;
                case E4linkBinding.BLEStatus.Scanning:
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