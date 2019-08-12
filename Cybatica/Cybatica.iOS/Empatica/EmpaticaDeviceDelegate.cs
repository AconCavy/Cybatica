using System;
using Cybatica.Empatica;
using E4linkBinding;
using DeviceStatus = Cybatica.Empatica.DeviceStatus;
using SensorStatus = Cybatica.Empatica.SensorStatus;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaDeviceDelegate : E4linkBinding.EmpaticaDeviceDelegate
    {
        private readonly Action<DeviceStatus> _deviceStatusAction;
        private readonly Action<SensorStatus> _sensorStatusAction;
        private bool _isCapturing;
        private double _startedTime;

        public EmpaticaDeviceDelegate(
            Action<DeviceStatus> deviceStatusAction,
            Action<SensorStatus> sensorStatusAction)
        {
            _deviceStatusAction = deviceStatusAction;
            _sensorStatusAction = sensorStatusAction;
        }

        public Action<BatteryLevel> BatteryLevelAction { get; set; }

        public Action<Bvp> BvpAction { get; set; }

        public Action<Ibi> IbiAction { get; set; }

        public Action<Hr> HrAction { get; set; }

        public Action<Gsr> GsrAction { get; set; }

        public Action<Temperature> TemperatureAction { get; set; }

        public Action<Acceleration> AccelerationAction { get; set; }

        public Action<Tag> TagAction { get; set; }

        public override void DidReceiveAccelerationX(sbyte x, sbyte y, sbyte z, double timestamp,
            EmpaticaDeviceManager device)
        {
            if (!_isCapturing) return;
            AccelerationAction?.Invoke(new Acceleration(x, y, z, timestamp - _startedTime));
        }

        public override void DidReceiveBatteryLevel(float level, double timestamp, EmpaticaDeviceManager device)
        {
            if (!_isCapturing) return;
            BatteryLevelAction?.Invoke(new BatteryLevel(level, timestamp - _startedTime));
        }

        public override void DidReceiveBVP(float bvp, double timestamp, EmpaticaDeviceManager device)
        {
            if (!_isCapturing) return;
            BvpAction?.Invoke(new Bvp(bvp, timestamp - _startedTime));
        }

        public override void DidReceiveGSR(float gsr, double timestamp, EmpaticaDeviceManager device)
        {
            if (!_isCapturing) return;
            GsrAction?.Invoke(new Gsr(gsr, timestamp - _startedTime));
        }

        public override void DidReceiveHR(float hr, int qualityIndex, double timestamp, EmpaticaDeviceManager device)
        {
            if (!_isCapturing) return;
            HrAction?.Invoke(new Hr(hr, qualityIndex, timestamp - _startedTime));
        }

        public override void DidReceiveIBI(float ibi, double timestamp, EmpaticaDeviceManager device)
        {
            if (!_isCapturing) return;
            IbiAction?.Invoke(new Ibi(ibi, timestamp - _startedTime));
        }

        public override void DidReceiveTagAtTimestamp(double timestamp, EmpaticaDeviceManager device)
        {
            if (!_isCapturing) return;
            TagAction?.Invoke(new Tag(timestamp - _startedTime));
        }

        public override void DidReceiveTemperature(float temp, double timestamp, EmpaticaDeviceManager device)
        {
            if (!_isCapturing) return;
            TemperatureAction?.Invoke(new Temperature(temp, timestamp - _startedTime));
        }

        public override void DidUpdateDeviceStatus(E4linkBinding.DeviceStatus status, EmpaticaDeviceManager device)
        {
            switch (status)
            {
                case E4linkBinding.DeviceStatus.Connected:
                    _deviceStatusAction.Invoke(DeviceStatus.Connected);
                    break;
                case E4linkBinding.DeviceStatus.Connecting:
                    _deviceStatusAction.Invoke(DeviceStatus.Connecting);
                    break;
                case E4linkBinding.DeviceStatus.Disconnected:
                    _deviceStatusAction.Invoke(DeviceStatus.Disconnected);
                    break;
                case E4linkBinding.DeviceStatus.Disconnecting:
                    _deviceStatusAction.Invoke(DeviceStatus.Disconnecting);
                    break;
                case E4linkBinding.DeviceStatus.FailedToConnect:
                    _deviceStatusAction.Invoke(DeviceStatus.FailedToConnect);
                    break;
            }
        }

        public override void DidUpdateOnWristStatus(E4linkBinding.SensorStatus onWristStatus,
            EmpaticaDeviceManager device)
        {
            switch (onWristStatus)
            {
                case E4linkBinding.SensorStatus.Dead:
                    _sensorStatusAction.Invoke(SensorStatus.Dead);
                    break;
                case E4linkBinding.SensorStatus.NotOnWrist:
                    _sensorStatusAction.Invoke(SensorStatus.NotOnWrist);
                    break;
                case E4linkBinding.SensorStatus.OnWrist:
                    _sensorStatusAction.Invoke(SensorStatus.OnWrist);
                    break;
            }
        }

        public void StartSession(double startedTime)
        {
            _startedTime = startedTime;
            _isCapturing = true;
        }

        public void StopSession()
        {
            _isCapturing = false;
        }
    }
}