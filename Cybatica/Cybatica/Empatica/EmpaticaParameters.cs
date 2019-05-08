using ReactiveUI.Fody.Helpers;

namespace Cybatica.Empatica
{
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

    public struct Bvp
    {
        public float Value { get; }
        public double Timestamp { get; }

        public Bvp(float value, double timestamp)
        {
            this.Value = value;
            this.Timestamp = timestamp;
        }
    }

    public struct Gsr
    {
        public float Value { get; }
        public double Timestamp { get; }

        public Gsr(float value, double timestamp)
        {
            this.Value = value;
            this.Timestamp = timestamp;
        }
    }

    public struct Hr
    {
        public float Value { get; }
        public int QualityIndex { get; }
        public double Timestamp { get; }

        public Hr(float value, int qualityIndex, double timestamp)
        {
            this.Value = value;
            this.QualityIndex = qualityIndex;
            this.Timestamp = timestamp;
        }
    }

    public struct Ibi
    {
        public float Value { get; }
        public double Timestamp { get; }

        public Ibi(float value, double timestamp)
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
