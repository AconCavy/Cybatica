using Cybatica.Empatica;
using System;
using System.Collections.ObjectModel;

namespace Cybatica.Droid.Empatica
{
    public class EmpaticaDelegate : IEmpaticaDelegate
    {
        public EmpaticaDelegate()
        {

        }

        public bool IsAllDevicesDisconnected => throw new NotImplementedException();

        public ReadOnlyCollection<EmpaticaDevice> Devices => throw new NotImplementedException();

        public EmpaticaBLEStatus BLEStatus => throw new NotImplementedException();
    }
}