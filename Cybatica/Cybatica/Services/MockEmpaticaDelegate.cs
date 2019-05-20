using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Cybatica.Empatica;

namespace Cybatica.Services
{
    public class MockEmpaticaDelegate : IEmpaticaDelegate
    {
        public bool IsAllDevicesDisconnected { get; private set; }

        public ReadOnlyCollection<EmpaticaDevice> Devices => new ReadOnlyCollection<EmpaticaDevice>(_devices);

        public EmpaticaBLEStatus BLEStatus => EmpaticaBLEStatus.Ready;

        private List<EmpaticaDevice> _devices;

        public MockEmpaticaDelegate()
        {
            Console.WriteLine("MockEmpaticaDelegate() in mock");

            _devices = new List<EmpaticaDevice>{
                new EmpaticaDevice("1", "1", "1", "1", "1"),
                new EmpaticaDevice("2", "2", "2", "2", "2")
            };
        }

    }
}
