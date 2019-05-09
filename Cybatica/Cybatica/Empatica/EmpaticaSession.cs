using DynamicData;

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
            Acceleration = new SourceList<Acceleration>();
            Gsr = new SourceList<Gsr>();
            Bvp = new SourceList<Bvp>();
            Ibi = new SourceList<Ibi>();
            Temperature = new SourceList<Temperature>();
            Hr = new SourceList<Hr>();
            Tag = new SourceList<Tag>();

        }

        public void AddBatteryLevel(BatteryLevel batteryLevel)
        {
            BatteryLevel.Add(batteryLevel);

        }

        public void AddAcceleration(Acceleration acceleration)
        {
            Acceleration.Add(acceleration);

        }

        public void AddGsr(Gsr gsr)
        {
            Gsr.Add(gsr);

        }

        public void AddBvp(Bvp bvp)
        {
            Bvp.Add(bvp);

        }

        public void AddIbi(Ibi ibi)
        {
            Ibi.Add(ibi);

        }

        public void AddTemperature(Temperature temperature)
        {
            Temperature.Add(temperature);

        }

        public void AddHr(Hr hr)
        {
            Hr.Add(hr);

        }

        public void AddTag(Tag tag)
        {
            Tag.Add(tag);

        }

    }

}
