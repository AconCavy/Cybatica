using Cybatica.Empatica;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Cybatica.Mocks
{
    public class MockEmpaticaHandler : IEmpaticaHandler
    {
        private List<EmpaticaDevice> _devices;

        public MockEmpaticaHandler()
        {
            _devices = new List<EmpaticaDevice>{
                new EmpaticaDevice("1", "1", "1", "1", "1"),
                new EmpaticaDevice("2", "2", "2", "2", "2")
            };

            var random = new Random();
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(300)
                .Subscribe(x =>
                {
                    var bvp = new Bvp(2 * (float)random.NextDouble(), x);
                    EmpaticaSession.Bvp.Add(bvp);

                    var ibi = new Ibi(750 + 100 * (float)random.NextDouble(), x);
                    EmpaticaSession.Ibi.Add(ibi);

                    var gsr = new Gsr((float)random.NextDouble(), x);
                    EmpaticaSession.Gsr.Add(gsr);

                    var temperature = new Temperature(35.5f + (float)random.NextDouble(), x);
                    EmpaticaSession.Temperature.Add(temperature);

                });
        }

        public DeviceStatus DeviceStatus { get; private set; }

        public SensorStatus SensorStatus { get; private set; }

        public BLEStatus BLEStatus { get; private set; }

        public ReadOnlyCollection<EmpaticaDevice> Devices => new ReadOnlyCollection<EmpaticaDevice>(_devices);

        public EmpaticaSession EmpaticaSession { get; private set; }

        public void Authenticate(string key)
        {
            Console.WriteLine("Authenticate in mock");
        }

        public void Connect(EmpaticaDevice device)
        {
            Console.WriteLine("Connect in mock");
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnect in mock");
        }

        public void Discover()
        {
            Console.WriteLine("Discover in mock");
        }

        public void InitializeSession()
        {
            Console.WriteLine("InitializeSession in mock");
            EmpaticaSession = new EmpaticaSession();
        }

        public void StartSession()
        {
            Console.WriteLine("StartSession in mock");
        }

        public void StopSession()
        {
            Console.WriteLine("StopSession in mock");
        }
    }
}
