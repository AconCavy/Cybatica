using System;
using Cybatica.Empatica;
using DynamicData;
using E4linkBinding;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaDeviceDelegate : E4linkBinding.EmpaticaDeviceDelegate
    {
        private readonly EmpaticaSession _empaticaSession;
        private readonly Action<Cybatica.Empatica.DeviceStatus> _deviceStatusAction;
        private readonly Action<Cybatica.Empatica.SensorStatus> _sensorStatusAction;
        private double _startedTime;

        public EmpaticaDeviceDelegate(EmpaticaSession session,
            Action<Cybatica.Empatica.DeviceStatus> deviceStatusAction,
            Action<Cybatica.Empatica.SensorStatus> sensorStatusAction)
        {
            _empaticaSession = session;
            _deviceStatusAction = deviceStatusAction;
            _sensorStatusAction = sensorStatusAction;
        }

        public override void DidReceiveAccelerationX(sbyte x, sbyte y, sbyte z, double timestamp, EmpaticaDeviceManager device)
        {
            Acceleration acceleration = new Acceleration(x, y, z, timestamp - _startedTime);
            _empaticaSession.Acceleration.Add(acceleration);
        }

        public override void DidReceiveBatteryLevel(float level, double timestamp, EmpaticaDeviceManager device)
        {
            BatteryLevel batteryLevel = new BatteryLevel(level, timestamp - _startedTime);
            _empaticaSession.BatteryLevel.Add(batteryLevel);
        }

        public override void DidReceiveBVP(float bvp, double timestamp, EmpaticaDeviceManager device)
        {
            Bvp bvpValue = new Bvp(bvp, timestamp - _startedTime);
            _empaticaSession.Bvp.Add(bvpValue);
        }

        public override void DidReceiveGSR(float gsr, double timestamp, EmpaticaDeviceManager device)
        {
            Gsr gsrValue = new Gsr(gsr, timestamp - _startedTime);
            _empaticaSession.Gsr.Add(gsrValue);
        }

        public override void DidReceiveHR(float hr, int qualityIndex, double timestamp, EmpaticaDeviceManager device)
        {
            Hr hrValue = new Hr(hr, qualityIndex, timestamp - _startedTime);
            _empaticaSession.Hr.Add(hrValue);
        }

        public override void DidReceiveIBI(float ibi, double timestamp, EmpaticaDeviceManager device)
        {
            Ibi ibiValue = new Ibi(ibi, timestamp - _startedTime);
            _empaticaSession.Ibi.Add(ibiValue);
        }

        public override void DidReceiveTagAtTimestamp(double timestamp, EmpaticaDeviceManager device)
        {
            Tag tag = new Tag(timestamp - _startedTime);
            _empaticaSession.Tag.Add(tag);
        }

        public override void DidReceiveTemperature(float temp, double timestamp, EmpaticaDeviceManager device)
        {
            Temperature temperature = new Temperature(temp, timestamp - _startedTime);
            _empaticaSession.Temperature.Add(temperature);
        }

        public override void DidUpdateDeviceStatus(E4linkBinding.DeviceStatus status, EmpaticaDeviceManager device)
        {
            switch (status)
            {
                case E4linkBinding.DeviceStatus.Connected:
                    _deviceStatusAction(Cybatica.Empatica.DeviceStatus.Connected);
                    break;
                case E4linkBinding.DeviceStatus.Connecting:
                    _deviceStatusAction(Cybatica.Empatica.DeviceStatus.Connecting);
                    break;
                case E4linkBinding.DeviceStatus.Disconnected:
                    _deviceStatusAction(Cybatica.Empatica.DeviceStatus.Disconnected);
                    break;
                case E4linkBinding.DeviceStatus.Disconnecting:
                    _deviceStatusAction(Cybatica.Empatica.DeviceStatus.Disconnecting);
                    break;
                case E4linkBinding.DeviceStatus.FailedToConnect:
                    _deviceStatusAction(Cybatica.Empatica.DeviceStatus.FailedToConnect);
                    break;
            }

        }

        public override void DidUpdateOnWristStatus(E4linkBinding.SensorStatus onWristStatus, EmpaticaDeviceManager device)
        {
            switch (onWristStatus)
            {
                case E4linkBinding.SensorStatus.Dead:
                    _sensorStatusAction(Cybatica.Empatica.SensorStatus.Dead);
                    break;
                case E4linkBinding.SensorStatus.NotOnWrist:
                    _sensorStatusAction(Cybatica.Empatica.SensorStatus.NotOnWrist);
                    break;
                case E4linkBinding.SensorStatus.OnWrist:
                    _sensorStatusAction(Cybatica.Empatica.SensorStatus.OnWrist);
                    break;
            }
        }

        public void StartSession()
        {
            _startedTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public void StopSession()
        {

        }
    }
}
