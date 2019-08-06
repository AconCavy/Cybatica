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
            _empaticaDeviceDelegate = new EmpaticaDeviceDelegate(EmpaticaSession,
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

        public EmpaticaSession EmpaticaSession { get; private set; }

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

        public void InitializeSession()
        {
            EmpaticaSession = new EmpaticaSession();
        }

        public void StartSession()
        {
            _empaticaDeviceDelegate.StartSession();
        }

        public void StopSession()
        {
            _empaticaDeviceDelegate.StopSession();
        }
        #endregion
    }
}