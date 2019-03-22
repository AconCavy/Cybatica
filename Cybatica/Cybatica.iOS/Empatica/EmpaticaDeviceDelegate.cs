using Cybatica.Empatica;
using E4linkBinding;
using Xamarin.Forms;

namespace Cybatica.iOS.Empatica
{
    public class EmpaticaDeviceDelegate : E4linkBinding.EmpaticaDeviceDelegate, IEmpaticaDeviceDelegateService
    {

        public EmpaticaSession EmpaticaSession { get; private set; }

        public EmpaticaSensorStatus SensorStatus { get; private set; }

        public EmpaticaDeviceStatus DeviceStatus { get; private set; }

        public EmpaticaDeviceDelegate()
        {
            EmpaticaSession = new EmpaticaSession();
        }

        public override void DidReceiveAccelerationX(sbyte x, sbyte y, sbyte z, double timestamp, EmpaticaDeviceManager device)
        {
            Acceleration acceleration = new Acceleration(x, y, z, timestamp);
            EmpaticaSession.AddAcceleration(acceleration);
        }

        public override void DidReceiveBatteryLevel(float level, double timestamp, EmpaticaDeviceManager device)
        {
            BatteryLevel batteryLevel = new BatteryLevel(level, timestamp);
            EmpaticaSession.AddBatteryLevel(batteryLevel);
        }

        public override void DidReceiveBVP(float bvp, double timestamp, EmpaticaDeviceManager device)
        {
            BVP bvpValue = new BVP(bvp, timestamp);
            EmpaticaSession.AddBVP(bvpValue);
        }

        public override void DidReceiveGSR(float gsr, double timestamp, EmpaticaDeviceManager device)
        {
            GSR gsrValue = new GSR(gsr, timestamp);
            EmpaticaSession.AddGSR(gsrValue);
        }

        public override void DidReceiveHR(float hr, int qualityIndex, double timestamp, EmpaticaDeviceManager device)
        {
            HR hrValue = new HR(hr, qualityIndex, timestamp);
            EmpaticaSession.AddHR(hrValue);
        }

        public override void DidReceiveIBI(float ibi, double timestamp, EmpaticaDeviceManager device)
        {
            IBI ibiValue = new IBI(ibi, timestamp);
            EmpaticaSession.AddIBI(ibiValue);
        }

        public override void DidReceiveTagAtTimestamp(double timestamp, EmpaticaDeviceManager device)
        {
            Tag tag = new Tag(timestamp);
            EmpaticaSession.AddTag(tag);
        }

        public override void DidReceiveTemperature(float temp, double timestamp, EmpaticaDeviceManager device)
        {
            Temperature temperature = new Temperature(temp, timestamp);
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
                    SensorStatus = EmpaticaSensorStatus.Dead;
                    break;
                case E4linkBinding.SensorStatus.OnWrist:
                    SensorStatus = EmpaticaSensorStatus.OnWrist;
                    break;
            }
        }
    }
}