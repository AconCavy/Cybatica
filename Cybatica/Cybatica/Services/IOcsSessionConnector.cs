using Cybatica.Models;
using DynamicData;
using System;

namespace Cybatica.Services
{
    public interface IOcsSessionConnector
    {
        IObservable<IChangeSet<AnalysisData>> OcsConnectable { get; }

        IObservable<IChangeSet<AnalysisData>> NnMeanConnectable { get; }

        IObservable<IChangeSet<AnalysisData>> SdNnConnectable { get; }

        IObservable<IChangeSet<AnalysisData>> MeanEdaConnectable { get; }

        IObservable<IChangeSet<AnalysisData>> PeakEdaConnectable { get; }
    }
}