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

        public void AddBatteryLevel(BatteryLevel batteryLevel) => BatteryLevel.Add(batteryLevel);

        public void AddAcceleration(Acceleration acceleration) => Acceleration.Add(acceleration);

        public void AddGsr(Gsr gsr) => Gsr.Add(gsr);

        public void AddBvp(Bvp bvp) => Bvp.Add(bvp);

        public void AddIbi(Ibi ibi) => Ibi.Add(ibi);

        public void AddTemperature(Temperature temperature) => Temperature.Add(temperature);

        public void AddHr(Hr hr) => Hr.Add(hr);

        public void AddTag(Tag tag) => Tag.Add(tag);

        /*
        public ReadOnlyCollection<BatteryLevel> BatteryLevel => new ReadOnlyCollection<BatteryLevel>(_batteryLevel);

        public ReadOnlyCollection<Acceleration> Acceleration => new ReadOnlyCollection<Acceleration>(_acceleration);

        public ReadOnlyCollection<GSR> GSR => new ReadOnlyCollection<GSR>(_gsr);

        public ReadOnlyCollection<BVP> BVP => new ReadOnlyCollection<BVP>(_bvp);

        public ReadOnlyCollection<IBI> IBI => new ReadOnlyCollection<IBI>(_ibi);

        public ReadOnlyCollection<Temperature> Temperature => new ReadOnlyCollection<Temperature>(_temperature);

        public ReadOnlyCollection<HR> HR => new ReadOnlyCollection<HR>(_hr);

        public ReadOnlyCollection<Tag> Tag => new ReadOnlyCollection<Tag>(_tag);

        private List<BatteryLevel> _batteryLevel;
        private List<Acceleration> _acceleration;
        private List<GSR> _gsr;
        private List<BVP> _bvp;
        private List<IBI> _ibi;
        private List<Temperature> _temperature;
        private List<HR> _hr;
        private List<Tag> _tag;

        public EmpaticaSession()
        {
            _batteryLevel = new List<BatteryLevel>();
            _acceleration = new List<Acceleration>();
            _gsr = new List<GSR>();
            _bvp = new List<BVP>();
            _ibi = new List<IBI>();
            _temperature = new List<Temperature>();
            _hr = new List<HR>();
            _tag = new List<Tag>();

        }

        public void AddBatteryLevel(BatteryLevel batteryLevel) => _batteryLevel.Add(batteryLevel);

        public void AddAcceleration(Acceleration acceleration) => _acceleration.Add(acceleration);

        public void AddGSR(GSR gsr) => _gsr.Add(gsr);

        public void AddBVP(BVP bvp) => _bvp.Add(bvp);

        public void AddIBI(IBI ibi) => _ibi.Add(ibi);

        public void AddTemperature(Temperature temperature) => _temperature.Add(temperature);

        public void AddHR(HR hr) => _hr.Add(hr);

        public void AddTag(Tag tag) => _tag.Add(tag);
        */
    }

}
