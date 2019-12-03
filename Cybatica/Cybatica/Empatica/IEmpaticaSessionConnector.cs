using DynamicData;
using System;

namespace Cybatica.Empatica
{
    public interface IEmpaticaSessionConnector
    {
        IObservable<IChangeSet<Acceleration>> AccelerationConnectable { get; }

        IObservable<IChangeSet<BatteryLevel>> BatteryConnectable { get; }
        
        IObservable<IChangeSet<Bvp>> BvpConnectable { get; }

        IObservable<IChangeSet<Gsr>> GsrConnectable { get; }

        IObservable<IChangeSet<Hr>> HrConnectable { get; }

        IObservable<IChangeSet<Ibi>> IbiConnectable { get; }

        IObservable<IChangeSet<Tag>> TagConnectable { get; }

        IObservable<IChangeSet<Temperature>> TemperatureConnectable { get; }
    }
}