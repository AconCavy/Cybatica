using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cybatica.Models
{
    public class EmpaticaSession
    {
        public ReadOnlyCollection<BatteryLevel> BatteryLevel
        {
            get
            {
                return new ReadOnlyCollection<BatteryLevel>(_batteryLevel);
            }
        }

        public ReadOnlyCollection<Acceleration> Acceleration
        {
            get
            {
                return new ReadOnlyCollection<Acceleration>(_acceleration);
            }
        }

        public ReadOnlyCollection<GSR> GSR
        {
            get
            {
                return new ReadOnlyCollection<GSR>(_gsr);
            }
        }

        public ReadOnlyCollection<BVP> BVP
        {
            get
            {
                return new ReadOnlyCollection<BVP>(_bvp);
            }
        }

        public ReadOnlyCollection<IBI> IBI
        {
            get
            {
                return new ReadOnlyCollection<IBI>(_ibi);
            }
        }

        public ReadOnlyCollection<Temperature> Temperature
        {
            get
            {
                return new ReadOnlyCollection<Temperature>(_temperature);
            }
        }

        public ReadOnlyCollection<Tag> Tag
        {
            get
            {
                return new ReadOnlyCollection<Tag>(_tag);
            }
        }

        private readonly List<BatteryLevel> _batteryLevel;
        private readonly List<Acceleration> _acceleration;
        private readonly List<GSR> _gsr;
        private readonly List<BVP> _bvp;
        private readonly List<IBI> _ibi;
        private readonly List<Temperature> _temperature;
        private readonly List<Tag> _tag;

        public EmpaticaSession()
        {
            _batteryLevel = new List<BatteryLevel>();
            _acceleration = new List<Acceleration>();
            _gsr = new List<GSR>();
            _bvp = new List<BVP>();
            _ibi = new List<IBI>();
            _temperature = new List<Temperature>();
            _tag = new List<Tag>();

        }
    }

    public struct BatteryLevel
    {
        public float Value { get; }
        public double Timestamp { get; }

        public BatteryLevel(float value, double timestamp)
        {
            this.Value = value;
            this.Timestamp = timestamp;
        }
    }

    public struct Acceleration
    {
        public float XValue { get; }
        public float YValue { get; }
        public float ZValue { get; }
        public double Timestamp { get; }

        public Acceleration(float x, float y, float z, double timestamp)
        {
            this.XValue = x;
            this.YValue = y;
            this.ZValue = z;
            this.Timestamp = timestamp;
        }
    }

    public struct BVP
    {
        public float Value { get; }
        public double Timestamp { get; }

        public BVP(float value, double timestamp)
        {
            this.Value = value;
            this.Timestamp = timestamp;
        }
    }

    public struct GSR
    {
        public float Value { get; }
        public double Timestamp { get; }

        public GSR(float value, double timestamp)
        {
            this.Value = value;
            this.Timestamp = timestamp;
        }
    }

    public struct IBI
    {
        public float Value { get; }
        public double Timestamp { get; }

        public IBI(float value, double timestamp)
        {
            this.Value = value;
            this.Timestamp = timestamp;
        }
    }

    public struct Temperature
    {
        public float Value { get; }
        public double Timestamp { get; }

        public Temperature(float value, double timestamp)
        {
            this.Value = value;
            this.Timestamp = timestamp;
        }
    }

    public struct Tag
    {
        public double Value { get; }
        public Tag(double value)
        {
            this.Value = value;
        }
    }
    
}
