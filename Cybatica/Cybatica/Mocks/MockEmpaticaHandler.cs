using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Cybatica.Empatica;
using ReactiveUI;

namespace Cybatica.Mocks
{
    public class MockEmpaticaHandler : IEmpaticaHandler, IDisposable
    {
        private readonly List<EmpaticaDevice> _devices;
        private readonly IObservable<long> _observer;
        private IDisposable _cleanUp;
        private bool _isCapturing;
        private double _startedTime;

        public MockEmpaticaHandler()
        {
            Console.WriteLine("MockEmpaticaHandler");

            _devices = new List<EmpaticaDevice>
            {
                new EmpaticaDevice("1", "1", "1", "1", "1"),
                new EmpaticaDevice("2", "2", "2", "2", "2")
            };

            var random = new Random();
            _observer = Observable.Interval(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Where(_ => _isCapturing)
                .Do(_ =>
                {
                    var time = DateTimeOffset.Now.ToUnixTimeSeconds() - _startedTime;
                    var bvp = new Bvp(2 * (float) random.NextDouble(), time);
                    BvpAction?.Invoke(bvp);

                    var ibi = new Ibi(750 + 100 * (float) random.NextDouble(), time);
                    IbiAction?.Invoke(ibi);

                    var gsr = new Gsr((float) random.NextDouble(), time);
                    GsrAction?.Invoke(gsr);

                    var temperature = new Temperature(35.5f + (float) random.NextDouble(), time);
                    TemperatureAction?.Invoke(temperature);
                });
        }

        public void Dispose()
        {
            _cleanUp?.Dispose();
        }

        public DeviceStatus DeviceStatus { get; private set; }

        public SensorStatus SensorStatus { get; private set; }

        public BleStatus BleStatus { get; private set; }

        public ReadOnlyCollection<EmpaticaDevice> Devices => new ReadOnlyCollection<EmpaticaDevice>(_devices);

        public Action<BatteryLevel> BatteryLevelAction { get; set; }

        public Action<Bvp> BvpAction { get; set; }

        public Action<Ibi> IbiAction { get; set; }

        public Action<Hr> HrAction { get; set; }

        public Action<Gsr> GsrAction { get; set; }

        public Action<Temperature> TemperatureAction { get; set; }

        public Action<Acceleration> AccelerationAction { get; set; }

        public Action<Tag> TagAction { get; set; }

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

        public void StartSession(double startedTime)
        {
            Console.WriteLine("StartSession in mock");
            _startedTime = startedTime;
            _cleanUp = _observer.Subscribe();
            _isCapturing = true;
        }

        public void StopSession()
        {
            Console.WriteLine("StopSession in mock");
            _cleanUp?.Dispose();
            _cleanUp = null;
            _isCapturing = false;
        }
    }
}