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
            BatteryLevel.Add(new BatteryLevel(0, 0));

            Acceleration = new SourceList<Acceleration>();
            Acceleration.Add(new Acceleration(0, 0, 0, 0));

            Gsr = new SourceList<Gsr>();
            Gsr.Add(new Gsr(0, 0));

            Bvp = new SourceList<Bvp>();
            Bvp.Add(new Bvp(0, 0));

            Ibi = new SourceList<Ibi>();
            Ibi.Add(new Ibi(0, 0));

            Temperature = new SourceList<Temperature>();
            Temperature.Add(new Temperature(0, 0));

            Hr = new SourceList<Hr>();
            Hr.Add(new Hr(0, 0, 0));

            Tag = new SourceList<Tag>();
            Tag.Add(new Tag(0));

        }
    }
}
