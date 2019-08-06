using CoreFoundation;
using E4linkBinding;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaDelegate : E4linkBinding.EmpaticaDelegate
    {
        private readonly Action<Cybatica.Empatica.BLEStatus> _bLEStatusAction;
        private readonly List<EmpaticaDeviceManager> _devices;

        public EmpaticaDelegate(List<EmpaticaDeviceManager> devices, Action<Cybatica.Empatica.BLEStatus> bLEStatusAction)
        {
            _devices = devices;
            _bLEStatusAction = bLEStatusAction;
        }

        public override void DidDiscoverDevices(NSObject[] devices)
        {
            if (IsAllDevicesDisconnected())
            {
                _devices.Clear();
                _devices.AddRange(devices.OfType<EmpaticaDeviceManager>().ToList());

                DispatchQueue.MainQueue.DispatchAsync(() =>
                {
                    if (IsAllDevicesDisconnected())
                    {
                        EmpaticaAPI.DiscoverDevicesWithDelegate(this);
                    }
                });
            }
        }

        public override void DidUpdateBLEStatus(E4linkBinding.BLEStatus status)
        {
            switch (status)
            {
                case E4linkBinding.BLEStatus.NotAvailable:
                    _bLEStatusAction(Cybatica.Empatica.BLEStatus.NotAvailable);
                    break;
                case E4linkBinding.BLEStatus.Ready:
                    _bLEStatusAction(Cybatica.Empatica.BLEStatus.Ready);
                    break;
                case E4linkBinding.BLEStatus.Scanning:
                    _bLEStatusAction(Cybatica.Empatica.BLEStatus.Scanning);
                    break;
            }
        }

        private bool IsAllDevicesDisconnected()
        {
            return _devices.Aggregate(true, (value, device) => value && (device.DeviceStatus == E4linkBinding.DeviceStatus.Disconnected));
        }
    }
}
