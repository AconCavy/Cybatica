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

        public GSR GSR => throw new NotImplementedException();

        public BVP BVP => throw new NotImplementedException();

        public IBI IBI => throw new NotImplementedException();

        public Temperature Temperature => throw new NotImplementedException();

        public HR HR => throw new NotImplementedException();

        public Tag Tag => throw new NotImplementedException();

        public EmpaticaSensorStatus SensorStatus => throw new NotImplementedException();

        public EmpaticaDeviceStatus DeviceStatus => throw new NotImplementedException();
    }
}