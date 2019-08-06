using Android.Runtime;
using Com.Empatica.Empalink;
using Com.Empatica.Empalink.Config;
using Com.Empatica.Empalink.Delegate;
using Cybatica.Empatica;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Cybatica.Droid.Empatica
{
    public class EmpaticaHandler : Java.Lang.Object, IEmpaticaHandler, IEmpaDataDelegate, IEmpaStatusDelegate
    {
        private readonly List<Com.Empatica.Empalink.EmpaticaDevice> _devices;
        private readonly EmpaDeviceManager _deviceManager;

        private double _startedTime;

        public EmpaticaHandler()
        {
            _deviceManager = new EmpaDeviceManager(Android.App.Application.Context, this, this);
            _devices = new List<Com.Empatica.Empalink.EmpaticaDevice>();
        }

        #region IEmpaticaHandler
        public DeviceStatus DeviceStatus { get; private set; }

        public SensorStatus SensorStatus { get; private set; }

        public BLEStatus BLEStatus { get; private set; }

        public ReadOnlyCollection<Cybatica.Empatica.EmpaticaDevice> Devices =>
            new ReadOnlyCollection<Cybatica.Empatica.EmpaticaDevice>(
                _devices.Select(x => new Cybatica.Empatica.EmpaticaDevice(
                    serialNumber: x.SerialNumber,
                    name: x.Name,
                    advertisingName: x.AdvertisingName,
                    hardwareId: x.HardwareId,
                    firmwareVersion: x.FirmwareVersion))
                .ToList());

        public EmpaticaSession EmpaticaSession { get; private set; }

        public void Authenticate(string key)
        {
            _deviceManager.AuthenticateWithAPIKey(key);
        }

        public async void Connect(Cybatica.Empatica.EmpaticaDevice device)
        {
            _deviceManager.StopScanning();
            try
            {
                var target = _devices.Find(x => x.SerialNumber.Equals(device.SerialNumber));
                _deviceManager.ConnectDevice(target);
            }
            catch (ArgumentException)
            {
                await Application.Current.MainPage.DisplayAlert(
                    title: "Alert",
                    message: "The selected device could not be connected.",
                    cancel: "OK");
            }
        }

        public void Disconnect()
        {
            _deviceManager.Disconnect();
        }

        public void Discover()
        {
            _deviceManager.StartScanning();
        }

        public void InitializeSession()
        {
            EmpaticaSession = new EmpaticaSession();
        }

        public void StartSession()
        {
            _startedTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public void StopSession()
        {
        }
        #endregion

        #region IEmpaDataDelegate
        public void DidReceiveAcceleration(int x, int y, int z, double timestamp)
        {
            Acceleration acceleration = new Acceleration(x, y, z, timestamp - _startedTime);
            EmpaticaSession.Acceleration.Add(acceleration);
        }

        public void DidReceiveBatteryLevel(float level, double timestamp)
        {
            BatteryLevel batteryLevel = new BatteryLevel(level, timestamp - _startedTime);
            EmpaticaSession.BatteryLevel.Add(batteryLevel);
        }

        public void DidReceiveBVP(float bvp, double timestamp)
        {
            Bvp bvpValue = new Bvp(bvp, timestamp - _startedTime);
            EmpaticaSession.Bvp.Add(bvpValue);
        }

        public void DidReceiveGSR(float gsr, double timestamp)
        {
            Gsr gsrValue = new Gsr(gsr, timestamp - _startedTime);
            EmpaticaSession.Gsr.Add(gsrValue);
        }

        public void DidReceiveIBI(float ibi, double timestamp)
        {
            Ibi ibiValue = new Ibi(ibi, timestamp - _startedTime);
            EmpaticaSession.Ibi.Add(ibiValue);
        }

        public void DidReceiveTag(double timestamp)
        {
            Tag tag = new Tag(timestamp - _startedTime);
            EmpaticaSession.Tag.Add(tag);
        }

        public void DidReceiveTemperature(float t, double timestamp)
        {
            Temperature temperature = new Temperature(t, timestamp - _startedTime);
            EmpaticaSession.Temperature.Add(temperature);
        }
        #endregion

        #region IEmpaStatusDelegate
        public void DidDiscoverDevice(Com.Empatica.Empalink.EmpaticaDevice device, string deviceLabel, int rssi, bool allowed)
        {
            if (allowed)
            {
                _devices.Clear();
                _devices.Add(device);
            }
        }

        public async void DidEstablishConnection()
        {
            await Application.Current.MainPage.DisplayAlert(
                title: "Alert",
                message: "The selected device is connecting",
                cancel: "OK");
        }

        public void DidRequestEnableBluetooth()
        {
        }

        public void DidUpdateOnWristStatus([IntDef(Type = "Com.Empatica.Empalink.Config.IEmpaSensorStatus", Fields = new[] { "NotOnWrist", "OnWrist", "Dead" })] int status)
        {
            switch (status)
            {
                case EmpaSensorStatus.Dead:
                    SensorStatus = SensorStatus.Dead;
                    break;
                case EmpaSensorStatus.NotOnWrist:
                    SensorStatus = SensorStatus.NotOnWrist;
                    break;
                case EmpaSensorStatus.OnWrist:
                    SensorStatus = SensorStatus.OnWrist;
                    break;
            }
        }

        public void DidUpdateSensorStatus([IntDef(Type = "Com.Empatica.Empalink.Config.IEmpaSensorStatus", Fields = new[] { "NotOnWrist", "OnWrist", "Dead" })] int status, EmpaSensorType type)
        {
            DidUpdateOnWristStatus(status);
        }

        public void DidUpdateStatus(EmpaStatus status)
        {
            if (status.Equals(EmpaStatus.Connected))
            {
                DeviceStatus = DeviceStatus.Connected;
            }
            else if (status.Equals(EmpaStatus.Connecting))
            {
                DeviceStatus = DeviceStatus.Connecting;
            }
            else if (status.Equals(EmpaStatus.Disconnected))
            {
                DeviceStatus = DeviceStatus.Disconnected;
            }
            else if (status.Equals(EmpaStatus.Disconnecting))
            {
                DeviceStatus = DeviceStatus.Disconnecting;
            }
            else if (status.Equals(EmpaStatus.Discovering))
            {

            }
            else
            {
                DeviceStatus = DeviceStatus.FailedToConnect;
            }
        }
        #endregion
    }
}