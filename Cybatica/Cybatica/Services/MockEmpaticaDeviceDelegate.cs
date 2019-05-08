using Cybatica.Empatica;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive.Linq;

namespace Cybatica.Services
{
    public class MockEmpaticaDeviceDelegate : IEmpaticaDeviceDelegate
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

        public MockEmpaticaDeviceDelegate()
        {
            Console.WriteLine("McokEmpaticaDeviceDelegate() in mock");
            InitializeSession();

            var random = new Random();
            Observable.Interval(TimeSpan.FromMilliseconds(500))
                .Take(1200)
                .Subscribe(x =>
                {
                    Bvp = new Bvp((float)random.NextDouble(), x);
                    EmpaticaSession.AddBvp(Bvp);

                    Ibi = new Ibi((float)random.NextDouble(), x);
                    EmpaticaSession.AddIbi(Ibi);

                    Hr = new Hr((float)random.NextDouble(), 1, x);
                    EmpaticaSession.AddHr(Hr);

                    Gsr = new Gsr((float)random.NextDouble(), x);
                    EmpaticaSession.AddGsr(Gsr);

                    Temperature = new Temperature((float)random.NextDouble(), x);
                    EmpaticaSession.AddTemperature(Temperature);

                });
        }

        public void InitializeSession()
        {
            EmpaticaSession = new EmpaticaSession();
        }
    }
}
