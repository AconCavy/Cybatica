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

            InitializeSession();
        }

        public void InitializeSession()
        {
            BatteryLevel.Clear();
            BatteryLevel.Add(new BatteryLevel(0, 0));

            Acceleration.Clear();
            Acceleration.Add(new Acceleration(0, 0, 0, 0));

            Gsr.Clear();
            Gsr.Add(new Gsr(0, 0));

            Bvp.Clear();
            Bvp.Add(new Bvp(0, 0));

            Ibi.Clear();
            Ibi.Add(new Ibi(0, 0));

            Temperature.Clear();
            Temperature.Add(new Temperature(0, 0));

            Hr.Clear();
            Hr.Add(new Hr(0, 0, 0));

            Tag.Clear();
            Tag.Add(new Tag(0));
        }
    }
}
