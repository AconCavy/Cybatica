using System;
using Cybatica.Empatica;
using E4linkBinding;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaDeviceDelegate : E4linkBinding.EmpaticaDeviceDelegate, IEmpaticaDeviceDelegate
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

        public EmpaticaDeviceDelegate()
        {
            InitializeSession();
        }

        public override void DidReceiveAccelerationX(sbyte x, sbyte y, sbyte z, double timestamp, EmpaticaDeviceManager device)
        {
            Acceleration acceleration = new Acceleration(x, y, z, timestamp - ConnectedTime);
            Acceleration = acceleration;
            EmpaticaSession.AddAcceleration(acceleration);
        }

        public override void DidReceiveBatteryLevel(float level, double timestamp, EmpaticaDeviceManager device)
        {
            BatteryLevel batteryLevel = new BatteryLevel(level, timestamp - ConnectedTime);
            BatteryLevel = batteryLevel;
            EmpaticaSession.AddBatteryLevel(batteryLevel);
        }

        public override void DidReceiveBVP(float bvp, double timestamp, EmpaticaDeviceManager device)
        {
            Bvp bvpValue = new Bvp(bvp, timestamp - ConnectedTime);
            Bvp = bvpValue;
            EmpaticaSession.AddBvp(bvpValue);
        }

        public override void DidReceiveGSR(float gsr, double timestamp, EmpaticaDeviceManager device)
        {
            Gsr gsrValue = new Gsr(gsr, timestamp - ConnectedTime);
            Gsr = gsrValue;
            EmpaticaSession.AddGsr(gsrValue);
        }

        public override void DidReceiveHR(float hr, int qualityIndex, double timestamp, EmpaticaDeviceManager device)
        {
            Hr hrValue = new Hr(hr, qualityIndex, timestamp - ConnectedTime);
            Hr = hrValue;
            EmpaticaSession.AddHr(hrValue);
        }

        public override void DidReceiveIBI(float ibi, double timestamp, EmpaticaDeviceManager device)
        {
            Ibi ibiValue = new Ibi(ibi, timestamp - ConnectedTime);
            Ibi = ibiValue;
            EmpaticaSession.AddIbi(ibiValue);
        }

        public override void DidReceiveTagAtTimestamp(double timestamp, EmpaticaDeviceManager device)
        {
            Tag tag = new Tag(timestamp - ConnectedTime);
            Tag = tag;
            EmpaticaSession.AddTag(tag);
        }

        public override void DidReceiveTemperature(float temp, double timestamp, EmpaticaDeviceManager device)
        {
            Temperature temperature = new Temperature(temp, timestamp - ConnectedTime);
            Temperature = temperature;
            EmpaticaSession.AddTemperature(temperature);
        }

        public override void DidUpdateDeviceStatus(DeviceStatus status, EmpaticaDeviceManager device)
        {
            switch (status)
            {
                case E4linkBinding.DeviceStatus.Connected:
                    DeviceStatus = EmpaticaDeviceStatus.Connected;
                    break;
                case E4linkBinding.DeviceStatus.Connecting:
                    DeviceStatus = EmpaticaDeviceStatus.Connecting;
                    break;
                case E4linkBinding.DeviceStatus.Disconnected:
                    DeviceStatus = EmpaticaDeviceStatus.Disconnected;
                    break;
                case E4linkBinding.DeviceStatus.Disconnecting:
                    DeviceStatus = EmpaticaDeviceStatus.Disconnecting;
                    break;
                case E4linkBinding.DeviceStatus.FailedToConnect:
                    DeviceStatus = EmpaticaDeviceStatus.FailedToConnect;
                    break;
            }

        }

        public override void DidUpdateOnWristStatus(SensorStatus onWristStatus, EmpaticaDeviceManager device)
        {
            switch (onWristStatus)
            {
                case E4linkBinding.SensorStatus.Dead:
                    SensorStatus = EmpaticaSensorStatus.Dead;
                    break;
                case E4linkBinding.SensorStatus.NotOnWrist:
                    SensorStatus = EmpaticaSensorStatus.NotOnWrist;
                    break;
                case E4linkBinding.SensorStatus.OnWrist:
                    SensorStatus = EmpaticaSensorStatus.OnWrist;
                    break;
            }
        }

        public void InitializeSession()
        {
            EmpaticaSession = new EmpaticaSession();
        }

        public void SetConnectedTime()
        {
            ConnectedTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}