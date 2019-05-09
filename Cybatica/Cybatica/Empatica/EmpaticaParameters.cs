using ReactiveUI.Fody.Helpers;

namespace Cybatica.Empatica
{
    public struct BatteryLevel
    {
        public float Value { get; }
        public double Timestamp { get; }

        public BatteryLevel(float value, double timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"{Value},{Timestamp}";
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
            XValue = x;
            YValue = y;
            ZValue = z;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"{XValue},{YValue},{ZValue},{Timestamp}";
        }
    }

    public struct Bvp
    {
        public float Value { get; }
        public double Timestamp { get; }

        public Bvp(float value, double timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"{Value},{Timestamp}";
        }
    }

    public struct Gsr
    {
        public float Value { get; }
        public double Timestamp { get; }

        public Gsr(float value, double timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"{Value},{Timestamp}";
        }
    }

    public struct Hr
    {
        public float Value { get; }
        public int QualityIndex { get; }
        public double Timestamp { get; }

        public Hr(float value, int qualityIndex, double timestamp)
        {
            Value = value;
            QualityIndex = qualityIndex;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"{Value},{Timestamp}";
        }
    }

    public struct Ibi
    {
        public float Value { get; }
        public double Timestamp { get; }

        public Ibi(float value, double timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"{Value},{Timestamp}";
        }
    }

    public struct Temperature
    {
        public float Value { get; }
        public double Timestamp { get; }

        public Temperature(float value, double timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"{Value},{Timestamp}";
        }
    }

    public struct Tag
    {
        public double Timestamp { get; }
        public Tag(double value)
        {
            Timestamp = value;
        }

        public override string ToString()
        {
            return $"{Timestamp}";
        }
    }
}
