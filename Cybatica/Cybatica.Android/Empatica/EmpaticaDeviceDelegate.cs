using Cybatica.Empatica;
using System;

namespace Cybatica.Droid.Empatica
{
    public class EmpaticaDeviceDelegate : IEmpaticaDeviceDelegate
    {
        public EmpaticaDeviceDelegate()
        {

        }

        public EmpaticaSession EmpaticaSession => throw new NotImplementedException();

        public BatteryLevel BatteryLevel => throw new NotImplementedException();

        public Acceleration Acceleration => throw new NotImplementedException();

        public Gsr Gsr => throw new NotImplementedException();

        public Bvp Bvp => throw new NotImplementedException();

        public Ibi Ibi => throw new NotImplementedException();

        public Temperature Temperature => throw new NotImplementedException();

        public Hr Hr => throw new NotImplementedException();

        public Tag Tag => throw new NotImplementedException();

        public EmpaticaSensorStatus SensorStatus => throw new NotImplementedException();

        public EmpaticaDeviceStatus DeviceStatus => throw new NotImplementedException();

        public double ConnectedTime { get; private set; }

        public void InitializeSession()
        {
            throw new NotImplementedException();
        }

        public void SetConnectedTime()
        {
            ConnectedTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}