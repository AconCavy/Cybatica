using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Cybatica.Empatica;
using ReactiveUI.Fody.Helpers;

namespace Cybatica.Services
{
    public class MockEmpaticaDeviceDelegate : IEmpaticaDeviceDelegate
    {
        public EmpaticaSession EmpaticaSession { get; }

        public BatteryLevel BatteryLevel { get; private set; }

        public Acceleration Acceleration { get; private set; }

        public GSR GSR { get; private set; }

        public BVP BVP { get; private set; }

        public IBI IBI { get; private set; }

        public Temperature Temperature { get; private set; }

        public HR HR { get; private set; }

        public Tag Tag { get; private set; }

        public EmpaticaSensorStatus SensorStatus { get; private set; }

        public EmpaticaDeviceStatus DeviceStatus { get; private set; }

        public MockEmpaticaDeviceDelegate()
        {
            Console.WriteLine("McokEmpaticaDeviceDelegate() in mock");
            EmpaticaSession = new EmpaticaSession();

            var random = new Random();
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(120)
                .Subscribe(x =>
                {
                    BVP = new BVP((float)random.NextDouble(), x);
                    EmpaticaSession.AddBVP(BVP);

                    IBI = new IBI((float)random.NextDouble(), x);
                    EmpaticaSession.AddIBI(IBI);

                    HR = new HR((float)random.NextDouble(), 1, x);
                    EmpaticaSession.AddHR(HR);

                    GSR = new GSR((float)random.NextDouble(), x);
                    EmpaticaSession.AddGSR(GSR);

                    Temperature = new Temperature((float)random.NextDouble(), x);
                    EmpaticaSession.AddTemperature(Temperature);

                });
        }
    }
}
