using Android.Runtime;
using Com.Empatica.Empalink.Config;
using Com.Empatica.Empalink.Delegate;
using Cybatica.Empatica;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Cybatica.Droid.Empatica
{
    public class EmpaticaDeviceDelegate : Java.Lang.Object, IEmpaDataDelegate, IEmpaStatusDelegate,
        IEmpaticaDelegate, IEmpaticaDeviceDelegate
    {
        public EmpaticaSession EmpaticaSession { get; private set; }

        public BatteryLevel BatteryLevel { get; private set; }

        public Acceleration Acceleration { get; private set; }

        public Gsr Gsr { get; private set; }

        public Bvp Bvp { get; private set; }

        public Ibi Ibi { get; private set; }

        public Temperature Temperature { get; private set; }

        public Hr Hr { get; private set; }

        public Tag Tag { get; private set; }

        public EmpaticaSensorStatus SensorStatus { get; private set; }

        public EmpaticaDeviceStatus DeviceStatus { get; private set; }

        public double ConnectedTime { get; private set; }

        public bool IsAllDevicesDisconnected => DeviceStatus == EmpaticaDeviceStatus.Disconnected;

        public ReadOnlyCollection<EmpaticaDevice> Devices =>
            new ReadOnlyCollection<EmpaticaDevice>(
                _devices.Select(x => new EmpaticaDevice(
                    serialNumber: x.SerialNumber,
                    name: x.Name,
                    advertisingName: x.AdvertisingName,
                    hardwareId: x.HardwareId,
                    firmwareVersion: x.FirmwareVersion))
                .ToList());

        public EmpaticaBLEStatus BLEStatus { get; private set; }

        private readonly List<Com.Empatica.Empalink.EmpaticaDevice> _devices;

        public EmpaticaDeviceDelegate()
        {
            _devices = new List<Com.Empatica.Empalink.EmpaticaDevice>();
            InitializeSession();
        }

        public void InitializeSession()
        {
            EmpaticaSession = new EmpaticaSession();
        }

        public void SetConnectedTime()
        {
            ConnectedTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public void DidReceiveAcceleration(int x, int y, int z, double timestamp)
        {
            Acceleration acceleration = new Acceleration(x, y, z, timestamp - ConnectedTime);
            Acceleration = acceleration;
            EmpaticaSession.AddAcceleration(acceleration);
        }

        public void DidReceiveBVP(float bvp, double timestamp)
        {
            Bvp bvpValue = new Bvp(bvp, timestamp - ConnectedTime);
            Bvp = bvpValue;
            EmpaticaSession.AddBvp(bvpValue);
        }

        public void DidReceiveBatteryLevel(float level, double timestamp)
        {
            BatteryLevel batteryLevel = new BatteryLevel(level, timestamp - ConnectedTime);
            BatteryLevel = batteryLevel;
            EmpaticaSession.AddBatteryLevel(batteryLevel);
        }

        public void DidReceiveGSR(float gsr, double timestamp)
        {
            Gsr gsrValue = new Gsr(gsr, timestamp - ConnectedTime);
            Gsr = gsrValue;
            EmpaticaSession.AddGsr(gsrValue);
        }

        public void DidReceiveIBI(float ibi, double timestamp)
        {
            Ibi ibiValue = new Ibi(ibi, timestamp - ConnectedTime);
            Ibi = ibiValue;
            EmpaticaSession.AddIbi(ibiValue);
        }

        public void DidReceiveTag(double timestamp)
        {
            Tag tag = new Tag(timestamp - ConnectedTime);
            Tag = tag;
            EmpaticaSession.AddTag(tag);
        }

        public void DidReceiveTemperature(float t, double timestamp)
        {
            Temperature temperature = new Temperature(t, timestamp - ConnectedTime);
            Temperature = temperature;
            EmpaticaSession.AddTemperature(temperature);
        }

        public void DidDiscoverDevice(Com.Empatica.Empalink.EmpaticaDevice device, string deviceLabel, int rssi, bool allowed)
        {
            Console.WriteLine("Did Discover Device");
            if (allowed)
            {
                _devices.Clear();
                _devices.Add(device);
            }
        }

        public void DidEstablishConnection()
        {
        }

        public void DidRequestEnableBluetooth()
        {
        }

        public void DidUpdateOnWristStatus([IntDef(Type = "Com.Empatica.Empalink.Config.IEmpaSensorStatus", Fields = new[] { "NotOnWrist", "OnWrist", "Dead" })] int status)
        {
            switch (status)
            {
                case EmpaSensorStatus.Dead:
                    SensorStatus = EmpaticaSensorStatus.Dead;
                    break;
                case EmpaSensorStatus.NotOnWrist:
                    SensorStatus = EmpaticaSensorStatus.NotOnWrist;
                    break;
                case EmpaSensorStatus.OnWrist:
                    SensorStatus = EmpaticaSensorStatus.OnWrist;
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
                DeviceStatus = EmpaticaDeviceStatus.Connected;
            }
            else if (status.Equals(EmpaStatus.Connecting))
            {
                DeviceStatus = EmpaticaDeviceStatus.Connecting;
            }
            else if (status.Equals(EmpaStatus.Disconnected))
            {
                DeviceStatus = EmpaticaDeviceStatus.Disconnected;
            }
            else if (status.Equals(EmpaStatus.Disconnecting))
            {
                DeviceStatus = EmpaticaDeviceStatus.Disconnecting;
            }
            else if (status.Equals(EmpaStatus.Discovering))
            {

            }
            else
            {
                DeviceStatus = EmpaticaDeviceStatus.FailedToConnect;
            }
        }

        public Com.Empatica.Empalink.EmpaticaDevice GetDevice(EmpaticaDevice device)
        {
            return _devices.Find(x => x.SerialNumber.Equals(device.SerialNumber));
        }
    }
}