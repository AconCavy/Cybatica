using Cybatica.Empatica;
using System;
using System.Collections.Generic;
using Cybatica.Models;

namespace Cybatica.Services
{
    public interface ICybaticaHandler : IEmpaticaModel, IEmpaticaSessionConnector, IOcsModel, IOcsSessionConnector, IDisposable
    {
        bool IsBaseSessionStored { get; }

        void Connect(EmpaticaDevice device);

        void Disconnect();

        void Discover();

        void InitializeSession();

        void StartSession(SessionType sessionType);

        void StopSession();

        IEnumerable<EmpaticaDevice> GetDiscoveredDevices();

        void Export();

        TimeSpan ElapsedTime { get; }

        SessionType CurrentSessionType { get; }
    }
}