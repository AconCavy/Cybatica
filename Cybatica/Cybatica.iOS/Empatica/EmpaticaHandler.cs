using CoreFoundation;
using Cybatica.Empatica;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaHandler : IEmpaticaHandler
    {
        private readonly Action<DeviceStatus> _deviceStatusAction;
        private readonly Action<SensorStatus> _sensorStatusAction;
        private readonly Action<BLEStatus> _bLEStatusAction;

        private readonly EmpaticaDelegate _empaticaDelegate;
        private readonly EmpaticaDeviceDelegate _empaticaDeviceDelegate;
        private readonly List<E4linkBinding.EmpaticaDeviceManager> _devices;
        private E4linkBinding.EmpaticaDeviceManager _deviceManager;

        public EmpaticaHandler()
        {
            _bLEStatusAction = status => BLEStatus = status;
            _deviceStatusAction = status => DeviceStatus = status;
            _sensorStatusAction = status => SensorStatus = status;
            _devices = new List<E4linkBinding.EmpaticaDeviceManager>();

            _empaticaDelegate = new EmpaticaDelegate(_devices, _bLEStatusAction);
            _empaticaDeviceDelegate = new EmpaticaDeviceDelegate(
                _deviceStatusAction, _sensorStatusAction);
        }

        #region IEmpaticaHandler
        public DeviceStatus DeviceStatus { get; private set; }

        public SensorStatus SensorStatus { get; private set; }

        public BLEStatus BLEStatus { get; private set; }

        public ReadOnlyCollection<EmpaticaDevice> Devices =>
            new ReadOnlyCollection<EmpaticaDevice>(
                _devices.Select(x => new EmpaticaDevice(
                    serialNumber: x.SerialNumber,
                    name: x.Name,
                    advertisingName: x.AdvertisingName,
                    hardwareId: x.HardwareId,
                    firmwareVersion: x.FirmwareVersion))
                .ToList());

        public Action<BatteryLevel> BatteryLevelAction
        {
            get => _empaticaDeviceDelegate.BatteryLevelAction;
            set
            {
                _empaticaDeviceDelegate.BatteryLevelAction = value;
            }
        }

        public Action<Bvp> BvpAction
        {
            get => _empaticaDeviceDelegate.BvpAction;
            set
            {
                _empaticaDeviceDelegate.BvpAction = value;
            }
        }

        public Action<Ibi> IbiAction
        {
            get => _empaticaDeviceDelegate.IbiAction;
            set
            {
                _empaticaDeviceDelegate.IbiAction = value;
            }
        }

        public Action<Hr> HrAction
        {
            get => _empaticaDeviceDelegate.HrAction;
            set
            {
                _empaticaDeviceDelegate.HrAction = value;
            }
        }

        public Action<Gsr> GsrAction
        {
            get => _empaticaDeviceDelegate.GsrAction;
            set
            {
                _empaticaDeviceDelegate.GsrAction = value;
            }
        }

        public Action<Temperature> TemperatureAction
        {
            get => _empaticaDeviceDelegate.TemperatureAction;
            set
            {
                _empaticaDeviceDelegate.TemperatureAction = value;
            }
        }

        public Action<Acceleration> AccelerationAction
        {
            get => _empaticaDeviceDelegate.AccelerationAction;
            set
            {
                _empaticaDeviceDelegate.AccelerationAction = value;
            }
        }

        public Action<Tag> TagAction
        {
            get => _empaticaDeviceDelegate.TagAction;
            set
            {
                _empaticaDeviceDelegate.TagAction = value;
            }
        }

        public void Authenticate(string key)
        {
            DispatchQueue.GetGlobalQueue(DispatchQueuePriority.Background).DispatchAsync(() =>
            {
                E4linkBinding.EmpaticaAPI.AuthenticateWithAPIKey(key,
                    (status, message) =>
                    {
                        if (status)
                        {
                            Discover();
                        }
                    });
            });
        }

        public async void Connect(EmpaticaDevice device)
        {
            try
            {
                _deviceManager = _devices.Find(x => x.SerialNumber.Equals(device.SerialNumber));
                _deviceManager?.ConnectWithDeviceDelegate(_empaticaDeviceDelegate);
            }
            catch (ArgumentException)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    title: "Alert",
                    message: "The selected device could not be connected.",
                    cancel: "OK");
            }
        }

        public void Disconnect()
        {
            if (_deviceManager == null)
            {
                return;
            }

            if (_deviceManager.DeviceStatus == E4linkBinding.DeviceStatus.Connected)
            {
                _deviceManager.Disconnect();
            }
            else if (_deviceManager.DeviceStatus == E4linkBinding.DeviceStatus.Connecting)
            {
                _deviceManager.CancelConnection();
            }

            _deviceManager = null;
        }

        public void Discover()
        {
            E4linkBinding.EmpaticaAPI.DiscoverDevicesWithDelegate(_empaticaDelegate);
        }

        public void StartSession(double startedTime)
        {
            _empaticaDeviceDelegate.StartSession(startedTime);
        }

        public void StopSession()
        {
            _empaticaDeviceDelegate.StopSession();
        }
        #endregion
    }
}