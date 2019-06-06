using DynamicData;
using System;

namespace Cybatica.Empatica
{
    public class EmpaticaSession
    {
        
        public SourceList<BatteryLevel> BatteryLevel { get; private set; }

        public SourceList<Acceleration> Acceleration { get; private set; }

        public SourceList<Gsr> Gsr { get; private set; }

        public SourceList<Bvp> Bvp { get; private set; }

        public SourceList<Ibi> Ibi { get; private set; }

        public SourceList<Temperature> Temperature { get; private set; }

        public SourceList<Hr> Hr { get; private set; }

        public SourceList<Tag> Tag { get; private set; }

        public EmpaticaSession()
        {
            BatteryLevel = new SourceList<BatteryLevel>();
            AddBatteryLevel(new BatteryLevel(0, 0));

            Acceleration = new SourceList<Acceleration>();
            AddAcceleration(new Acceleration(0, 0, 0, 0));

            Gsr = new SourceList<Gsr>();
            AddGsr(new Gsr(0, 0));

            Bvp = new SourceList<Bvp>();
            AddBvp(new Bvp(0, 0));

            Ibi = new SourceList<Ibi>();
            AddIbi(new Ibi(0, 0));

            Temperature = new SourceList<Temperature>();
            AddTemperature(new Temperature(0, 0));

            Hr = new SourceList<Hr>();
            AddHr(new Hr(0, 0, 0));

            Tag = new SourceList<Tag>();
            AddTag(new Tag(0));

        }

        public IObservable<IChangeSet<Bvp>> ConnectBvp() => Bvp.Connect();

        public IObservable<IChangeSet<Ibi>> ConnectIbi() => Ibi.Connect();

        public IObservable<IChangeSet<Hr>> ConnectHr() => Hr.Connect();

        public IObservable<IChangeSet<Gsr>> ConnectGsr() => Gsr.Connect();

        public IObservable<IChangeSet<Temperature>> ConnectTemperature() => Temperature.Connect();

        public void AddBatteryLevel(BatteryLevel batteryLevel) => BatteryLevel.Add(batteryLevel);

        public void AddAcceleration(Acceleration acceleration) => Acceleration.Add(acceleration);

        public void AddGsr(Gsr gsr) => Gsr.Add(gsr);

        public void AddBvp(Bvp bvp) => Bvp.Add(bvp);

        public void AddIbi(Ibi ibi) => Ibi.Add(ibi);

        public void AddTemperature(Temperature temperature) => Temperature.Add(temperature);

        public void AddHr(Hr hr) => Hr.Add(hr);

        public void AddTag(Tag tag) => Tag.Add(tag);

    }

}
