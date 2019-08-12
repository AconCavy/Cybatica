using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoreFoundation;
using Cybatica.Empatica;
using E4linkBinding;
using DeviceStatus = Cybatica.Empatica.DeviceStatus;
using SensorStatus = Cybatica.Empatica.SensorStatus;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaHandler : IEmpaticaHandler
    {
        private readonly List<EmpaticaDeviceManager> _devices;

        private readonly EmpaticaDelegate _empaticaDelegate;
        private readonly EmpaticaDeviceDelegate _empaticaDeviceDelegate;
        private EmpaticaDeviceManager _deviceManager;

        public EmpaticaHandler()
        {
            void BleStatusAction(BleStatus status) => BleStatus = status;
            void DeviceStatusAction(DeviceStatus status) => DeviceStatus = status;
            void SensorStatusAction(SensorStatus status) => SensorStatus = status;
            _devices = new List<EmpaticaDeviceManager>();

            _empaticaDelegate = new EmpaticaDelegate(_devices, BleStatusAction);
            _empaticaDeviceDelegate = new EmpaticaDeviceDelegate(
                DeviceStatusAction, SensorStatusAction);
        }

        #region IEmpaticaHandler

        public DeviceStatus DeviceStatus { get; private set; }

        public SensorStatus SensorStatus { get; private set; }

        public BleStatus BleStatus { get; private set; }

        public ReadOnlyCollection<EmpaticaDevice> Devices =>
            new ReadOnlyCollection<EmpaticaDevice>(
                _devices.Select(x => new EmpaticaDevice(
                        x.SerialNumber,
                        x.Name,
                        x.AdvertisingName,
                        x.HardwareId,
                        x.FirmwareVersion))
                    .ToList());

        public Action<BatteryLevel> BatteryLevelAction
        {
            get => _empaticaDeviceDelegate.BatteryLevelAction;
            set => _empaticaDeviceDelegate.BatteryLevelAction = value;
        }

        public Action<Bvp> BvpAction
        {
            get => _empaticaDeviceDelegate.BvpAction;
            set => _empaticaDeviceDelegate.BvpAction = value;
        }

        public Action<Ibi> IbiAction
        {
            get => _empaticaDeviceDelegate.IbiAction;
            set => _empaticaDeviceDelegate.IbiAction = value;
        }

        public Action<Hr> HrAction
        {
            get => _empaticaDeviceDelegate.HrAction;
            set => _empaticaDeviceDelegate.HrAction = value;
        }

        public Action<Gsr> GsrAction
        {
            get => _empaticaDeviceDelegate.GsrAction;
            set => _empaticaDeviceDelegate.GsrAction = value;
        }

        public Action<Temperature> TemperatureAction
        {
            get => _empaticaDeviceDelegate.TemperatureAction;
            set => _empaticaDeviceDelegate.TemperatureAction = value;
        }

        public Action<Acceleration> AccelerationAction
        {
            get => _empaticaDeviceDelegate.AccelerationAction;
            set => _empaticaDeviceDelegate.AccelerationAction = value;
        }

        public Action<Tag> TagAction
        {
            get => _empaticaDeviceDelegate.TagAction;
            set => _empaticaDeviceDelegate.TagAction = value;
        }

        public void Authenticate(string key)
        {
            DispatchQueue.GetGlobalQueue(DispatchQueuePriority.Background).DispatchAsync(() =>
            {
                EmpaticaAPI.AuthenticateWithAPIKey(key,
                    (status, message) =>
                    {
                        if (status) Discover();
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
                    "Alert",
                    "The selected device could not be connected.",
                    "OK");
            }
        }

        public void Disconnect()
        {
            if (_deviceManager == null) return;

            switch (_deviceManager.DeviceStatus)
            {
                case E4linkBinding.DeviceStatus.Connected:
                    _deviceManager.Disconnect();
                    break;
                case E4linkBinding.DeviceStatus.Connecting:
                    _deviceManager.CancelConnection();
                    break;
            }

            _deviceManager = null;
        }

        public void Discover()
        {
            EmpaticaAPI.DiscoverDevicesWithDelegate(_empaticaDelegate);
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