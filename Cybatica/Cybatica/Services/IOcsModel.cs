using Cybatica.Models;

namespace Cybatica.Services
{
    public interface IOcsModel
    {
        AnalysisData Ocs { get; }
        AnalysisData NnMean { get; }
        AnalysisData SdNn { get; }
        AnalysisData MeanEda { get; }
        AnalysisData PeakEda { get; }
    }
}