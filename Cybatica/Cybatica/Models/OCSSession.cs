using System;
using DynamicData;

namespace Cybatica.Models
{
    public readonly struct AnalysisData : IEquatable<AnalysisData>
    {
        public float Value { get; }
        public double Timestamp { get; }

        public AnalysisData(float value, double timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"{Value},{Timestamp}";
        }

        public bool Equals(AnalysisData other)
        {
            return Value.Equals(other.Value) && Timestamp.Equals(other.Timestamp);
        }

        public override bool Equals(object obj)
        {
            return obj is AnalysisData other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ Timestamp.GetHashCode();
            }
        }
    }

    public class OcsSession
    {
        private readonly SourceList<AnalysisData> _meanEda;
        private readonly SourceList<AnalysisData> _nnMean;
        private readonly SourceList<AnalysisData> _ocs;
        private readonly SourceList<AnalysisData> _peakEda;
        private readonly SourceList<AnalysisData> _sdNn;

        public OcsSession()
        {
            _ocs = new SourceList<AnalysisData>();
            _nnMean = new SourceList<AnalysisData>();
            _sdNn = new SourceList<AnalysisData>();
            _meanEda = new SourceList<AnalysisData>();
            _peakEda = new SourceList<AnalysisData>();

            Ocs = _ocs.AsObservableList();
            NnMean = _nnMean.AsObservableList();
            SdNn = _sdNn.AsObservableList();
            MeanEda = _meanEda.AsObservableList();
            PeakEda = _peakEda.AsObservableList();
        }

        public IObservableList<AnalysisData> Ocs { get; }
        public IObservableList<AnalysisData> NnMean { get; }
        public IObservableList<AnalysisData> SdNn { get; }
        public IObservableList<AnalysisData> MeanEda { get; }
        public IObservableList<AnalysisData> PeakEda { get; }

        public void InitializeSession()
        {
            _ocs.Clear();
            _ocs.Add(new AnalysisData(0, 0));

            _nnMean.Clear();
            _nnMean.Add(new AnalysisData(0, 0));

            _sdNn.Clear();
            _sdNn.Add(new AnalysisData(0, 0));

            _meanEda.Clear();
            _meanEda.Add(new AnalysisData(0, 0));

            _peakEda.Clear();
            _peakEda.Add(new AnalysisData(0, 0));
        }

        public void AddOcs(in AnalysisData ocs)
        {
            _ocs.Add(ocs);
        }

        public void AddNnMean(in AnalysisData nnMean)
        {
            _nnMean.Add(nnMean);
        }

        public void AddSdNn(in AnalysisData sdNn)
        {
            _sdNn.Add(sdNn);
        }

        public void AddMeanEda(in AnalysisData meanEda)
        {
            _meanEda.Add(meanEda);
        }

        public void AddPeakEda(in AnalysisData peakEda)
        {
            _peakEda.Add(peakEda);
        }
    }
}