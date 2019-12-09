using Cybatica.Models;
using Cybatica.Services;
using DynamicData;
using ReactiveUI;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Cybatica.ViewModels
{
    public class OcsChartViewModel : ReactiveObject, IDisposable
    {
        public ReadOnlyObservableCollection<AnalysisData> Ocs => _ocs;
        public ReadOnlyObservableCollection<AnalysisData> NnMean => _nnMean;
        public ReadOnlyObservableCollection<AnalysisData> MeanEda => _meanEda;

        private readonly IDisposable _cleanUp;
        private readonly ICybaticaHandler _cybaticaHandler;
        private readonly ReadOnlyObservableCollection<AnalysisData> _ocs;
        private readonly ReadOnlyObservableCollection<AnalysisData> _nnMean;
        private readonly ReadOnlyObservableCollection<AnalysisData> _meanEda;

        public OcsChartViewModel(ICybaticaHandler cybaticaHandler = null)
        {
            _cybaticaHandler = cybaticaHandler ?? Locator.Current.GetService<ICybaticaHandler>();

            var ocs = _cybaticaHandler.OcsConnectable
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _ocs)
                .Subscribe();

            var nnMean = _cybaticaHandler.NnMeanConnectable
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _nnMean)
                .Subscribe();

            var meanEda = _cybaticaHandler.MeanEdaConnectable
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _meanEda)
                .Subscribe();

            _cleanUp = new CompositeDisposable(ocs, nnMean, meanEda);
        }

        public void Dispose()
        {
            _cleanUp?.Dispose();
        }
    }
}