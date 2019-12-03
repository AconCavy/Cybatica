using System;

namespace Cybatica.Empatica
{
    public readonly struct BatteryLevel : IEquatable<BatteryLevel>
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

        public bool Equals(BatteryLevel other)
        {
            return Value.Equals(other.Value) && Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return obj is BatteryLevel other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ Timestamp.GetHashCode();
            }
        }
    }

    public readonly struct Acceleration : IEquatable<Acceleration>
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

        public bool Equals(Acceleration other)
        {
            return XValue.Equals(other.XValue) && YValue.Equals(other.YValue) && ZValue.Equals(other.ZValue) &&
                   Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return obj is Acceleration other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = XValue.GetHashCode();
                hashCode = (hashCode * 397) ^ YValue.GetHashCode();
                hashCode = (hashCode * 397) ^ ZValue.GetHashCode();
                hashCode = (hashCode * 397) ^ Timestamp.GetHashCode();
                return hashCode;
            }
        }
    }

    public readonly struct Bvp : IEquatable<Bvp>
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

        public bool Equals(Bvp other)
        {
            return Value.Equals(other.Value) && Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return obj is Bvp other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ Timestamp.GetHashCode();
            }
        }
    }

    public readonly struct Gsr : IEquatable<Gsr>
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

        public bool Equals(Gsr other)
        {
            return Value.Equals(other.Value) && Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return obj is Gsr other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ Timestamp.GetHashCode();
            }
        }
    }

    public readonly struct Hr : IEquatable<Hr>
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

        public bool Equals(Hr other)
        {
            return Value.Equals(other.Value) && QualityIndex == other.QualityIndex && Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return obj is Hr other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Value.GetHashCode();
                hashCode = (hashCode * 397) ^ QualityIndex;
                hashCode = (hashCode * 397) ^ Timestamp.GetHashCode();
                return hashCode;
            }
        }
    }

    public readonly struct Ibi : IEquatable<Ibi>
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

        public bool Equals(Ibi other)
        {
            return Value.Equals(other.Value) && Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return obj is Ibi other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ Timestamp.GetHashCode();
            }
        }
    }

    public readonly struct Temperature : IEquatable<Temperature>
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

        public bool Equals(Temperature other)
        {
            return Value.Equals(other.Value) && Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return obj is Temperature other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ Timestamp.GetHashCode();
            }
        }
    }

    public readonly struct Tag : IEquatable<Tag>
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

        public bool Equals(Tag other)
        {
            return Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return obj is Tag other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Timestamp.GetHashCode();
        }
    }
}