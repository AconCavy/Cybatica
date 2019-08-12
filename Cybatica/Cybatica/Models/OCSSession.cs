using DynamicData;

namespace Cybatica.Models
{
    public struct AnalysisData
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
    }

    public class OcsSession
    {
        public OcsSession()
        {
            Ocs = new SourceList<AnalysisData>();
            NnMean = new SourceList<AnalysisData>();
            SdNn = new SourceList<AnalysisData>();
            MeanEda = new SourceList<AnalysisData>();
            PeakEda = new SourceList<AnalysisData>();

            InitializeSession();
        }

        public SourceList<AnalysisData> Ocs { get; }
        public SourceList<AnalysisData> NnMean { get; }
        public SourceList<AnalysisData> SdNn { get; }
        public SourceList<AnalysisData> MeanEda { get; }
        public SourceList<AnalysisData> PeakEda { get; }

        public void InitializeSession()
        {
            Ocs.Clear();
            Ocs.Add(new AnalysisData(0, 0));

            NnMean.Clear();
            NnMean.Add(new AnalysisData(0, 0));

            SdNn.Clear();
            SdNn.Add(new AnalysisData(0, 0));

            MeanEda.Clear();
            MeanEda.Add(new AnalysisData(0, 0));

            PeakEda.Clear();
            PeakEda.Add(new AnalysisData(0, 0));
        }
    }
}