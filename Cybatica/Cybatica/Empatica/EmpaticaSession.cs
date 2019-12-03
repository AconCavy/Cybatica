using DynamicData;

namespace Cybatica.Empatica
{
    public class EmpaticaSession
    {
        private readonly SourceList<Acceleration> _acceleration;
        private readonly SourceList<BatteryLevel> _batteryLevel;
        private readonly SourceList<Bvp> _bvp;
        private readonly SourceList<Gsr> _gsr;
        private readonly SourceList<Hr> _hr;
        private readonly SourceList<Ibi> _ibi;
        private readonly SourceList<Tag> _tag;
        private readonly SourceList<Temperature> _temperature;

        public EmpaticaSession()
        {
            _acceleration = new SourceList<Acceleration>();
            _batteryLevel = new SourceList<BatteryLevel>();
            _bvp = new SourceList<Bvp>();
            _gsr = new SourceList<Gsr>();
            _hr = new SourceList<Hr>();
            _ibi = new SourceList<Ibi>();
            _tag = new SourceList<Tag>();
            _temperature = new SourceList<Temperature>();

            Acceleration = _acceleration.AsObservableList();
            BatteryLevel = _batteryLevel.AsObservableList();
            Bvp = _bvp.AsObservableList();
            Gsr = _gsr.AsObservableList();
            Hr = _hr.AsObservableList();
            Ibi = _ibi.AsObservableList();
            Tag = _tag.AsObservableList();
            Temperature = _temperature.AsObservableList();
        }

        public IObservableList<Acceleration> Acceleration { get; }
        public IObservableList<BatteryLevel> BatteryLevel { get; }
        public IObservableList<Bvp> Bvp { get; }
        public IObservableList<Gsr> Gsr { get; }
        public IObservableList<Hr> Hr { get; }
        public IObservableList<Ibi> Ibi { get; }
        public IObservableList<Tag> Tag { get; }
        public IObservableList<Temperature> Temperature { get; }

        public void InitializeSession()
        {
            _acceleration.Clear();
            _acceleration.Add(new Acceleration(0, 0, 0, 0));

            _batteryLevel.Clear();
            _batteryLevel.Add(new BatteryLevel(0, 0));

            _bvp.Clear();
            _bvp.Add(new Bvp(0, 0));

            _gsr.Clear();
            _gsr.Add(new Gsr(0, 0));

            _hr.Clear();
            _hr.Add(new Hr(0, 0, 0));

            _ibi.Clear();
            _ibi.Add(new Ibi(0, 0));

            _tag.Clear();
            _tag.Add(new Tag(0));

            _temperature.Clear();
            _temperature.Add(new Temperature(0, 0));
        }

        public void AddAcceleration(in Acceleration acceleration)
        {
            _acceleration?.Add(acceleration);
        }

        public void AddBatteryLevel(in BatteryLevel batteryLevel)
        {
            _batteryLevel.Add(batteryLevel);
        }

        public void AddBvp(in Bvp bvp)
        {
            _bvp.Add(bvp);
        }

        public void AddGsr(in Gsr gsr)
        {
            _gsr.Add(gsr);
        }

        public void AddHr(in Hr hr)
        {
            _hr.Add(hr);
        }

        public void AddIbi(in Ibi ibi)
        {
            _ibi.Add(ibi);
        }

        public void AddTag(in Tag tag)
        {
            _tag.Add(tag);
        }

        public void AddTemperature(in Temperature temperature)
        {
            _temperature.Add(temperature);
        }
    }
}