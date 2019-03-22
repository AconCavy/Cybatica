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

    public struct HR
    {
        public float Value { get; }
        public int QualityIndex { get; }
        public double Timestamp { get; }

        public HR(float value, int qualityIndex, double timestamp)
        {
            this.Value = value;
            this.QualityIndex = qualityIndex;
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
